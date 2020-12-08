using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Portfolio.Models;
using Web_Portfolio.Services;

namespace Web_Portfolio.Controllers
{
     [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("login")]
        public ActionResult<User> Login(User userIn)
        {
            
            var user = _authService.Get(u => u.Username == userIn.Username);
            
            if(user != null)
            {   
                var reHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userIn.Password,
                salt: Convert.FromBase64String(user.Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

                if(reHash == user.Password)
                {
                    var a = 1;

                    var b = 1;

                    var c = a + b;
                    //  Return a jwt token
                }
                else
                {
                    // return password dont match
                }
            }

            return null; // TODO need to fix this
            
        }



        /*

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/projects")]
        public ActionResult<List<Post>> Get() =>
            _projService.Get();

        [HttpGet("{id:length(24)}")]
        [Microsoft.AspNetCore.Mvc.Route("api/projects/{id}")]
        public ActionResult<Post> Get(string id)
        {
            var proj = _projService.Get(id);

            if (proj == null)
            {
                return NotFound();
            }

            return proj;
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/projects")]
        public ActionResult<Post> Create(Post proj)
        {
            _projService.Create(proj);

            return CreatedAtRoute("GetBook", new { id = proj.Id.ToString() }, proj);
        }

        [HttpPut("{id:length(24)}")]
        [Microsoft.AspNetCore.Mvc.Route("api/projects/id")]
        public IActionResult Update(string id, Post projIn)
        {
            var proj = _projService.Get(id);

            if (proj == null)
            {
                return NotFound();
            }

            _projService.Update(id, projIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Microsoft.AspNetCore.Mvc.Route("api/projects/id")]
        public IActionResult Delete(string id)
        {
            var proj = _projService.Get(id);

            if (proj == null)
            {
                return NotFound();
            }

            _projService.Remove(proj.Id);

            return NoContent();
        }

    */
    }
}
