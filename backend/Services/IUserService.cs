using System.Collections.Generic;
using backend.Dtos.Login;
using backend.Models;
using WebApi.Models;

namespace backend.Services
{
    public interface IUserLoginService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        UserLogin GetById(int id);

        IEnumerable<UserLogin> CreateUsers();
    }
}