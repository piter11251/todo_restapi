using TodoAPI.Entities.Enums;

namespace TodoAPI.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public int PercentComplete { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Uncompleted;
    }
}
