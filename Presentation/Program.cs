using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>(optional: true);

// Development diagnostics: don't print secret values, only presence
if (builder.Environment.IsDevelopment())
{
    var hasSmtpPassword = !string.IsNullOrEmpty(builder.Configuration["SendEmail:Password"]);
    var hasJwtSecret = !string.IsNullOrEmpty(builder.Configuration["AuthSettings:SecretKey"]);
    Console.WriteLine($"[Diagnostics] SendEmail:Password loaded: {hasSmtpPassword}");
    Console.WriteLine($"[Diagnostics] AuthSettings:SecretKey loaded: {hasJwtSecret}");
}

builder.Host.ConfigureSerilog();
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAppMiddlewares();

app.MapControllers();

app.Run();