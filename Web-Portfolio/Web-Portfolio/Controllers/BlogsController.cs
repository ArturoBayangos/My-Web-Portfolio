using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Portfolio.Models;

namespace Web_Portfolio
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BlogsService _blogsService;

        public BooksController(BlogsService blogService)
        {
            _blogsService = blogService;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/blogs")]
        public ActionResult<List<Post>> Get() =>
            _blogsService.Get();

        [HttpGet("{id:length(24)}")]
        [Microsoft.AspNetCore.Mvc.Route("api/blogs/{id}")]
        public ActionResult<Post> Get(string id)
        {
            var blog = _blogsService.Get(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/blogs")]
        public ActionResult<Post> Create(Post blog)
        {
            _blogsService.Create(blog);

            return CreatedAtRoute("GetBook", new { id = blog.Id.ToString() }, blog);
        }

        [HttpPut("{id:length(24)}")]
        [Microsoft.AspNetCore.Mvc.Route("api/blogs/id")]
        public IActionResult Update(string id, Post blogIn)
        {
            var blog = _blogsService.Get(id);

            if (blog == null)
            {
                return NotFound();
            }

            _blogsService.Update(id, blogIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Microsoft.AspNetCore.Mvc.Route("api/blogs/id")]
        public IActionResult Delete(string id)
        {
            var book = _blogsService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _blogsService.Remove(book.Id);

            return NoContent();
        }
    }
}
