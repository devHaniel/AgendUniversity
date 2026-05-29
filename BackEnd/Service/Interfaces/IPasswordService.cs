using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.Service.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(Usuario usuario, string password);
        bool VerifyPassword(Usuario usuario, string passwordHash, string password);
    }
}