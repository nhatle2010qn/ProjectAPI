using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Controllers
{
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _CommentService;

        public CommentController(ICommentService CommentService)
        {
            _CommentService = CommentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> List(int productId)
        {
            var comments = await _CommentService.getCommentList(productId);
            return Ok(comments);
        }           

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save(CommentViewModel vm)
        {
            await _CommentService.Add(vm);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateComment (CommentViewModel vm)
        {
            await _CommentService.Update(vm);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLength(int productId)
        {
            var length = await _CommentService.GetLength(productId);
            return Ok(length);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRatingComment(int productId)
        {
            var rating = await _CommentService.getRating(productId);
            return Ok(rating);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentUser(string userId, int productId)
        {
            var comment = await _CommentService.GetById(userId, productId);
            return Ok(comment);
        }
    }
}