using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthorizationController : Controller
    {
        private readonly UserService service;
        public UserAuthorizationController(UserService _service)
        {
            service = _service;
        }
        // [AllowAnonymous]
        [HttpGet]
        public async Task<List<User>> GetUsers(){
            return await service.GetAsync();
        }

        [HttpGet]
        [Route("email")]
        public async Task<ActionResult<User>> GetUser(string email){
            var user = await service.GetUserAsync(email);
            if(user == null){
                return NotFound();
            }
            return user;
        }
        
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<User>> AddUser(User user){
            if(user == null){
                return null;
            }
            await service.AddUserAsync(user);
            return user;
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<ActionResult<User>> RemoveUser(string email){
            var user = await service.GetUserAsync(email);
            if(user == null){
                return NotFound();
            }
            await service.RemoveUserAsync(email);
            return user;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public ActionResult<User> Login(User user)
        {
            // service.AddUser(user);
            // return user;
            var token = service.Authenticate(user.EmailAddress,user.Password);
            if(token==null){
                return Unauthorized();
            }
            return Ok(new{token,user});
        }
    }
}