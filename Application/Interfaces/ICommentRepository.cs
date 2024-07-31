using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
    CommentDto GetCommentById(int id);
    Task<CommentDto> CreateCommentAsync(CommentDto commentDto);
    Task<CommentDto> UpdateCommentAsync(CommentDto commentDto);
    Task<bool> DeleteCommentAsync(int id);
    Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(int postId);
}