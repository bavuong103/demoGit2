using QuanLyNhanVienDbFirst.Models;

namespace QuanLyNhanVienDbFirst.Services
{
    public interface IEmployee
    {
        IEnumerable<Employee> getEmployeeAll();

        int totalEmployee();

        int numberPage(int totalEmployee, int limit);

        IEnumerable<Employee> paginationEmployee(int start, int limit);
    }
}
