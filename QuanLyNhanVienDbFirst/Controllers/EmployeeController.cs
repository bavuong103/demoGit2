using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using QuanLyNhanVienDbFirst.Models;
using QuanLyNhanVienDbFirst.Services;

namespace QuanLyNhanVienDbFirst.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;
        private readonly quanlynhanvien2Context _context;

        public EmployeeController(quanlynhanvien2Context context, IEmployee employee)
        {
            _context = context;
            _employee = employee;
        }

        // GET: Employee
        public async Task<IActionResult> Index(int? page=0)
        {
            //var list = _context.Employees.Include(e => e.Department).ToList();
            //return View(list);

            int limit = 2;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;

            int totalEmployee = _employee.totalEmployee();

            ViewBag.totalEmployee = totalEmployee;

            ViewBag.numberPage = _employee.numberPage(totalEmployee, limit);

            var data = _employee.paginationEmployee(start, limit);

            return View(data);

        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public async Task<ActionResult> Create()
        {
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            
                Employee e = new Employee();
                e.Id = employee.Id;
                e.Name = employee.Name;
                e.UserName = employee.UserName;
                e.Password = employee.Password;
                e.Address = employee.Address;
                e.Email = employee.Email;
                e.Phone = employee.Phone;
                e.Salary = employee.Salary;
                e.DepartmentId = employee.DepartmentId;

                await _context.AddAsync(e);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment", employee.DepartmentId);
            ViewBag.Department = await _context.Departments.ToListAsync();
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            
                try
                {
                // todo

                    var e = await _context.Employees.FindAsync(id);
                    e.Id = employee.Id;
                    e.Name = employee.Name;
                    e.UserName = employee.UserName;
                    e.Password = employee.Password;
                    e.Address = employee.Address;
                    e.Email = employee.Email;
                    e.Phone = employee.Phone;
                    e.Salary = employee.Salary;
                    e.Gender = employee.Gender;
                    e.DepartmentId = employee.DepartmentId;

                    //_context.Update(employee);
                    _context.Entry(e).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'quanlynhanvien2Context.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GroupBy()
        {
            // lay name khac nhau
            var employee = from e in _context.Employees
                           group e by new { e.Name} into e
                           select new Employee()
                           {
                               //Id = e.Key.Id,
                               Name = e.Key.Name,
                               
                           };
            return View(employee);
        }

        public async Task<ActionResult> Distinct()
        {
            var employee = (await _context.Employees.ToListAsync())
                           .Distinct(new EmployeeComparer());

            return View(employee);
        }

        public ActionResult GroupBySumSalary()
        {
            // lay name giong nhau
            var employee = from e in _context.Employees
                           group e by new { e.Name } into e
                           select new EmployeeDTO()
                           {
                               //Id = e.Key.Id,
                               Name = e.Key.Name,
                               Quantity = e.Count(),
                               SumSalary = e.Sum(x => x.Salary),

                           };
            return View(employee);
        }

        public ActionResult DistinctSumSalary()
        {
            // lay name giong nhau
            var employee = _context.Employees.ToList()
                           .Distinct(new EmployeeComparer());

            return View(employee);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            // khoi tao Session
            var session = HttpContext.Session;
            //ViewBag.session = session;
            

            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = _context.Employees.Where(s => s.UserName.Equals(username) 
                && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    session.SetString("Name", data.FirstOrDefault().Name);
                    session.SetString("UserName", data.FirstOrDefault().UserName);
                    session.SetInt32("Id", data.FirstOrDefault().Id);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            var session = HttpContext.Session;

            session.Clear();//remove session
            return RedirectToAction("Login");
        }



        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}
