using Dominio.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly Contexto contexto;

        public HomeController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> LimparBanco()
        {
            var colaboradores = await contexto.Colaborador
                .Include(x => x.Ferias)
                    .ThenInclude(x => x.PeriodosDeFerias)
                .Include(x => x.Ferias)
                    .ThenInclude(x => x.Homologacao)
                .ToListAsync();

            contexto.RemoveRange(colaboradores);
            await contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}