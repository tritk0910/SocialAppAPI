namespace Application.DTOs;

public class PostDto
{
    public int Id { get; set; }
    public string Body { get; set; } = null!;
    public ICollection<CommentDto> Comments { get; set; }
}