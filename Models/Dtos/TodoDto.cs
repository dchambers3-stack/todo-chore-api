namespace TodoChoreApp2.Models.Dtos
{
    public class TodoDto
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
