using System.Text.Json.Serialization;
using Blueeit.Api.Repositories;
using Blueeit.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var clientUrl = builder.Configuration.GetValue<string>("Blueeit:ClientUrl")
    ?? throw new InvalidOperationException("Blueeit:ClientUrl is not configured.");

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

builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions =>
	{
		jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

builder.Services.AddSingleton<IForumRepository, InMemoryForumRepository>();
builder.Services.AddSingleton<IForumThreadRepository, InMemoryForumThreadRepository>();
builder.Services.AddSingleton<IPostRepository, InMemoryPostRepository>();
builder.Services.AddSingleton<IProfilePostRepository, InMemoryProfilePostRepository>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IUserActivityRepository, InMemoryUserActivityRepository>();

builder.Services.AddSingleton<IUserActivityService, InMemoryUserActivityService>();
builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IForumService, InMemoryForumService>();
builder.Services.AddSingleton<IForumThreadService, InMemoryForumThreadService>();
builder.Services.AddSingleton<IPostService, InMemoryPostService>();
builder.Services.AddSingleton<IProfilePostService, InMemoryProfilePostService>();
builder.Services.AddSingleton<IStatsService, InMemoryStatsService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

//app.UseHttpsRedirection();

app.UseCors("Client");

app.MapControllers();

app.MapGet("/", () => "Welcome to the Blueeit API!");

app.Run();
