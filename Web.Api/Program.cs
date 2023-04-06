using Shared.Core;
using Web.Api.Installers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAllService(builder.Configuration);


var app = builder.Build();
app.Use(builder.Configuration,builder.Environment);
app.Run();