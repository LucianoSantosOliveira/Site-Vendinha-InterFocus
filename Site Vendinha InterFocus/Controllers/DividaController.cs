using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site_Vendinha_InterFocus.Models;

namespace Site_Vendinha_InterFocus.Controllers
{
    public class DividaController : Controller
    {
        private readonly HttpClient Request;

        public DividaController()
        {
            Request = new HttpClient();
            Request.BaseAddress = new Uri("https://localhost:7200/");
        }
        // GET: DividaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DividaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DividaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DividaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DividaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DividaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DividaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DividaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult List()
        {
            return View(new List<Divida>() { });
        }
    }
}
