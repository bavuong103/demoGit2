using System;
using System.Collections.Generic;

namespace QuanLyNhanVienDbFirst.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdDepartment { get; set; }
        public string NameDepartment { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
