using Microsoft.AspNetCore.Mvc;

namespace Flow.Controllers
{
	public class MusicsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
