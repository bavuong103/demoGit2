using Microsoft.EntityFrameworkCore;
using QuanLyNhanVienDbFirst.Models;

namespace QuanLyNhanVienDbFirst.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly quanlynhanvien2Context _context;
        private List<Employee> employees = new List<Employee>();
        public EmployeeService(quanlynhanvien2Context context)
        {
            _context = context;
            this.employees = _context.Employees.ToList();
        }

        public IEnumerable<Employee> getEmployeeAll()
        {
            return employees;
        }

        public int totalEmployee()
        {
            return employees.Count;
        }

        public int numberPage(int totalEmployee, int limit)
        {
            float numberpage = totalEmployee / limit;
            return (int)Math.Ceiling(numberpage);
        }

        public IEnumerable<Employee> paginationEmployee(int start, int limit)
        {
            var data = _context.Employees.Include(e => e.Department);
            var dataEmployee = data.OrderBy(x => x.Id).Skip(start).Take(limit);

            return dataEmployee.ToList();
        }

        
    }
}
