using Blueeit.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var clientUrl = builder.Configuration.GetValue<string>("Blueeit:ClientUrl")
    ?? throw new InvalidOperationException("Blueeit:ClientUrl is not configured.");

//builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Client", policy =>
    {
        policy
            .WithOrigins(clientUrl)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IForumService, InMemoryForumService>();
builder.Services.AddSingleton<IForumThreadService, InMemoryForumThreadService>();
builder.Services.AddSingleton<IPostService, InMemoryPostService>();
builder.Services.AddSingleton<IProfilePostService, InMemoryProfilePostService>();
builder.Services.AddSingleton<IStatsService, InMemoryStatsService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseCors("Client");

app.MapControllers();

app.MapGet("/", () => "Welcome to the Blueeit API!");

app.Run();
