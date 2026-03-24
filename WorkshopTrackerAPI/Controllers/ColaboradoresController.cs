using SeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Data;


namespace SeuProjeto.Controllers
{
    [ApiController]
    [Route("colaboradores")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradoresController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET - Listar todos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colaboradores = await _context.Colaboradores
                .Include(c => c.WorkshopColaboradores)
                .ThenInclude(wc => wc.Workshop)
                .ToListAsync();

            return Ok(colaboradores);
        }

        // ✅ GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var colab = await _context.Colaboradores
                .Include(c => c.WorkshopColaboradores)
                .ThenInclude(wc => wc.Workshop)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (colab == null)
                return NotFound();

            return Ok(colab);
        }

        // ✅ POST - Criar
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColaboradorDTO dto)
        {
            var colab = new Colaborador
            {
                Nome = dto.Nome
            };

            _context.Colaboradores.Add(colab);
            await _context.SaveChangesAsync();

            return Ok(colab);
        }

        // ✅ PUT - Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ColaboradorDTO dto)
        {
            var colab = await _context.Colaboradores.FindAsync(id);

            if (colab == null)
                return NotFound();

            colab.Nome = dto.Nome;

            await _context.SaveChangesAsync();

            return Ok(colab);
        }

        // ✅ DELETE - Remover
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var colab = await _context.Colaboradores.FindAsync(id);

            if (colab == null)
                return NotFound();

            _context.Colaboradores.Remove(colab);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


