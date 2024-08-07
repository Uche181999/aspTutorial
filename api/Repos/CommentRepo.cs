using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Data;
using api.Models;
using api.Mappers;
using api.Dtos.Comment;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _context;
        public CommentRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();

        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }
        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
        {
            var existComment = await  _context.Comments.FirstOrDefaultAsync(s => s.Id == id);

            if (existComment == null)
            {
                return null;
            }
            existComment.Title = commentDto.Title;
            existComment.Content = commentDto.Content;

            await _context.SaveChangesAsync();

            return existComment;
        }
        public async Task<Comment?> DeleteAsync(int id){
            var commentModel = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);
            if (commentModel== null){
                return null;
            }
             _context.Comments.Remove(commentModel);
             await _context.SaveChangesAsync();

             return commentModel;

        }

    }
}