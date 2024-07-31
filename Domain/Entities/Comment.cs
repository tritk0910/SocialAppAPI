namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public required int PostId { get; set; }
    public Post Post { get; set; } = null!;

}