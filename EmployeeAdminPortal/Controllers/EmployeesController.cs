using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var result = dbContext.Employees.ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetAllEmployeeById(Guid id)
        {
            var result = dbContext.Employees.Find(id);

            if (result == null) {
                return NotFound("Employee Not Found");
            } else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto employeeDto)
        {
            var newEmployee = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary
            };

            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();

            return Ok(newEmployee);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployeeById(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if ( employee == null)
            {
                return NotFound("Employee Not Found");
            }
            else
            {
                employee.Name = updateEmployeeDto.Name;
                employee.Email = updateEmployeeDto.Email;
                employee.Phone = updateEmployeeDto.Phone;
                employee.Salary = updateEmployeeDto.Salary;
                dbContext.SaveChanges();
                return Ok(employee);
            }
        }

        [HttpPatch]
        [Route("{id:guid}")]
        public IActionResult PatchEmployeeById(Guid id, PatchEmployeeDto patchEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound("Employee Not Found");
            }
            else
            {
                employee.Name = (patchEmployeeDto.Name != null) ? patchEmployeeDto.Name : employee.Name;
                employee.Phone = (patchEmployeeDto.Phone != null)? patchEmployeeDto.Phone : employee.Phone;
                employee.Email = (patchEmployeeDto.Email != null) ? patchEmployeeDto.Email : employee.Email;
                var newSalary = (patchEmployeeDto.Salary != null) ? patchEmployeeDto.Salary : employee.Salary;
                employee.Salary = (decimal)newSalary;

                dbContext.SaveChanges();
                return Ok(employee);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployeeById(Guid id)
        {
            var result = dbContext.Employees.Find(id);

            if (result == null)
            {
                return NotFound("Employee Not Found");
            }
            else
            {
                dbContext.Employees.Remove(result);
                dbContext.SaveChanges();
                return Ok("Employee Deleted");
            }
        }

    }
}
