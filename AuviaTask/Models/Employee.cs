namespace AuviaTask.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public DateTime EmploymentDate { get; set; }
        public byte[] PersonalPhoto { get; set; }
        public Governorate Governorate { get; set; }
        public bool IsProbation { get; set; }
        public bool IsDeleted { get; set; }

        // Define a collection of EmployeeJob entities to represent the relationship
        public ICollection<EmployeeJob> EmployeeJobs { get; set; } = new List<EmployeeJob>();
    }

    public enum Governorate
    {
        Beirut,
        MountLebanon,
        North,
        South,
        Beqaa,
        Nabatieh,
        Akkar,
        BaalbekHermel,

    }
}