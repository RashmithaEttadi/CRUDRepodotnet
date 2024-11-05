using ApplicationCRUD.Data;
using ApplicationCRUD.Models;
using ApplicationCRUD.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees=dbContext.Employees.ToList();
            return Ok(employees);
        }

         [HttpGet]
         [Route("{id:guid}")]
        public IActionResult GetEmployeesById(Guid id)
        {
            var employee=dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeDto addEmployeeDto)
        {
            var empEntity=new Employee(){
                Name=addEmployeeDto.Name,
                Email=addEmployeeDto.Email,
                Phone=addEmployeeDto.Phone,
                Salary=addEmployeeDto.Salary

            };
            dbContext.Employees.Add(empEntity);
            dbContext.SaveChanges();
            return Ok(empEntity);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmpDto updateEmpDto)
        {
            var employee=dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }  
            employee.Name=updateEmpDto.Name;
            employee.Email=updateEmpDto.Email;
            employee.Phone=updateEmpDto.Phone;
            employee.Salary=updateEmpDto.Salary;
            dbContext.SaveChanges();
            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
          var employee=dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            } 
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
