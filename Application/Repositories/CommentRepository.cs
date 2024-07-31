using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories;

public class CommentRepository(DataContext context, IMapper mapper) : ICommentRepository
{
    public async Task<CommentDto> CreateCommentAsync(CommentDto commentDto)
    {
        var comment = mapper.Map<Comment>(commentDto);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
        return commentDto;
    }

    public async Task<bool> DeleteCommentAsync(int id)
    {
        var comment = context.Comments.SingleOrDefault(x => x.Id == id);
        if (comment == null)
        {
            return false;
        }
        context.Comments.Remove(comment);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
    {
        var comments = await context.Comments.Include(p => p.Post).ToListAsync();
        var commentsToReturn = mapper.Map<IEnumerable<CommentDto>>(comments);
        return commentsToReturn;
    }

    public CommentDto GetCommentById(int id)
    {
        var comment = context.Comments.Include(p => p.Post).FirstOrDefault(x => x.Id == id);
        var commentToReturn = mapper.Map<CommentDto>(comment);

        return commentToReturn;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await context.Comments.Where(c => c.PostId == postId).ToListAsync();
        var commentsToReturn = mapper.Map<IEnumerable<CommentDto>>(comments);
        return commentsToReturn;
    }

    public async Task<CommentDto> UpdateCommentAsync(CommentDto commentDto)
    {
        var comment = context.Comments.SingleOrDefault(x => x.Id == commentDto.Id);
        if (comment == null)
        {
            return null;
        }
        comment.Message = commentDto.Message;
        await context.SaveChangesAsync();
        return commentDto;
    }
}