using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentViewModel>> getCommentList(int productId);
        Task<CommentViewModel> GetById(string userId, int productId);
        Task Delete(int id);
        Task Add(CommentViewModel commentViewModel);
        Task Update(CommentViewModel commentViewModel);
        Task<int> GetLength(int productId);
        Task<RatingViewModel> getRating(int productId);
    }
}
