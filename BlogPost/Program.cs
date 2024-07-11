global using BlogPost.Persistence;
global using Serilog;
global using BlogPost.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, config) =>
{
    config.Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MMM-yyyy:HH:mm:ss} {Level:u3}] {Message}{NewLine}{Exception}")
        .ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHsts();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();