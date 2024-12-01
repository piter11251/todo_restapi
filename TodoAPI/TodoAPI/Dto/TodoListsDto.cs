using TodoAPI.Entities;

namespace TodoAPI.Dto
{
    public class TodoListsDto
    {
        public List<Todo>? TodayList { get; set; }
        public List<Todo>? ThisWeekList { get; set; }
        public List<Todo>? ThisMonthList { get; set; }
    }
}
