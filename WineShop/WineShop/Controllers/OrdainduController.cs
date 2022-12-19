using Microsoft.AspNetCore.Mvc;

namespace WineShop.Controllers
{
    using WineShop.Data;
    using WineShop.Models;
    using Microsoft.AspNetCore.Authorization;
    using WineShop.ViewModels;
    using Wineshop.Models;
    using Wineshop.Services;
    using Wineshop.ViewModels;

    [Authorize]
    public class OrdainduController : Controller   // Los servivcios los injectamos 
    {
        private readonly ISaskiaService _saskiaService;
        private readonly IArdoaService _ardoaService;

        public OrdainduController(ISaskiaService saskiaService, IArdoaService ardoaService)
        {
            _saskiaService = saskiaService;
            _ardoaService = ardoaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Abizena,Helbidea,Herrialdea,Hiria,Izena,Postakodea,Telefonoa")]
BezeroaEskaera bezeroaEskaera)
        {
            if (ModelState.IsValid)
            {
                /*Bezeroen datuak gorde*/
                bezeroaEskaera.Erabiltzailea = HttpContext.User.Identity.Name;
                bezeroaEskaera.Data = DateTime.Now;
                _saskiaService.EskaeraBezeroaGehitu(bezeroaEskaera);
                /*Eskaera gorde*/
                var cart = Saskia.SaskiaLortu(this.HttpContext);
                _saskiaService.EskaeraSortu(bezeroaEskaera, cart.SaskiaId);
                /*Beste pantaila batera berbideratzen da*/
                return RedirectToAction("Osatu", new
                {
                    bezeroa = bezeroaEskaera.Izena + " " + bezeroaEskaera.Abizena,
                    saskiaId = cart.SaskiaId
                });
            }
            return View(bezeroaEskaera);
        }
        public async Task<IActionResult> Osatu(string bezeroa, string saskiaId)
        {
            var osatuViewModel = new OsatuViewModel(); //ViewModel bat erabiliko dugu
            osatuViewModel.SaskiaAleak = await _saskiaService.SaskiaLortuAleak(saskiaId);
            osatuViewModel.SaskiaId = saskiaId;
            osatuViewModel.Bezeroa = bezeroa;

            IList<SaskiaAlea> saskiaAleaList = new List<SaskiaAlea>();
            saskiaAleaList = await _saskiaService.SaskiaLortuAleak(saskiaId);
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
            osatuViewModel.SaskiaAleaVMList = saskiaAleaVMList;





            return View(osatuViewModel);
        }

    }
}

