using System;
using System.Collections.Generic;

namespace QuanLyNhanVienDbFirst.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int Salary { get; set; }
        public string? Gender { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;

        //add

    }
}
