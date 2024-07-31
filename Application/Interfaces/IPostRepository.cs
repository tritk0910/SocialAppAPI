using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<PostDto>> GetAllPosts();
    PostDto GetPostById(int id);
    Task<PostDto> CreatePostAsync(PostDto postDto);
    Task<PostDto> UpdatePostAsync(PostDto postDto);
    Task<bool> DeletePostAsync(int id);
    Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
}