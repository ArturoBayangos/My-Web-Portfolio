using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Web_Portfolio.Models;
using Web_Portfolio.Services;

namespace Web_Portfolio.Controllers
{
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsService _projService;

        public ProjectsController(ProjectsService projService)
        {
            _projService = projService;
        }

        [Microsoft.AspNetCore.Mvc.Route("api/hash")]
        public string PPP()
        {
            string password = "masterdu";

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }


            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return "Hashed password : " + hashed + " , Salt : " +  Convert.ToBase64String(salt);
        }

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
    }
}

