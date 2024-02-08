using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprout.Exam.DataAccess.Models
{
    [Table("EmployeeType")]
    public class EmployeeType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("TypeName", TypeName = "varchar(50)")]
        public string TypeName { get; set; }
    }
}
