namespace TaskManagementAPI.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public string Priority { get; set; } = "Medium";

        public DateTime DueDate { get; set; }

        public int UserId { get; set; }

    }
}
