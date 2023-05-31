using Microsoft.EntityFrameworkCore;
using PWAY_ASPNetCore_WebAPI.Models;
using PWAY_ASPNetCore_WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// startup functions for dbContext
//var connectionString = builder.Configuration.GetConnectionString("BooksDB");
builder.Services.AddDbContext<BookDbContext>(options =>
options.UseSqlServer("name=ConnectionStrings:BooksDB"));
// startup functions for IUriServices
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService, UriService>(o => //get the baseUri
{ 
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider(); 
var configuration = provider.GetRequiredService<IConfiguration>();

// CORS policy
builder.Services.AddCors(options =>
{
    var frontendURL = configuration.GetValue<string>("frontend_URL"); //from appsettings.json

    options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//Enable CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();



