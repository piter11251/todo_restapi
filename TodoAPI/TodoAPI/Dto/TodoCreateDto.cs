namespace TodoAPI.Dto
{
    public class TodoCreateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public int Percentage { get; set; } = 0;
    }
}
