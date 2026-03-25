using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using SeuProjeto.Data;
using SeuProjeto.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("WorkshopDb"));

builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém nomes originais
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            // Permite apenas o frontend Angular rodando localmente
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); 
        });
    
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();


app.UseCors("AllowAngularApp"); 


app.UseDefaultFiles();
app.UseStaticFiles();


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    
    if (!dbContext.Colaboradores.Any())
    {
        dbContext.Colaboradores.AddRange(
            new Colaborador { Nome = "João Silva" },
            new Colaborador { Nome = "Maria Santos" },
            new Colaborador { Nome = "Pedro Oliveira" }
        );
        await dbContext.SaveChangesAsync();
    }
    
    
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