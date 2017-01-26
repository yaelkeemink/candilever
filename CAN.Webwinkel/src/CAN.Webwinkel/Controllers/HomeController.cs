using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Domain.Interfaces;
using CAN.Webwinkel.ViewModels.HomeViewModels;

namespace CAN.Webwinkel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArtikelService _artikelService;
        private readonly IWinkelwagenService _winkelmandjeservice;

        public HomeController(ILogger<HomeController> logger, 
            IArtikelService artikelService, 
            IWinkelwagenService winkelmandjeService)
        {
            _logger = logger;
            _artikelService = artikelService;
            _winkelmandjeservice = winkelmandjeService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", new { id = 1 });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Index(int id)
        {
            var aantalArtikelenPerPagina = 24;
            var artikelen = _artikelService.AlleArtikelenPerPagina(id, aantalArtikelenPerPagina)
                .Select(a => new ApiArtikelenViewModel(a))
                .ToList();
            int paginas = _artikelService.AantalPaginas(aantalArtikelenPerPagina);
            var viewModel = new ArtikelOverzichtViewModel()
            {
                Artikelen = artikelen,
                AantalPaginas = paginas,
                HuidigePaginanummer = id
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Registreren()
        {
            return View();
        }
        public IActionResult AlgemeneVoorwaarden()
        {
            return View();
        }

        public IActionResult WettelijkeTeksten()
        {
            return View();
        }

        public IActionResult ToonWinkelmandje(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index/1");
            }
            var viewModel = new WinkelmandjeViewModel(_winkelmandjeservice.FindWinkelmandje(id));
            return View(viewModel);
        }
        protected override void Dispose(bool disposing)
        {
            _artikelService?.Dispose();
            _winkelmandjeservice?.Dispose();
            base.Dispose(disposing);
        }
    }
}
