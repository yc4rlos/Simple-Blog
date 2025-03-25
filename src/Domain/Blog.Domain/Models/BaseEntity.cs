namespace Blog.Domain.Models;

public class BaseEntity
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatedById { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int UpdatedById { get; set; }
    public DateTime? DeletedAt { get; set; }
}