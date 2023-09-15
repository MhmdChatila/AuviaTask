namespace AuviaTask.Models
{
    public class EmployeeJob
    {

        public int Id { get; set; } 

        public Guid EmployeeId { get; set; }
        public Guid JobId { get; set; }
        public Employee Employee { get; set; }
        public Job Job { get; set; }

        public bool IsActive { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}