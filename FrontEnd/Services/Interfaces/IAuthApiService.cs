using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;

namespace FrontEnd.Services.Interfaces
{
    public interface IAuthApiService
{
    Task<AuthResponseModel> LoginAsync(LoginViewModel dto);
    Task<AuthResponseModel> RegisterAsync(RegisterViewModel dto);
}
}