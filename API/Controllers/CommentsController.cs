using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CommentsController(ICommentRepository commentRepository, IPostRepository postRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllCommentsAsync()
    {
        var comments = await commentRepository.GetAllCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CommentDto> GetCommentById(int id)
    {
        var comment = commentRepository.GetCommentById(id);
        if (comment == null)
        {
            return NotFound("Comment not found");
        }
        return comment;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateCommentAsync(CommentDto commentDto)
    {
        var post = postRepository.GetPostById(commentDto.PostId);
        if (post == null)
        {
            return BadRequest("Post does not exist");
        }
        if (post.Comments.Any(c => c.Id == commentDto.Id))
        {
            return BadRequest("Comment already exists");
        }
        var comment = await commentRepository.CreateCommentAsync(commentDto);
        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
    }

    [HttpPut]
    public async Task<ActionResult<CommentDto>> UpdateCommentAsync(CommentDto commentDto)
    {
        var post = postRepository.GetPostById(commentDto.PostId);
        if (post == null)
        {
            return BadRequest("Post does not exist");
        }

        var targetedComment = post.Comments.FirstOrDefault(c => c.Id == commentDto.Id);
        if (targetedComment == null)
        {
            return BadRequest("Comment does not exist");
        }
        var comment = await commentRepository.UpdateCommentAsync(commentDto);
        return Ok(comment);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCommentAsync(int id)
    {
        var result = await commentRepository.DeleteCommentAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("post/{postId:int}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await commentRepository.GetCommentsByPostIdAsync(postId);
        return Ok(comments);
    }
}