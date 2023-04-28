using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace JWTAuthentication.Services
{
    public class UserService
    {
        private IMongoCollection<User> users;
        private readonly string key;
        private readonly string issuer;
        private readonly string audiance;

        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("HyphenDb"));
            var database = client.GetDatabase("AuthUsers");
            users = database.GetCollection<User>("Users");
            this.key = configuration.GetSection("JwtKey").Value;
            this.issuer = configuration.GetSection("ValidateIssuer").Value;
            this.audiance = configuration.GetSection("ValidateAudiance").Value;
        }
        public async Task<List<User>> GetAsync() => await users.Find(user => true).ToListAsync();
        public async Task<User?> GetUserAsync(string email) => await users.Find<User>(user => user.EmailAddress == email).FirstOrDefaultAsync();
        public async Task AddUserAsync(User user){
            await users.InsertOneAsync(user);
        }
        public async Task RemoveUserAsync(string email) => await users.DeleteOneAsync(x => x.EmailAddress == email);

        public string Authenticate(string email,string password)
        {
            var user = this.users.Find(x => x.EmailAddress == email && x.Password == password).FirstOrDefault();
            if(user == null)
            {
                return null;
            }
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var tokenKey = Encoding.ASCII.GetBytes(key);
            // var tokenDescriptor = new SecurityTokenDescriptor(){
            //     Subject = new ClaimsIdentity(new Claim[]{
            //         new Claim(ClaimTypes.Email,email),
            //     }),
            //     Expires = DateTime.UtcNow.AddHours(1),
            //     SigningCredentials = new SigningCredentials(
            //         new SymmetricSecurityKey(tokenKey),
            //         SecurityAlgorithms.HmacSha256Signature
            //     ),
            //     Issuer = issuer,
            //     Audience = audiance
            // };
            // var token = tokenHandler.CreateToken(tokenDescriptor);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var token = new JwtSecurityToken(  
                    issuer: issuer,  
                    audience: audiance,  
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)    
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}