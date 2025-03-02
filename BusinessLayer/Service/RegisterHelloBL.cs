using ModelLayer.DTO;
using RepositoryLayer.Service;
using System;
using System.Threading.Tasks;
using NLog;

namespace BusinessLayer.Service
{
    public class RegisterHelloBL
    {
        private readonly RegisterHelloRL _registerHelloRL;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public RegisterHelloBL(RegisterHelloRL registerHelloRL)
        {
            _registerHelloRL = registerHelloRL;
        }

        public async Task<string> Registration(User user)
        {
            try
            {
                bool isRegistered = await _registerHelloRL.RegisterUser(user);
                return isRegistered ? "User registered successfully" : "Username already exists!";
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in Registration");
                throw;
            }
        }

        public async Task<bool> LoginUser(string username, string password)
        {
            try
            {
                var user = await _registerHelloRL.GetUserByUsername(username);
                if (user != null && user.PasswordHash == password)  // In real cases, hash password comparison is needed
                {
                    Logger.Info($"User {username} logged in successfully.");
                    return true;
                }

                Logger.Warn($"Login failed for {username}: Invalid credentials.");
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in LoginUser");
                throw;
            }
        }
    }
}
