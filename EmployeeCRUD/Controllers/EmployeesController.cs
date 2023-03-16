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

		[HttpGet]
		public async  Task<IActionResult> View(Guid id)
		{
			var employee = await mVCDemoEbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

			if(employee != null)
			{
				var viewModel = new UpdateEmployeeVM()
				{
					Id = employee.Id,
					Name = employee.Name,
					Email = employee.Email,
					Salary = employee.Salary,
					DateOfBirth = employee.DateOfBirth,
					Department = employee.Department
				};
				return await Task.Run(() =>View("View", viewModel));
			}

			
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> View(UpdateEmployeeVM model)
		{
			var employee = await mVCDemoEbContext.Employees.FindAsync(model.Id);

			if(employee != null)
			{
				employee.Name = model.Name;
				employee.Email = model.Email;
				employee.Salary = model.Salary;
				employee.DateOfBirth = model.DateOfBirth;
				employee.Department = model.Department;
			}
			await mVCDemoEbContext.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UpdateEmployeeVM model)
		{
			var employee = await mVCDemoEbContext.Employees.FindAsync(model.Id);

			if(employee != null)
			{
				mVCDemoEbContext.Employees.Remove(employee);

				await mVCDemoEbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

	}
}
