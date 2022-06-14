using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Servientrega.Data.Models;
using Servientrega.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servientrega.Controllers
{
    public class AvionController : Controller
    {
        private readonly ILogger<AvionController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string _uri;

        public AvionController(ILogger<AvionController> logger,
                              IConfiguration configuration,
                              IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _uri = _configuration.GetValue<string>("UrlApi");
        }

        public async Task<IActionResult> IndexAsync()
        {
            AvionViewModel model = new();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var respuesta = await httpClient.GetAsync(_uri);
                    if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var response = await respuesta.Content.ReadAsStringAsync();
                        await httpClient.GetStringAsync(_uri);
                        var listAvion = JsonSerializer.Deserialize<List<Avion>>(response,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        model.ListAvion = listAvion;
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AvionViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    var respuesta = await httpClient.PostAsJsonAsync(_uri, _mapper.Map<Avion>(model));

                    if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var cuerpo = await respuesta.Content.ReadAsStringAsync();
                        var erroresDelAPI = Util.Util.ExtraerErroresDelWebAPI(cuerpo);
                        List<string> ListError = new();
                        foreach (var campoErrores in erroresDelAPI)
                        {
                            string bodyError = string.Empty;
                            bodyError += $"-{campoErrores.Key}";
                            foreach (var error in campoErrores.Value)
                                bodyError += $"-{error}";
                            ListError.Add(bodyError);
                        }
                        TempData["message"] = ListError.ToString();
                    }
                    TempData["message"] = "Se registro correctamente la Avion";

                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return View();
        }

        public async Task<IActionResult> Update(Guid id)
        {
            AvionViewModel model = new();
            using (var httpClient = new HttpClient())
            {
                using var respuesta = await httpClient.GetAsync(_uri);
                if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var response = await respuesta.Content.ReadAsStringAsync();
                    await httpClient.GetStringAsync(_uri);
                    var listAvion = JsonSerializer.Deserialize<List<Avion>>(response,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    model = _mapper.Map<AvionViewModel>(listAvion.Where(x => x.Id.Equals(id)).FirstOrDefault());
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(AvionViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    await httpClient.PutAsJsonAsync($"{_uri}/{model.Id}", _mapper.Map<Avion>(model));
                    TempData["message"] = "Se actualizo correctamente la Avion";
                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await httpClient.DeleteAsync($"{_uri}/{id}");
                    TempData["message"] = "Se Elimino correctamente la Avion";
                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return RedirectToAction("Index");
        }
    }
}
