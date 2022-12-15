using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wineshop.Models;
using Wineshop.Services;
using Wineshop.ViewModels;

namespace Wineshop.Controllers
{
    public class SaskiaController : Controller
    {
        private readonly ISaskiaService _saskiaService;
        private readonly IArdoaService _ardoaService;
        public SaskiaController(ISaskiaService saskiaService, IArdoaService ardoaService)
        {
            _saskiaService = saskiaService;
            _ardoaService = ardoaService;
        }
        public async Task<IActionResult> Index(string id)
        {
            IList<SaskiaAlea> saskiaAleaList = new List<SaskiaAlea>();
            saskiaAleaList = await _saskiaService.SaskiaLortuAleak(id);

            //Ardo bakoitzaren datuak hartu eta ViewModel bezala sortu
            IList<SaskiaAleaViewModel> saskiaAleaVMList = new List<SaskiaAleaViewModel>();
            foreach (var saskiaAlea in saskiaAleaList)
            {
                var ardoa = await _ardoaService.GetArdoa(saskiaAlea.ArdoaId);
                SaskiaAleaViewModel saskiaAleaViewModel = new SaskiaAleaViewModel()
                {
                    ArdoaId = ardoa.Id,
                    Irudia = ardoa.Irudia,
                    Izena = ardoa.Izena,
                    Kantitatea = saskiaAlea.Kantitatea,
                    Salneurria = ardoa.Salneurria
                };
                saskiaAleaVMList.Add(saskiaAleaViewModel);
            }
            var saskiaViewModel = new SaskiaViewModel(); //Bistaratuko dugun ViewModel osoa
            saskiaViewModel.SaskiaAleaVMList = saskiaAleaVMList;
            return View(saskiaViewModel);

        }
        public async Task<IActionResult> SaskiaGehitu(int id)
        {
            var cart = Saskia.SaskiaLortu(this.HttpContext); //aurretik sortu dugun Saskia klasea erabiliz
            await _saskiaService.SaskiaGehitu(id, cart.SaskiaId); //zerbitzu berrian karritoan gehitzeko
            return RedirectToAction("Index", new { id = cart.SaskiaId });
        }
        public async Task<IActionResult> SaskiaGehituAjax(int id, int kantitatea, float salneurria, float guztira)
        {
            var cart = Saskia.SaskiaLortu(this.HttpContext);
            await _saskiaService.SaskiaGehitu(id, cart.SaskiaId);
            kantitatea++;
            var results = new
            {
                mezua = "Zure saskia eguneratu da",
                kantitatea = kantitatea,
                salneurria = salneurria,
                guztira = guztira
            };
            return Json(results);
        }
        public async Task<IActionResult> SaskiaKendu(int id)
        {
            var cart = Saskia.SaskiaLortu(this.HttpContext); //aurretik sortu dugun Saskia klasea erabiliz
            await _saskiaService.SaskiaKendu(id, cart.SaskiaId); //zerbitzu berrian karritoan gehitzeko
            return RedirectToAction("Index", new { id = cart.SaskiaId });
        }
        public async Task<IActionResult> SaskiaKenduAjax(int id, int kantitatea, float salneurria, float guztira)
        {
            var cart = Saskia.SaskiaLortu(this.HttpContext);
            await _saskiaService.SaskiaKendu(id, cart.SaskiaId);
            kantitatea--;
            var results = new
            {
                mezua = "Zure saskia eguneratu da",
                kantitatea = kantitatea,
                salneurria = salneurria,
                guztira = guztira
            };
            return Json(results);
        }
    }
}

