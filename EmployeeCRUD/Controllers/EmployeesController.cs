using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using EmployeeCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly MVCDemoEbContext mVCDemoEbContext;

		public EmployeesController(MVCDemoEbContext mVCDemoEbContext)
		{
			this.mVCDemoEbContext = mVCDemoEbContext;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var employees = await mVCDemoEbContext.Employees.ToListAsync();
			return View(employees);
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

			await mVCDemoEbContext.Employees.AddAsync(employee);
			await mVCDemoEbContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}


	}
}
