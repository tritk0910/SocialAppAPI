namespace Application.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public required int PostId { get; set; }
    public required string Message { get; set; }
}