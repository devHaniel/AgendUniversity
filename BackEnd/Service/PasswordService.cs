using BackEnd.Models;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Service
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public string HashPassword(Usuario usuario, string password)
        {
            return _passwordHasher.HashPassword(usuario,usuario.Password);
        }

        public bool VerifyPassword(Usuario usuario, string passwordHash,
            string password)
        {
            var result = _passwordHasher
                .VerifyHashedPassword(usuario, passwordHash, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
