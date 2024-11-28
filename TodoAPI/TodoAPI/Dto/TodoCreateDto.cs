namespace TodoAPI.Dto
{
    public class TodoCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Expiry { get; set; }
        public int Percentage { get; set; } = 0;
    }
}
