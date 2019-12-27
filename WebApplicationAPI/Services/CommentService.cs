using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IAsyncRepository<Comment> _commentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public CommentService(IAsyncRepository<Comment> commentRepository, IMapper mapper, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task Add(CommentViewModel commentViewModel)
        {
            var item = new Comment()
            {
                Id = commentViewModel.Id,
                ProductId = commentViewModel.ProductId,
                UserId = Guid.Parse(commentViewModel.UserId),
                Content = commentViewModel.Content,
                Rating = commentViewModel.Rating,  
                DateReview = DateTime.Now
            };
            await _commentRepository.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var item = await _commentRepository.GetByIdAsync(id);
            await _commentRepository.DeleteAsync(item);
        }

        public async Task<CommentViewModel> GetById(string userId, int productId)
        {
            var items = await _commentRepository.ListAllAsync();
            var item = items.Where(i => i.UserId.ToString() == userId && i.ProductId == productId).SingleOrDefault();
            var username = await _userManager.FindByIdAsync(item.UserId.ToString());
            CommentViewModel comment = new CommentViewModel()
            {
                Id = item.Id,
                ProductId = item.ProductId,
                UserId = item.UserId.ToString(),
                UserName = username.Name,
                Content = item.Content,
                Rating = item.Rating,
                DateReview = item.DateReview
            };
            return comment;
        }

        public async Task<int> GetLength(int productId)
        {
            var item = await _commentRepository.ListAllAsync();
            item = item.Where(p => p.ProductId == productId).ToList();
            var length = item.Count();
            return length;
        }

        public async Task<List<CommentViewModel>> getCommentList(int productId)
        {
            var items = await _commentRepository.ListAllAsync();
            items = items.Where(p => p.ProductId == productId).ToList();
            List<CommentViewModel> Commentvm = new List<CommentViewModel>();
            foreach(var item in items)
            {

                var username = await _userManager.FindByIdAsync(item.UserId.ToString());
                CommentViewModel comment = new CommentViewModel()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    UserId = item.UserId.ToString(),
                    UserName = username.Name,
                    Content = item.Content,
                    Rating = item.Rating,
                    DateReview = item.DateReview
                };
                Commentvm.Add(comment);
            }         
            return Commentvm;
        }

        public async Task<RatingViewModel> getRating(int productId)
        {
            var items = await _commentRepository.ListAllAsync();
            items = items.Where(p => p.ProductId == productId).ToList();
            RatingViewModel rating = new RatingViewModel();
            if (items.Count > 0)
            {             
                rating.Rating1 = items.Where(i => i.Rating == 1).Count();
                rating.Rating2 = items.Where(i => i.Rating == 2).Count();
                rating.Rating3 = items.Where(i => i.Rating == 3).Count();
                rating.Rating4 = items.Where(i => i.Rating == 4).Count();
                rating.Rating5 = items.Where(i => i.Rating == 5).Count();
                rating.Rating = (rating.Rating1 * 1 + rating.Rating2 * 2 + rating.Rating3 * 3 + rating.Rating4 * 4 + rating.Rating5 * 5) / (rating.Rating1 + rating.Rating2 + rating.Rating3 + rating.Rating4 + rating.Rating5);
            }          
            return rating;
        }
        public async Task Update(CommentViewModel commentViewModel)
        {
            Comment comment = new Comment()
            {
                Id = commentViewModel.Id,
                ProductId = commentViewModel.ProductId,
                UserId = Guid.Parse(commentViewModel.UserId),
                Content = commentViewModel.Content,
                Rating = commentViewModel.Rating,
                DateReview = DateTime.Now
            };

            await _commentRepository.UpdateAsync(comment);
        }
    }
}
