using System;
using System.Linq;
using System.Threading.Tasks;
using Cuidados.Caninos.Marcos.Montiel.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Cuidados.Caninos.Marcos.Montiel.Controllers
{
    public class ComPersonaController : Controller
    {
        private readonly CCContext _context;

        public ComPersonaController(CCContext context)
        {
            _context = context;
        }

        // GET: ComPersona
        public async Task<IActionResult> Index(string valuesOrder, string searchPerson)
        {
            try
            {
                ViewData["CurrentFilter"] = searchPerson;
                ViewData["NameOrderAscDesc"] = String.IsNullOrEmpty(valuesOrder) ? "nombre_desc" : "";
                ViewData["APatOrderAscDesc"] = valuesOrder == "paterno_asc" ? "paterno_desc" : "paterno_asc";
                ViewData["AMatOrderAscDesc"] = valuesOrder == "materno_asc" ? "materno_desc" : "materno_asc";

                var persona = from s in _context.ComPersona select s;

                // Ordenar valores desc y asc en la tabla
                switch (valuesOrder)
                {
                    case "nombre_desc":
                        persona = persona.OrderByDescending(s => s.Nombre);
                        break;
                    case "paterno_desc":
                        persona = persona.OrderByDescending(s => s.APaterno);
                        break;
                    case "paterno_asc":
                        persona = persona.OrderBy(s => s.APaterno);
                        break;
                    case "materno_desc":
                        persona = persona.OrderByDescending(s => s.AMaterno);
                        break;
                    case "materno_asc":
                        persona = persona.OrderBy(s => s.AMaterno);
                        break;
                    default:
                        persona = persona.OrderBy(s => s.Nombre);
                        break;
                }

                // Caja de búsqueda
                if (!String.IsNullOrEmpty(searchPerson))
                {
                    persona = persona.Where(s => s.Nombre.Contains(searchPerson) || s.APaterno.Contains(searchPerson));
                }

                persona = persona.Include(c => c.ComCatEscolaridad).Include(c => c.ComCatSexo);
                return View(await persona.AsNoTracking().ToListAsync());
            }
            catch (Exception ex)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Send", "marcosmontiel.excepciones@gmail.com"));
                message.To.Add(new MailboxAddress("Reception", "marcos-gab14@hotmail.com"));
                message.Subject = "Exceptions";
                message.Body = new TextPart("plain")
                {
                    Text = "Excepción encontrada: " + ex.StackTrace
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("marcosmontiel.excepciones@gmail.com", "PruebaExcepciones123");
                    client.Send(message);
                    client.Disconnect(true);
                }

                return null;
            }
        }

        // GET: ComPersona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comPersona = await _context.ComPersona
                .Include(c => c.ComCatEscolaridad)
                .Include(c => c.ComCatSexo)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (comPersona == null)
            {
                return NotFound();
            }

            return View(comPersona);
        }

        // GET: ComPersona/Create
        public IActionResult Create()
        {
            ViewData["FKComCatEscolaridad"] = new SelectList(_context.ComCatEscolaridad, "ID", "Nombre");
            ViewData["FKComCatSexo"] = new SelectList(_context.ComCatSexo, "ID", "Nombre");
            return View();
        }

        // POST: ComPersona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,APaterno,AMaterno,Curp,FechaNac,FKComCatSexo,FKComCatEscolaridad")] ComPersona comPersona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comPersona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKComCatEscolaridad"] = new SelectList(_context.ComCatEscolaridad, "ID", "Nombre", comPersona.FKComCatEscolaridad);
            ViewData["FKComCatSexo"] = new SelectList(_context.ComCatSexo, "ID", "Nombre", comPersona.FKComCatSexo);
            return View(comPersona);
        }

        // GET: ComPersona/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comPersona = await _context.ComPersona.SingleOrDefaultAsync(m => m.ID == id);
            if (comPersona == null)
            {
                return NotFound();
            }
            ViewData["FKComCatEscolaridad"] = new SelectList(_context.ComCatEscolaridad, "ID", "Nombre", comPersona.FKComCatEscolaridad);
            ViewData["FKComCatSexo"] = new SelectList(_context.ComCatSexo, "ID", "Nombre", comPersona.FKComCatSexo);
            return View(comPersona);
        }

        // POST: ComPersona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,APaterno,AMaterno,Curp,FechaNac,FKComCatSexo,FKComCatEscolaridad")] ComPersona comPersona)
        {
            if (id != comPersona.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comPersona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComPersonaExists(comPersona.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKComCatEscolaridad"] = new SelectList(_context.ComCatEscolaridad, "ID", "Nombre", comPersona.FKComCatEscolaridad);
            ViewData["FKComCatSexo"] = new SelectList(_context.ComCatSexo, "ID", "Nombre", comPersona.FKComCatSexo);
            return View(comPersona);
        }

        // GET: ComPersona/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comPersona = await _context.ComPersona
                .Include(c => c.ComCatEscolaridad)
                .Include(c => c.ComCatSexo)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (comPersona == null)
            {
                return NotFound();
            }

            return View(comPersona);
        }

        // POST: ComPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comPersona = await _context.ComPersona.SingleOrDefaultAsync(m => m.ID == id);
            _context.ComPersona.Remove(comPersona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComPersonaExists(int id)
        {
            return _context.ComPersona.Any(e => e.ID == id);
        }
    }
}
