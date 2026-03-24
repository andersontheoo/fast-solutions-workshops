using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using SeuProjeto.Data;


var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do banco (In Memory)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("WorkshopDb"));

// 2. Configuração de Autorização e Autenticação (Adicionado)
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();

// 3. Controllers + evitar loop JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();


// Ativa o CORS primeiro
app.UseCors("AllowAll");

// 🔥 IMPORTANTE: Permite que o servidor entregue arquivos HTML, CSS e JS
// Seus arquivos devem estar na pasta 'wwwroot' do projeto
app.UseDefaultFiles(); 
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();