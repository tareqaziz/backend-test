using BackendCore.Interfaces;
using BackendCore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendCore.Services
{
    public class PostsService : IPostsService
    {
        private readonly ICommentsService commentsService;
        private readonly IOptions<ApiEndPoints> apiEndPoints;

        public PostsService(ICommentsService commentsService, IOptions<Utilities.ApiEndPoints> apiEndPoints)
        {
            this.commentsService = commentsService;
            this.apiEndPoints = apiEndPoints;
        }

        public async Task<IEnumerable<Models.PostsDto>> GetTopPosts(int top)
        {
            var postDto = new List<Models.PostsDto>();
            var comm = await commentsService.GetComments();

            using (HttpClient client = new HttpClient())
            {
                var postsResponse = await client.GetAsync(apiEndPoints.Value.Posts);
                if (postsResponse.StatusCode == HttpStatusCode.OK)
                {
                    var posts = JsonConvert.DeserializeObject<IEnumerable<Models.Posts>>(await postsResponse.Content.ReadAsStringAsync());

                    return posts
                        .Select(p =>
                             new Models.PostsDto
                             {
                                 Post_Id = p.Id,
                                 Post_Title = p.Title,
                                 Post_Body = p.Body,
                                 Total_Number_Of_Comments = comm.Count(c => c.PostId == p.Id)
                             })
                        .OrderByDescending(o => o.Total_Number_Of_Comments)
                        .Take(top);
                }
            }

            return postDto;
        }
    }
}
