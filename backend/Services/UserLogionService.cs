using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend.DataAccess.Context;
using backend.Dtos.Login;
using backend.Helpers;
using backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace backend.Services
{
    public class UserLoginService : IUserLoginService
    {

        //Mocked userlogins due to time constraints, also the passwords must by hashed. 
        // This list is used to Init Create some logins via the createusers call. 
        private List<UserLogin> _users = new List<UserLogin>
        {
            new UserLogin { Username = "Admin", Password = "Admin" , Role="Admin" , userInformation= new UserInformation{FirstName= "Admin FirstName", LastName="Admin LastName", Email="email@Admin.com", WebsiteURL="www.adminsWebsite.com"}},
            new UserLogin { Username = "User1", Password = "User1" , Role="User",userInformation= new UserInformation{FirstName= "User 1 FirstName", LastName="User 1 LastName", Email="email@User1.com", WebsiteURL="www.User1Website.com"}},
            new UserLogin {  Username = "User2", Password = "User2" , Role="User", userInformation= new UserInformation{FirstName= "User 2 FirstName", LastName="User 2 LastName", Email="email@User3.com", WebsiteURL="www.User2Website.com"}}
        };

        private readonly AppSettings _appSettings;
        private readonly GoodFoodContext _context;

        public UserLoginService(IOptions<AppSettings> appSettings, GoodFoodContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.UserLogins.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public UserLogin GetById(int id)
        {
            return _context.UserLogins.FirstOrDefault(x => x.Id == id);
        }

        private string generateJwtToken(UserLogin user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<UserLogin> CreateUsers()
        {
            _context.UserLogins.AddRange(_users);
            var users = _context.UserLogins.ToList();
            _context.SaveChanges();

            return users;
        }
    }
}