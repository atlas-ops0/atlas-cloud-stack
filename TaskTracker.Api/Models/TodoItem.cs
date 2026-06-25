namespace TaskTracker.Api.Models;

public class TodoItem
{
	public int Id { get; set;}
	public string Title { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set;}
	public bool IsCompleted { get; set; }
}
