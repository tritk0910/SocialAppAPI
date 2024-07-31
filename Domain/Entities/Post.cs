namespace Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Body { get; set; } = null!;
    public ICollection<Comment>? Comments { get; set; }
}