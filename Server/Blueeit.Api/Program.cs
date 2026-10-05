// Copyright Marco Silva (c). All rights reserved.

using Blueeit.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Client", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
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

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseCors("Client");

app.MapControllers();

app.MapGet("/", () => "Welcome to the Blueeit API!");

app.Run();
