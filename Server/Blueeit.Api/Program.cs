// Copyright Marco Silva (c). All rights reserved.

using Blueeit.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IThreadService, InMemoryThreadService>();
builder.Services.AddSingleton<IPostService, InMemoryPostService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/", () => "Welcome to the Blueeit API!");

app.MapControllers();

app.Run();
