using ChatSampleApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("/", (IWebHostEnvironment env) =>
{
    var indexPath = Path.Combine(env.ContentRootPath, "wwwroot", "index.html");
    if (File.Exists(indexPath))
    {
        // File exists, serve it
        return Results.File(indexPath, "text/html");
    }
    else
    {
        // Log an error or handle the missing file scenario
        Console.WriteLine($"Index HTML file not found at path: {indexPath}");
        // You may want to return a different response or handle the error in another way
        return Results.NotFound();
    }
});

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chathub"); // Map the SignalR hub

app.Run();
