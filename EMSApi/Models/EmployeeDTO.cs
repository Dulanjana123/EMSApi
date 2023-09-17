using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApi.Models
{
    public class CreateEmployeeDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "First Name is too long")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class EmployeeDTO : CreateEmployeeDTO
    {
        public int Id { get; set; }
    }
}
