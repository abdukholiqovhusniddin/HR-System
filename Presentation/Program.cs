using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>(optional: true);

builder.Host.ConfigureSerilog();
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();
app.Environment.LogSecretsPresence(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAppMiddlewares();

app.MapControllers();

app.Run();