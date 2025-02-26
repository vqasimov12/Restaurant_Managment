using Application;
using DAL.SqlServer;
using RestaurantManagement.Middlewares;
using RestaurantManagment.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var conn = builder.Configuration.GetConnectionString("myconn");

builder.Services.AddSqlServerServices(conn);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>(2,TimeSpan.FromMinutes(1));


app.MapControllers();

app.Run();