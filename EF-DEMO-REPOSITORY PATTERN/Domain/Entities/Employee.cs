
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        public string Email { get; set; }
        [Column("job_id")]
        public string JobTitle { get; set; }
        [Column("hire_date")]
        public System.DateTime HireDate { get; set; }
        public double Salary { get; set; }

    }
}
