﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Site_Vendinha_InterFocus.Models;
using System.Text;

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
        public async Task<ActionResult> DetailsAsync(Guid id)
        {
            var dividaJson = await Request.GetStringAsync($"api/Dividas/{id.ToString()}");
            var divida = JsonConvert.DeserializeObject<Divida>(dividaJson);
            return View(divida);
        }

        // GET: DividaController/Create
        public async Task<ActionResult> CreateAsync(Guid? clienteId)
        {
            var clienteJson = await Request.GetStringAsync("api/Clientes");
            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(clienteJson);


            if (clienteId == null)
                ViewBag.Clientes = clientes.Select(c => new SelectListItem { Text = c.ClienteName, Value = c.ClienteId.ToString()});
            else
                ViewBag.Clientes = clientes.Where(cl => cl.ClienteId == clienteId).Select(c => new SelectListItem { Text = c.ClienteName, Value = c.ClienteId.ToString() });

            var divida = new Divida() { 
                DividaId = Guid.NewGuid(),
                DataDeCriacao = DateTime.Now,
                DataDePagamento = DateTime.Now.AddDays(30)
            };
            return View(divida);
        }

        // POST: DividaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Divida divida)
        {
            try
            {
                var DividaJson = JsonConvert.SerializeObject(divida);
                var requestContent = new StringContent(DividaJson, Encoding.UTF8, "application/json");
                var DividasJson = await Request.GetStringAsync("api/Dividas");
                var Dividas = JsonConvert.DeserializeObject<List<Divida>>(DividasJson);

                var TemDividasEmAberto = Dividas.Where(d => d.ClienteId == divida.ClienteId && d.EstaPaga == false).ToList();
                if (TemDividasEmAberto.Count > 0)
                    return RedirectToAction(nameof(Index));

                await Request.PostAsync("api/Dividas/", requestContent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DividaController/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var dividaJson = await Request.GetStringAsync($"api/Dividas/{id.ToString()}");
            var divida = JsonConvert.DeserializeObject<Divida>(dividaJson);
            return View(divida);
        }

        // POST: DividaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid id, Divida divida)
        {
            try
            {
                var DividaJson = JsonConvert.SerializeObject(divida);
                var requestContent = new StringContent(DividaJson, Encoding.UTF8, "application/json");
                await Request.PutAsync($"api/Dividas/{divida.DividaId}", requestContent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DividaController/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var dividaJson = await Request.GetStringAsync($"api/Dividas/{id.ToString()}");
            var divida = JsonConvert.DeserializeObject<Divida>(dividaJson);
            return View(divida);
        }

        // POST: DividaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Guid id, Divida divida)
        {
            try
            {
                await Request.DeleteAsync($"api/Dividas/{id}");
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

            var DividasJson = await Request.GetStringAsync("api/Dividas");
            var Dividas = JsonConvert.DeserializeObject<List<Divida>>(DividasJson);
            Dividas.ForEach(d => d.nomeCliente = clientes.FirstOrDefault(c => c.ClienteId == d.ClienteId).ClienteName);
            return View(Dividas);
        }
    }
}
