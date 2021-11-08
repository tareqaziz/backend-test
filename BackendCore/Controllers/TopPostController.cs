using BackendCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopPostController : ControllerBase
    {
        private readonly IPostsService postsService;

        public TopPostController(IPostsService postsService)
        {
            this.postsService = postsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int top)
        {
            return Ok(await postsService.GetTopPosts(top));
        }
    }
}
