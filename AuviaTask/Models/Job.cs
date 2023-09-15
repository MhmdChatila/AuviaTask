using System.ComponentModel.DataAnnotations;

namespace AuviaTask.Models
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public ICollection<EmployeeJob> EmployeeJobs { get; set; } = new List<EmployeeJob>();
    }
    public enum Category
    {
        First,
        Second,
        Third
    }
}
