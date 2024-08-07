using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Models;
using api.Repos;
using api.Dtos.Comment;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;
        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comment = await _commentRepo.GetAllAsync();
            var commentDto = comment.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());

        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExistAsync(stockId))
            {
                return BadRequest("stock id does not exist");
            }
            var commentModel = commentDto.ToCreateCommentDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }


    }
}