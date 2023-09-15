using AuviaTask.Data;
using AuviaTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuviaTask.Controllers
{
	public class JobsController : Controller
	{
		// Action for displaying the list of jobs on the left side of the Jobs page
		private readonly AppDBContext databaseMVC;
		public JobsController(AppDBContext DatabaseMVC)
		{
			databaseMVC = DatabaseMVC;
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddJobViewModel addJobViewReq)
		{
			var job = new Job()
			{
				Id = Guid.NewGuid(),
				Name = addJobViewReq.Name,
				Category = addJobViewReq.Category,
			};

			await databaseMVC.Jobs.AddAsync(job);
			await databaseMVC.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var jobs = await databaseMVC.Jobs.ToListAsync();
			return View(jobs);
		}


		[HttpGet]
		public async Task<IActionResult> View(Guid id)
		{
			var job = await databaseMVC.Jobs.FirstOrDefaultAsync(x => x.Id == id);
			if (job != null) 
			{
				var viewModel = new UpdateJobViewModel()
				{
					Id = job.Id,
					Name = job.Name,
					Category = job.Category,
				};

                return await Task.Run(() => View("View",viewModel));
            }
			

			return RedirectToAction("Index");
		}

        [HttpPost]
        public async Task<IActionResult> View(UpdateJobViewModel model)
        {
			var job = await databaseMVC.Jobs.FindAsync(model.Id);
			if (job != null)
			{
				job.Name = model.Name;
				job.Category = model.Category;
				await databaseMVC.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

		[HttpPost]
        public async Task<IActionResult> Delete(UpdateJobViewModel model)
        {
            var job = await databaseMVC.Jobs.FindAsync(model.Id);

            if (job != null)
            {
               
                databaseMVC.Jobs.Remove(job);
                await databaseMVC.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
