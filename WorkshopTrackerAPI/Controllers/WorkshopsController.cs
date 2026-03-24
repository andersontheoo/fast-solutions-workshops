/*    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    //using Microsoft.AspNetCore.Authorization;
    using SeuProjeto.Models;
    using SeuProjeto.Data;


    namespace SeuProjeto.Controllers
    {
    //[Authorize]
    [Route("api/workshops")]
    [ApiController]

    public class WorkshopsController : ControllerBase
        {
        private readonly AppDbContext _context;

        public WorkshopsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET - Listar todos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var workshops = await _context.Workshops
                .Include(w => w.WorkshopColaboradores)
                .ThenInclude(wc => wc.Colaborador)
                .ToListAsync();

            return Ok(workshops);
        }

        // ✅ GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workshop = await _context.Workshops
                .Include(w => w.WorkshopColaboradores)
                .ThenInclude(wc => wc.Colaborador)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null)
                return NotFound();

            return Ok(workshop);
        }

        // ✅ POST - Criar
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkshopDTO dto)
        {
        var workshop = new Workshop
        {
        Nome = dto.Nome,
        DataRealizacao = dto.DataRealizacao,
        Descricao = dto.Descricao,
        WorkshopColaboradores = new List<WorkshopColaborador>()
        };

        if (dto.ColaboradoresIds != null)
        {
            foreach (var colabId in dto.ColaboradoresIds)
            {
                var colaborador = await _context.Colaboradores.FindAsync(colabId);

            if (colaborador == null)
                return BadRequest($"Colaborador com ID {colabId} não existe.");

            workshop.WorkshopColaboradores.Add(new WorkshopColaborador
            {
                ColaboradorId = colabId,
                Colaborador = colaborador
            });
        }
    }

    _context.Workshops.Add(workshop);
    await _context.SaveChangesAsync();

    return Ok(workshop);
}

        // ✅ PUT - Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkshopDTO dto)
        {
            var workshop = await _context.Workshops
                .Include(w => w.WorkshopColaboradores)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null)
                return NotFound();

            workshop.Nome = dto.Nome;
            workshop.DataRealizacao = dto.DataRealizacao;
            workshop.Descricao = dto.Descricao;

            // limpa relações antigas
            workshop.WorkshopColaboradores.Clear();

            // adiciona novas
            if (dto.ColaboradoresIds != null)
            {
                foreach (var colabId in dto.ColaboradoresIds)
                {
                    workshop.WorkshopColaboradores.Add(new WorkshopColaborador
                    {
                        WorkshopId = id,
                        ColaboradorId = colabId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(workshop);
        }

        // ✅ DELETE - Remover
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);

            if (workshop == null)
                return NotFound();

            _context.Workshops.Remove(workshop);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
    } 
*/



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;
using SeuProjeto.Data;
using SeuProjeto.DTOs;

namespace SeuProjeto.Controllers
{
    [Route("api/workshops")]
    [ApiController]
    public class WorkshopsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkshopsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET - Listar todos (mostrando nome e ID dos colaboradores)
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var workshops = await _context.Workshops
                .Include(w => w.WorkshopColaboradores)
                    .ThenInclude(wc => wc.Colaborador)
                .Select(w => new WorkshopResponseDTO
                {
                    Id = w.Id,
                    Nome = w.Nome,
                    Descricao = w.Descricao,
                    DataRealizacao = w.DataRealizacao,
                    Colaboradores = w.WorkshopColaboradores.Select(wc => new ColaboradorInfoDTO
                    {
                        Id = wc.Colaborador.Id,
                        Nome = wc.Colaborador.Nome
                    }).ToList()
                })
                .ToListAsync();

            return Ok(workshops);
        }

        // ✅ GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workshop = await _context.Workshops
                .Include(w => w.WorkshopColaboradores)
                    .ThenInclude(wc => wc.Colaborador)
                .Where(w => w.Id == id)
                .Select(w => new WorkshopResponseDTO
                {
                    Id = w.Id,
                    Nome = w.Nome,
                    Descricao = w.Descricao,
                    DataRealizacao = w.DataRealizacao,
                    Colaboradores = w.WorkshopColaboradores.Select(wc => new ColaboradorInfoDTO
                    {
                        Id = wc.Colaborador.Id,
                        Nome = wc.Colaborador.Nome
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (workshop == null)
                return NotFound(new { mensagem = "Workshop não encontrado" });

            return Ok(workshop);
        }

        // ✅ POST - Criar (com validação de colaboradores)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkshopCreateDTO dto)
        {
            try
            {
                // Validar se há colaboradores
                if (dto.ColaboradoresIds == null || !dto.ColaboradoresIds.Any())
                {
                    return BadRequest(new { mensagem = "É necessário informar pelo menos um colaborador para o workshop" });
                }

                // Validar se todos os colaboradores existem
                var colaboradoresExistentes = await _context.Colaboradores
                    .Where(c => dto.ColaboradoresIds.Contains(c.Id))
                    .ToListAsync();

                if (colaboradoresExistentes.Count != dto.ColaboradoresIds.Count)
                {
                    var idsNaoEncontrados = dto.ColaboradoresIds
                        .Except(colaboradoresExistentes.Select(c => c.Id));
                    
                    return BadRequest(new 
                    { 
                        mensagem = "Alguns colaboradores não foram encontrados",
                        idsNaoEncontrados = idsNaoEncontrados 
                    });
                }

                // Criar workshop
                var workshop = new Workshop
                {
                    Nome = dto.Nome,
                    DataRealizacao = dto.DataRealizacao,
                    Descricao = dto.Descricao,
                    WorkshopColaboradores = new List<WorkshopColaborador>()
                };

                // Adicionar colaboradores
                foreach (var colabId in dto.ColaboradoresIds)
                {
                    workshop.WorkshopColaboradores.Add(new WorkshopColaborador
                    {
                        ColaboradorId = colabId
                    });
                }

                _context.Workshops.Add(workshop);
                await _context.SaveChangesAsync();

                // Carregar os dados completos para retornar
                var workshopCriado = await _context.Workshops
                    .Include(w => w.WorkshopColaboradores)
                        .ThenInclude(wc => wc.Colaborador)
                    .Where(w => w.Id == workshop.Id)
                    .Select(w => new WorkshopResponseDTO
                    {
                        Id = w.Id,
                        Nome = w.Nome,
                        Descricao = w.Descricao,
                        DataRealizacao = w.DataRealizacao,
                        Colaboradores = w.WorkshopColaboradores.Select(wc => new ColaboradorInfoDTO
                        {
                            Id = wc.Colaborador.Id,
                            Nome = wc.Colaborador.Nome
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshopCriado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar workshop", erro = ex.Message });
            }
        }

        // ✅ PUT - Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkshopUpdateDTO dto)
        {
            try
            {
                var workshop = await _context.Workshops
                    .Include(w => w.WorkshopColaboradores)
                    .FirstOrDefaultAsync(w => w.Id == id);

                if (workshop == null)
                    return NotFound(new { mensagem = "Workshop não encontrado" });

                // Validar colaboradores se foram fornecidos
                if (dto.ColaboradoresIds != null && dto.ColaboradoresIds.Any())
                {
                    var colaboradoresExistentes = await _context.Colaboradores
                        .Where(c => dto.ColaboradoresIds.Contains(c.Id))
                        .ToListAsync();

                    if (colaboradoresExistentes.Count != dto.ColaboradoresIds.Count)
                    {
                        var idsNaoEncontrados = dto.ColaboradoresIds
                            .Except(colaboradoresExistentes.Select(c => c.Id));
                        
                        return BadRequest(new 
                        { 
                            mensagem = "Alguns colaboradores não foram encontrados",
                            idsNaoEncontrados = idsNaoEncontrados 
                        });
                    }
                }

                // Atualizar dados básicos
                workshop.Nome = dto.Nome;
                workshop.DataRealizacao = dto.DataRealizacao;
                workshop.Descricao = dto.Descricao;

                // Atualizar colaboradores se fornecidos
                if (dto.ColaboradoresIds != null)
                {
                    workshop.WorkshopColaboradores.Clear();
                    
                    foreach (var colabId in dto.ColaboradoresIds)
                    {
                        workshop.WorkshopColaboradores.Add(new WorkshopColaborador
                        {
                            WorkshopId = id,
                            ColaboradorId = colabId
                        });
                    }
                }

                await _context.SaveChangesAsync();

                // Retornar workshop atualizado com dados completos
                var workshopAtualizado = await _context.Workshops
                    .Include(w => w.WorkshopColaboradores)
                        .ThenInclude(wc => wc.Colaborador)
                    .Where(w => w.Id == id)
                    .Select(w => new WorkshopResponseDTO
                    {
                        Id = w.Id,
                        Nome = w.Nome,
                        Descricao = w.Descricao,
                        DataRealizacao = w.DataRealizacao,
                        Colaboradores = w.WorkshopColaboradores.Select(wc => new ColaboradorInfoDTO
                        {
                            Id = wc.Colaborador.Id,
                            Nome = wc.Colaborador.Nome
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return Ok(workshopAtualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar workshop", erro = ex.Message });
            }
        }

        // ✅ DELETE - Remover
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var workshop = await _context.Workshops
                    .Include(w => w.WorkshopColaboradores)
                    .FirstOrDefaultAsync(w => w.Id == id);

                if (workshop == null)
                    return NotFound(new { mensagem = "Workshop não encontrado" });

                _context.Workshops.Remove(workshop);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao deletar workshop", erro = ex.Message });
            }
        }
    }
}