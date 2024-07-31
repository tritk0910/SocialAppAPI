using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories;


public class PostRepository(DataContext context, IMapper mapper) : IPostRepository
{
    public async Task<PostDto> CreatePostAsync(PostDto postDto)
    {
        var post = mapper.Map<Post>(postDto);
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        var postToReturn = mapper.Map<PostDto>(post);
        return postToReturn;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var post = context.Posts.SingleOrDefault(x => x.Id == id);
        if (post == null)
        {
            return false;
        }
        // Retrieve and remove all comments associated with the post
        var comments = context.Comments.Where(c => c.PostId == id);
        context.Comments.RemoveRange(comments);

        context.Posts.Remove(post);

        var result = await context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IEnumerable<PostDto>> GetAllPosts()
    {
        var posts = await context.Posts.Include(c => c.Comments).ToListAsync();

        var postsToReturn = mapper.Map<IEnumerable<PostDto>>(posts);
        return postsToReturn;
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = context.Comments.Where(c => c.PostId == postId);
        return await comments.ToListAsync();
    }

    public PostDto GetPostById(int id)
    {
        var post = context.Posts.Include(c => c.Comments).FirstOrDefault(x => x.Id == id);
        var postToReturn = mapper.Map<PostDto>(post);
        return postToReturn;
    }

    public async Task<PostDto> UpdatePostAsync(PostDto postDto)
    {
        var post = context.Posts.SingleOrDefault(x => x.Id == postDto.Id);
        if (post == null)
        {
            return null;
        }

        mapper.Map(postDto, post);

        await context.SaveChangesAsync();
        return postDto;
    }
}