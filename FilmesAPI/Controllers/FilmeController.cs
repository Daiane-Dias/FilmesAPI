using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    // realizado pois não tinhamos a conexão com o banco sql
    //private static List<Filme> filmes = new List<Filme>();
    //private static int id = 0;

    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {

        //filme.Id = id++;
        //filmes.Add(filme);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), 
            new { id = filme.Id}, 
            filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery]int skip=0, [FromQuery] int take=50)
    {
        return _context.Filmes.Skip(skip).Take(take);//filmes.Skip(skip).Take(take);
    }
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
       var filme =_context.Filmes.FirstOrDefault(filme => filme.Id == id); //filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
}
