using CryptoManager.Data;
using CryptoManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Registrar HttpClient + CryptoPriceService
builder.Services.AddHttpClient<ICryptoPriceService, CryptoPriceService>();


// Controladores (API)
builder.Services.AddControllers();

// DbContext con SQL Server
// Asegurate de tener instalado Microsoft.EntityFrameworkCore.SqlServer
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS: permitir peticiones desde el frontend (puerto 5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // Ajustar si cambia el puerto
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Swagger/OpenAPI (para desarrollo y pruebas)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// SWAGGER: habilitar en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS según la política definida
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear rutas de los controladores
app.MapControllers();

app.Run();
