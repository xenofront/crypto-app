using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppsettingsMonitor>(builder.Configuration);

builder.Services.AddSingleton<ICommandFactory, CommandFactory>();

WebApplication app = builder.Build();

Appsettings.OptionsMonitor = app.Services.GetRequiredService<IOptionsMonitor<AppsettingsMonitor>>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();