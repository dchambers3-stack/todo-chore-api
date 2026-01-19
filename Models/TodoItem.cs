namespace TodoChoreApp2.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

    }
}
