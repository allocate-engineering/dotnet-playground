using Microsoft.AspNetCore.Rewrite;
using UserApi.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Has to happen before builder.Build() gets called.
builder.Services.AddSingleton<UserRepository>();

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

// Redirect to swagger from root
var rewriteOptions = new RewriteOptions().AddRedirect("^$", "/swagger");
app.UseRewriter(rewriteOptions);

app.Run();
