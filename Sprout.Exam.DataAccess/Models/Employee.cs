using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprout.Exam.DataAccess.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("FullName", TypeName = "varchar(100)")]
        public string FullName { get; set; }

        [Required]
        [Column("Birthdate", TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [Required]
        [Column("TIN", TypeName = "varchar(100)")]
        public string TIN { get; set; }

        [Required]
        [ForeignKey("EmployeeType")]
        [Column("EmployeeTypeId", TypeName = "int")]
        public int EmployeeTypeId { get; set; }

        [Required]
        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
    }
}
