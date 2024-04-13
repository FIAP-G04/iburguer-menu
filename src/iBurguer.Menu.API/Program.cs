using iBurguer.Menu.Infrastructure.IoC;
using iBurguer.Menu.Infrastructure.Logger;
using iBurguer.Menu.Infrastructure.MongoDb.Extensions;
using iBurguer.Menu.Infrastructure.Swagger;
using iBurguer.Menu.Infrastructure.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddWebApi()
       .AddMongoDb()
       .AddRepositories()
       .AddUseCases()
       .AddSerilog();

var app = builder.Build();

app.UseWebApi();
app.Run();