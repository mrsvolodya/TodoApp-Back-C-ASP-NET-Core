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
        options.AddPolicy("AllowAll",
                  policy => policy.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
      });

      builder.Services.AddControllers();

      var app = builder.Build();

      app.UseCors("AllowAll");

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
