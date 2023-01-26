using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site_Vendinha_InterFocus.Models;
using System.Security.Policy;
using System.Text;

namespace Site_Vendinha_InterFocus.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient Request;
        // GET: ClienteController

        public ClienteController()
        {
            Request = new HttpClient();
            Request.BaseAddress = new Uri("https://localhost:7200/");
        }

        // GET: ClienteController/Details/5
        public async Task<ActionResult> DetailsAsync(Guid id)
        {
            var clienteJson = await Request.GetStringAsync($"api/Clientes/{id.ToString()}");
            var cliente = JsonConvert.DeserializeObject<Cliente>(clienteJson);
            return View(cliente);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            var cliente = new Cliente() {ClienteId = Guid.NewGuid() };
            return View(cliente);
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateClienteAsync(Cliente cliente)
        {
            try
            {
                var ClienteJson = JsonConvert.SerializeObject(cliente);
                var requestContent = new StringContent(ClienteJson ,Encoding.UTF8, "application/json");
                await Request.PostAsync("api/Clientes/", requestContent);
                return RedirectToAction(nameof(ListAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var clienteJson = await Request.GetStringAsync($"api/Clientes/{id.ToString()}");
            var cliente = JsonConvert.DeserializeObject<Cliente>(clienteJson);
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid id, Cliente cliente)
        {
            try
            {
                var ClienteJson = JsonConvert.SerializeObject(cliente);
                var requestContent = new StringContent(ClienteJson, Encoding.UTF8, "application/json");
                await Request.PutAsync($"api/Clientes/{cliente.ClienteId}", requestContent);
                return RedirectToAction(nameof(ListAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var clienteJson = await Request.GetStringAsync($"api/Clientes/{id.ToString()}");
            var cliente = JsonConvert.DeserializeObject<Cliente>(clienteJson);
            return View(cliente);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Guid id, Cliente cliente)
        {
            try
            {
                await Request.DeleteAsync($"api/Clientes/{id}");
                return RedirectToAction(nameof(ListAsync));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ListAsync()
        {
            var clienteJson = await Request.GetStringAsync("api/Clientes");
            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(clienteJson);
            return View(clientes);
        }
    }
}
