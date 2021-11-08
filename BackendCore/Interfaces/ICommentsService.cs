using BackendCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendCore.Interfaces
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comments>> GetComments();
        Task<IEnumerable<CommentsDto>> GetCommentsByFilder(string filter);
    }
}