using School.Domain;

namespace School.Presentation
{
    public class UpdateInstructorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string ImageURL { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
