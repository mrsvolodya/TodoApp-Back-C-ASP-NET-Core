using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TodoApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddCors(options =>
      {
        options.AddPolicy("AllowFrontend",
                  policy => policy.WithOrigins("http://localhost:5174")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
      });

      builder.Services.AddControllers();

      var app = builder.Build();

      app.UseCors("AllowFrontend");

      if (app.Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.MapControllers();

      app.Run();
    }
  }
}
