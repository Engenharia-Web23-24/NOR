using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOR.Data;
using NOR.Models;

namespace NOR.Controllers
{
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Client)] // a "duração" não é requerida no teste mas temos de indicar um valor
    public class RegistosController : Controller
    {
        private readonly NORContext _context;

        public RegistosController(NORContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Lista()
        {
              return View(await _context.RegistoUtilizador.ToListAsync());
        }

        public async Task<IActionResult> Valida(int? id)
        {
            if(id.HasValue)
            {
                RegistoUtilizador alterado = await _context.RegistoUtilizador.FindAsync(id);
                if(alterado != null && alterado.Valido == false) // esta verificação não é necessária 
                {
                    alterado.Valido = true;
                    _context.RegistoUtilizador.Update(alterado);
                    await _context.SaveChangesAsync();
                }
            }

            return PartialView("ListaParcial", await _context.RegistoUtilizador.ToListAsync());
        }

        public async Task<IActionResult> InValida(int? id)
        {
            if (id.HasValue)
            {
                RegistoUtilizador alterado = await _context.RegistoUtilizador.FindAsync(id);
                if (alterado != null && alterado.Valido == true) // esta verificação não é necessária 
                {
                    alterado.Valido = false;
                    _context.RegistoUtilizador.Update(alterado);
                    await _context.SaveChangesAsync();
                }
            }

            return PartialView("ListaParcial", await _context.RegistoUtilizador.ToListAsync());
        }

        public IActionResult Adiciona()
        {
            List<SelectListItem> lista = new List<SelectListItem>
            {
                new SelectListItem() { Value="", Text="--- Escolha um valor ---", Selected = true}, // esta opção não vem exigida no enunciado
                new SelectListItem() { Value="Ordinário", Text="Ordinário"},
                new SelectListItem() { Value="Especial", Text="Especial"},
                new SelectListItem() { Value="Super", Text="Super"}
            };

            ViewBag.opcoesRegime = lista;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adiciona([Bind("RegistoId,Nome,Regime,Valido")] RegistoUtilizador registoUtilizador)
        {
            // verificar se o campo Regime está bem preenchido. É uma alternativa à DataAnnotation usada no model
            if(Regex.IsMatch(registoUtilizador.Regime, "(Ordinário|Especial|Super)") == false)
            {
                ModelState.AddModelError("Regime", "Apenas pode usar um dos valores (Ordinário|Especial|Super)");
            }
            if (ModelState.IsValid)
            {
                _context.Add(registoUtilizador);
                await _context.SaveChangesAsync();

                TempData["msgOK"] = "Utilizador registado com sucesso.";

                return RedirectToAction(nameof(Lista));
            }
            List<SelectListItem> lista = new List<SelectListItem>
            {
                new SelectListItem() { Value="", Text="--- Escolha um valor ---", Selected = true},  // esta opção não vem exigida no enunciado
                new SelectListItem() { Value="Ordinário", Text="Ordinário"},
                new SelectListItem() { Value="Especial", Text="Especial"},
                new SelectListItem() { Value="Super", Text="Super"}
            };

            ViewBag.opcoesRegime = lista;
            return View(registoUtilizador);
        }
    }
}
