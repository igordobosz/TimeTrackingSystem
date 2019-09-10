using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrackingSystem.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class AuthorizationController : ControllerBase

    {

        private readonly AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        //GET api/<controller>/5
        [HttpGet]
        [Authorize]
        [Route("Users")]
        public User[] Get()
        {
            return AuthorizationService._users;
        }

        // POST api/<controller>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [ValidateModel]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginDTO loginDto)
        {
            return await _authorizationService.Login(loginDto);
        }

    }
}
