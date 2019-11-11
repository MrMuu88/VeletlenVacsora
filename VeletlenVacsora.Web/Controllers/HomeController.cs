using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeletlenVacsora.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VeletlenVacsora.Web.Controllers {
	public class HomeController : Controller {

		private readonly IVacsoraRepository repository;


		public HomeController(IVacsoraRepository repo) {
			repository = repo;
		}

		// GET: /<controller>/
		public async Task<IActionResult> Index() {
			var Recepies = await repository.GetAllRecepiesAsync();
			return View(Recepies);
		}
	}
}
