using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cuidados.Caninos.Marcos.Montiel.Models;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
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
        public async Task<IActionResult> Index(string reporte, string valuesOrder, string searchPerson)
        {
            try
            {
                var persona = from s in _context.ComPersona select s;
                persona = persona.Include(c => c.ComCatEscolaridad).Include(c => c.ComCatSexo);

                // Generar reporte PDF
                ViewData["GenerateReports"] = "reporte";

                if (!String.IsNullOrEmpty(reporte))
                {
                    // Creamos el documento PDF con el formato de hoja A4, y Rotate() para ponerla horizontal.
                    Document doc = new Document(PageSize.A4.Rotate());
                    // Indicamos la ruta donde se va a guardar el documento.
                    PdfWriter writer = PdfWriter.GetInstance(doc,
                            new FileStream(@"/Users/marcosmontiel/Desktop/personas.pdf", FileMode.Create));
                    // Colocamos el titulo y autor del documento (Esto no será visible en el documento).
                    doc.AddTitle("Reporte - Personas");
                    doc.AddCreator("Marcos Gabriel Cruz Montiel");
                    // Abrimos el documento.
                    doc.Open();
                    // Creamos el tipo de fuente, tamaño, estilo y color que vamos a utilizar.
                    Font fontPrincipal = new Font(Font.HELVETICA, 12, Font.BOLD, BaseColor.White);
                    Font fontBody = new Font(Font.HELVETICA, 12, Font.NORMAL, BaseColor.Black);
                    // Añadimos el encabezado del documento.
                    doc.Add(new Paragraph("Reporte - Personas"));
                    doc.Add(Chunk.Newline);
                    // Creamos la tabla que contendrá los registro de la tabla ComPersona.
                    PdfPTable tblPersona = new PdfPTable(7)
                    {
                        WidthPercentage = 100
                    };
                    // Configuramos el titulo de las columnas.
                    PdfPCell colNombre = new PdfPCell(new Phrase("Nombre", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPCell colAPaterno = new PdfPCell(new Phrase("A. Paterno", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPCell colAMaterno = new PdfPCell(new Phrase("A. Materno", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPCell colGenero = new PdfPCell(new Phrase("Género", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPCell colFechaNac = new PdfPCell(new Phrase("Fecha de Nac.", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPCell colCurp = new PdfPCell(new Phrase("C.U.R.P", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    PdfPCell colEscolaridad = new PdfPCell(new Phrase("Escolaridad", fontPrincipal))
                    {
                        BorderWidth = 0,
                        BorderColor = WebColors.GetRgbColor("#009432"),
                        BackgroundColor = WebColors.GetRgbColor("#009432"),
                        PaddingTop = 10,
                        PaddingRight = 0,
                        PaddingBottom = 13,
                        PaddingLeft = 15,
                        VerticalAlignment = Element.ALIGN_CENTER
                    };

                    // Añadimos las celdas a la tabla.
                    tblPersona.AddCell(colNombre);
                    tblPersona.AddCell(colAPaterno);
                    tblPersona.AddCell(colAMaterno);
                    tblPersona.AddCell(colGenero);
                    tblPersona.AddCell(colFechaNac);
                    tblPersona.AddCell(colCurp);
                    tblPersona.AddCell(colEscolaridad);

                    // Llenamos la tabla de información.
                    foreach (var item in persona){
                        colNombre = new PdfPCell(new Phrase(item.Nombre, fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        colAPaterno = new PdfPCell(new Phrase(item.APaterno, fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        colAMaterno = new PdfPCell(new Phrase(item.AMaterno, fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        colGenero = new PdfPCell(new Phrase(item.ComCatSexo.Nombre, fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        colFechaNac = new PdfPCell(new Phrase(item.FechaNac.ToShortDateString(), fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        colCurp = new PdfPCell(new Phrase(item.Curp, fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        colEscolaridad = new PdfPCell(new Phrase(item.ComCatEscolaridad.Nombre, fontBody))
                        {
                            BorderWidth = 0,
                            BorderWidthBottom = 0.75f
                        };

                        // Añadimos las celdas a la tabla.
                        tblPersona.AddCell(colNombre);
                        tblPersona.AddCell(colAPaterno);
                        tblPersona.AddCell(colAMaterno);
                        tblPersona.AddCell(colGenero);
                        tblPersona.AddCell(colFechaNac);
                        tblPersona.AddCell(colCurp);
                        tblPersona.AddCell(colEscolaridad);
                    }

                    // Añadimos la tabla al documento.
                    doc.Add(tblPersona);

                    doc.Close();
                    writer.Close();
                }

                // Ordenar valores desc y asc en la tabla
                ViewData["NameOrderAscDesc"] = String.IsNullOrEmpty(valuesOrder) ? "nombre_desc" : "";
                ViewData["APatOrderAscDesc"] = valuesOrder == "paterno_asc" ? "paterno_desc" : "paterno_asc";
                ViewData["AMatOrderAscDesc"] = valuesOrder == "materno_asc" ? "materno_desc" : "materno_asc";

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
                ViewData["CurrentFilter"] = searchPerson;

                if (!String.IsNullOrEmpty(searchPerson))
                {
                    persona = persona.Where(s => s.Nombre.Contains(searchPerson) || s.APaterno.Contains(searchPerson));
                }

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
            try
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

        // GET: ComPersona/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["FKComCatEscolaridad"] = new SelectList(_context.ComCatEscolaridad, "ID", "Nombre");
                ViewData["FKComCatSexo"] = new SelectList(_context.ComCatSexo, "ID", "Nombre");
                return View();
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

        // POST: ComPersona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,APaterno,AMaterno,Curp,FechaNac,FKComCatSexo,FKComCatEscolaridad")] ComPersona comPersona)
        {
            try
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

        // GET: ComPersona/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
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

        // POST: ComPersona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,APaterno,AMaterno,Curp,FechaNac,FKComCatSexo,FKComCatEscolaridad")] ComPersona comPersona)
        {
            try
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

        // GET: ComPersona/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
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

        // POST: ComPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var comPersona = await _context.ComPersona.SingleOrDefaultAsync(m => m.ID == id);
                _context.ComPersona.Remove(comPersona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        private bool ComPersonaExists(int id)
        {
            return _context.ComPersona.Any(e => e.ID == id);
        }
    }
}