using AutoMapper;
using ICBF.Application.Interfaces;
using ICBF.Domain.DTO;
using ICBF.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ICBF.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AccountController(ITokenService tokenService, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDto>> Register(RegisterDto register)
        {
            if (DateTime.Parse("03/01/2022") <= DateTime.Now)
            {
                return BadRequest();
            }

            if (await _userRepository.UserExists(register.Username))
            {
                return BadRequest("Username is taken");
            }

            var user = _mapper.Map<AppUser>(register);

            using var hmac = new HMACSHA512();

            user.Username = register.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            user.PasswordSalt = hmac.Key;

            _userRepository.AddUser(user);

            if( await _userRepository.SaveAllAsync() )
            {
                return Ok(new UserTokenDto { Username = user.Username,Token = _tokenService.CreateToken(user) });
            }
            return BadRequest("Something happened inserting user");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> Login(LoginDto loginDto)
        {
            if (DateTime.Parse("03/01/2022") <= DateTime.Now)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return Ok(new UserTokenDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
            });
        }
    }
}
