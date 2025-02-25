using HRMSystem.Data;
using HRMSystem.Models;
using HRMSystem.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HRMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Contract)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    UserName = e.User.UserName,
                    Email = e.User.Email,
                    PhoneNumber = e.User.PhoneNumber,
                    RoleName = e.User.Role.Name,
                    DepartmentName = e.Department.Name,
                    HireDate = e.HireDate,
                    SalaryAmount = e.Contract.Salary,
                    ContractStartDate = e.Contract.StartDate,
                    ContractEndDate = e.Contract.EndDate
                }).ToListAsync();
            return employees;
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Contract)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    UserName = e.User.UserName,
                    Email = e.User.Email,
                    PhoneNumber = e.User.PhoneNumber,
                    RoleName = e.User.Role.Name,
                    DepartmentName = e.Department.Name,
                    HireDate = e.HireDate,
                    SalaryAmount = e.Contract.Salary,
                    ContractStartDate = e.Contract.StartDate,
                    ContractEndDate = e.Contract.EndDate
                }).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDto employeeDto)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == employeeDto.DepartmentName);
            if (department == null)
            {
                return BadRequest("Invalid department.");
            }

            var employee = new Employee
            {
                FullName = employeeDto.FullName,
                DepartmentId = department.Id,
                HireDate = employeeDto.HireDate
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employeeDto);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == employeeDto.DepartmentName);
            if (department == null)
            {
                return BadRequest("Invalid department.");
            }

            employee.FullName = employeeDto.FullName;
            employee.DepartmentId = department.Id;
            employee.HireDate = employeeDto.HireDate;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
