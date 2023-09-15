using System.ComponentModel.DataAnnotations;

namespace AuviaTask.Models
{
    public class AddEmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
		[Required]
		public DateTime BirthDate { get; set; }
		[Required]
        public string Phone { get; set; }
		[Required]
		public DateTime EmploymentDate { get; set; }
        public byte[] PersonalPhoto { get; set; }
		[Required]
		public Governorate Governorate { get; set; }
        public bool IsProbation { get; set; }
        public bool IsDeleted { get; set; }
    }
}
