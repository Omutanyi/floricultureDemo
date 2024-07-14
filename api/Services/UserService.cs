using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace api.Services
{
    public class UserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUser(string username, string email, string password)
        {
            if (await _context.User.AnyAsync(u => u.UserName == username || u.Email == email))
            {
                throw new Exception("User already exists.");
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                // Id = Guid.NewGuid(),
                UserName = username,
                Email = email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                Role = "User" // Default role
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
            {
                return null;
            }

            return user;
        }
    }
}