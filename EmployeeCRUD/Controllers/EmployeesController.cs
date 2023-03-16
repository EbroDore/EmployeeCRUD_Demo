using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using EmployeeCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoEbContext employeeCRUD;

        public EmployeesController(MVCDemoEbContext employeeCRUD)
        {
            this.employeeCRUD = employeeCRUD;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeVM addEmployeeVM) 
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeVM.Name,
                Email = addEmployeeVM.Email,
                Salary = addEmployeeVM.Salary,
                DateOfBirth = addEmployeeVM.DateOfBirth,
                Department = addEmployeeVM.Department
            };

            await employeeCRUD.Employees.AddAsync(employee);
            await employeeCRUD.SaveChangesAsync();
            return RedirectToAction("Add");
        }


    }
}
