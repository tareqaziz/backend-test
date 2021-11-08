using BackendCore.Interfaces;
using BackendCore.Utilities;
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
    public class CommentsService : ICommentsService
    {
        private readonly IOptions<ApiEndPoints> apiEndPoints;

        public CommentsService(IOptions<Utilities.ApiEndPoints> apiEndPoints)
        {
            this.apiEndPoints = apiEndPoints;
        }
        public async Task<IEnumerable<Models.Comments>> GetComments()
        {
            var comments = new List<Models.Comments>();

            using (HttpClient client = new HttpClient())
            {
                var commentsResponse = await client.GetAsync(apiEndPoints.Value.Comments);
                if (commentsResponse.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await commentsResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Models.Comments>>(responseBody);
                }
            }

            return comments;
        }

        public async Task<IEnumerable<Models.CommentsDto>> GetCommentsByFilder(string filter)
        {
            var commentsDto = new List<Models.CommentsDto>();
            IEnumerable<Models.Comments> comments = new List<Models.Comments>();

            var commentsResponse = await GetComments();

            filter = filter.Trim().ToLower();

            if (string.IsNullOrEmpty(filter))
            {
                return commentsResponse.Select(c =>
                             new Models.CommentsDto()
                             {
                                 PostId = c.PostId,
                                 Id = c.Id,
                                 Body = c.Body,
                                 Email = c.Email,
                                 Name = c.Name
                             });
            }

            int value;
            if (int.TryParse(filter, out value))
            {
                comments = commentsResponse.Where(f =>
                    f.PostId == Convert.ToInt32(filter) ||
                    f.Id == Convert.ToInt32(filter) ||
                    f.Name.ToLower().Contains(filter) ||
                    f.Email.ToLower().Contains(filter) ||
                    f.Body.ToLower().Contains(filter));
            }
            else
            {
                comments = commentsResponse.Where(f =>
                    f.Name.ToLower().Contains(filter) ||
                    f.Email.ToLower().Contains(filter) ||
                    f.Body.ToLower().Contains(filter));
            }

            return comments.Select(c =>
                             new Models.CommentsDto()
                             {
                                 PostId = c.PostId,
                                 Id = c.Id,
                                 Body = c.Body,
                                 Email = c.Email,
                                 Name = c.Name
                             });

        }
    }
}
