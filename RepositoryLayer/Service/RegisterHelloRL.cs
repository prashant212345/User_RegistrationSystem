using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using NLog;
using System;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class RegisterHelloRL
    {
        private readonly UserAuthDbContext _context;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public RegisterHelloRL(UserAuthDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                {
                    Logger.Warn($"Registration failed: Username {user.Username} already exists.");
                    return false;
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                Logger.Info($"User {user.Username} registered successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in RegisterUser");
                throw;
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetUserByUsername");
                throw;
            }
        }
    }
}
