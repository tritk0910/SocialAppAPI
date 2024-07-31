using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostsController(IPostRepository postRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await postRepository.GetAllPosts();
        return Ok(posts);
    }

    [HttpGet("{id:int}")]
    public ActionResult<PostDto> GetPostById(int id)
    {
        var post = postRepository.GetPostById(id);
        if (post == null)
        {
            return NotFound("Post not found");
        }
        return post;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePostAsync(PostDto postDto)
    {
        var post = await postRepository.CreatePostAsync(postDto);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    [HttpPut]
    public async Task<ActionResult<PostDto>> UpdatePostAsync(PostDto postDto)
    {
        var post = postRepository.GetPostById(postDto.Id);
        if (post == null)
        {
            return NotFound();
        }

        var postToReturn = await postRepository.UpdatePostAsync(postDto);
        return Ok(postToReturn);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePostAsync(int id)
    {
        var post = postRepository.GetPostById(id);
        if (post == null)
        {
            return NotFound();
        }
        var result = await postRepository.DeletePostAsync(id);
        if (!result)
        {
            return BadRequest("Failed to delete the post");
        }

        return NoContent();
    }

    [HttpGet("{postId:int}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await postRepository.GetCommentsByPostIdAsync(postId);
        return Ok(comments);
    }
}