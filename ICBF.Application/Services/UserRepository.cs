using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ICBF.Application.Interfaces;
using ICBF.Domain.DTO;
using ICBF.Domain.Models;
using ICBF.Persistence;

namespace ICBF.Application.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddUser(AppUser user)
        {
            _context.Users.Add(user);
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Where(x => x.Username == username)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }
    }
}
