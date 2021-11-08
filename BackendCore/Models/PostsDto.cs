using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Models
{
    public class PostsDto
    {
        public int Post_Id { get; set; }
        public string Post_Title { get; set; }
        public string Post_Body { get; set; }
        public int Total_Number_Of_Comments { get; set; }
    }
}
