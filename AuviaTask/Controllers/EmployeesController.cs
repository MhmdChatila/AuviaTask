using AuviaTask.Data;
using AuviaTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuviaTask.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDBContext databaseMVC;

        public EmployeesController(AppDBContext DatabaseMVC)
        {
            databaseMVC = DatabaseMVC;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await databaseMVC.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequests) 
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequests.Name,
                BirthDate = addEmployeeRequests.BirthDate,
                Phone = addEmployeeRequests.Phone,
                EmploymentDate = addEmployeeRequests.EmploymentDate,
                PersonalPhoto = addEmployeeRequests.PersonalPhoto,
                Governorate = addEmployeeRequests.Governorate,
                IsProbation = addEmployeeRequests.IsProbation,
                IsDeleted = addEmployeeRequests.IsDeleted,
            };

            await databaseMVC.Employees.AddAsync(employee);
            await databaseMVC.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await databaseMVC.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    BirthDate = employee.BirthDate,
                    Phone = employee.Phone,
                    EmploymentDate = employee.EmploymentDate,
                    PersonalPhoto = employee.PersonalPhoto,
                    Governorate = employee.Governorate,
                    IsProbation = employee.IsProbation,
                    IsDeleted = employee.IsDeleted,
                };
                return await Task.Run(() => View("View", viewModel)) ;

            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await databaseMVC.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.BirthDate = model.BirthDate;
                employee.Phone = model.Phone;
                employee.EmploymentDate = model.EmploymentDate;
                employee.PersonalPhoto = model.PersonalPhoto;
                employee.Governorate = model.Governorate;
                employee.IsProbation = model.IsProbation;
                employee.IsDeleted = model.IsDeleted;

                await databaseMVC.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await databaseMVC.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                databaseMVC.Employees.Remove(employee);
                await databaseMVC.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
