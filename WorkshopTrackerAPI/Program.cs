using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using SeuProjeto.Data;
using SeuProjeto.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do banco (In Memory)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("WorkshopDb"));

// 2. Configuração de Autorização e Autenticação (Adicionado)
// ⚠️ IMPORTANTE: Configure o JWT corretamente ou remova se não for usar
// Por enquanto, vou comentar a autenticação para facilitar os testes
// builder.Services.AddAuthentication("Bearer").AddJwtBearer();
// builder.Services.AddAuthorization();

// 3. Controllers + evitar loop JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém nomes originais
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. CORS - Configuração para aceitar o frontend Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            // Permite apenas o frontend Angular rodando localmente
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); // Se usar autenticação
        });
    
    // Opcional: política para desenvolvimento (permite tudo)
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// 🔥 IMPORTANTE: Ordem correta dos middlewares
// 1. CORS primeiro
app.UseCors("AllowAngularApp"); // Use a política específica

// 2. Arquivos estáticos (se tiver)
app.UseDefaultFiles();
app.UseStaticFiles();

// 3. HTTPS (se estiver em produção)
app.UseHttpsRedirection();

// 4. Autenticação e Autorização (comentado por enquanto)
app.UseAuthentication();
app.UseAuthorization();

// 5. Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 6. Mapeamento dos Controllers
app.MapControllers();

// 🔥 OPCIONAL: Adicionar dados iniciais para teste
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Adicionar alguns colaboradores de exemplo se não existirem
    if (!dbContext.Colaboradores.Any())
    {
        dbContext.Colaboradores.AddRange(
            new Colaborador { Nome = "João Silva" },
            new Colaborador { Nome = "Maria Santos" },
            new Colaborador { Nome = "Pedro Oliveira" }
        );
        await dbContext.SaveChangesAsync();
    }
    
    // Adicionar alguns workshops de exemplo se não existirem
    if (!dbContext.Workshops.Any())
    {
        var workshop1 = new Workshop 
        { 
            Nome = "Workshop de C#", 
            Descricao = "Introdução ao ASP.NET Core", 
            DataRealizacao = DateTime.Now.AddDays(7) 
        };
        
        var workshop2 = new Workshop 
        { 
            Nome = "Workshop de Angular", 
            Descricao = "Desenvolvimento Frontend Moderno", 
            DataRealizacao = DateTime.Now.AddDays(14) 
        };
        
        dbContext.Workshops.AddRange(workshop1, workshop2);
        await dbContext.SaveChangesAsync();
    }
}

app.Run();