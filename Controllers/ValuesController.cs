using ConsoleApp4;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly UtentiContext _context;

        public ValuesController(UtentiContext context)
        {
            _context = context;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<DatiUtente> Get()
        {
            return _context.DatiUtentes.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{CodiceFiscale}")]
        public IActionResult Get(string codiceFiscale)
        {
            DatiUtente utenteDaRicercare = _context.DatiUtentes.Find(codiceFiscale);

            if(utenteDaRicercare == null)
            {
                return NotFound($"Utente con codice fiscale {codiceFiscale} non trovato");
            } else
            {
                return Ok(utenteDaRicercare);
            }
        }

        // Metodo per trovare DATA DI NASCITA, SESSO, COMUNE tramite un CF
        [HttpGet("Alternativo/{codiceFiscale}")]
        public IActionResult Get_Alternativo(string codiceFiscale)
        {
            try
            {
                CodiceFiscale cf = new CodiceFiscale();
                CodiceFiscale risultato = cf.CalcolaDatiDaCF(codiceFiscale);
                return Ok(risultato);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] DatiUtente datiUtente)
        {
            try
            {

                datiUtente.CodiceFiscale = CodiceFiscale.calcoloCF(datiUtente.Nome, datiUtente.Cognome, datiUtente.DataDiNascita, datiUtente.Sesso, datiUtente.Comune);
                _context.Add(datiUtente);
                _context.SaveChanges();
                return Ok("Utente aggiunto correttamente");

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // PUT api/<ValuesController>/5
        [HttpPut("{codiceFiscale}")]
        public IActionResult Put(string codiceFiscale, [FromBody] DatiUtente datiUtente)
        {
            DatiUtente utenteDaRicercare = _context.DatiUtentes.Find(codiceFiscale);

            if (utenteDaRicercare == null)
            {
                return NotFound($"Utente con codice fiscale {codiceFiscale} non trovato");
            }
            else
            {
                try
                {
                    utenteDaRicercare.Nome = datiUtente.Nome;
                    utenteDaRicercare.Cognome = datiUtente.Cognome;
                    utenteDaRicercare.Sesso = datiUtente.Sesso;
                    utenteDaRicercare.DataDiNascita = datiUtente.DataDiNascita;
                    utenteDaRicercare.Comune = datiUtente.Comune;

                    utenteDaRicercare.CodiceFiscale = CodiceFiscale.calcoloCF(utenteDaRicercare.Nome, utenteDaRicercare.Cognome, utenteDaRicercare.DataDiNascita, utenteDaRicercare.Sesso, utenteDaRicercare.Comune);

                    _context.SaveChanges();
                    return Ok($"Utente con codice fiscale {codiceFiscale} modificato correttamente");
                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{codiceFiscale}")]
        public IActionResult Delete(string codiceFiscale)
        {
            DatiUtente utenteDaEliminare = _context.DatiUtentes.Find(codiceFiscale);

            if(utenteDaEliminare == null)
            {
                return NotFound($"Utente con ID {codiceFiscale} non trovato");
            }
            else
            {
                try
                {
                    _context.Remove(utenteDaEliminare);
                    _context.SaveChanges();
                    return Ok($"Utente con ID {codiceFiscale} rimosso correttamente");
                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }              
            }
        }
    }
}
