using MyFi.TheBadlands.Clients;
using MyFi.TheBadlands.Database;
using MyFi.TheBadlands.Models.Monzo;
using MyFi.TheBadlands.Repositories;
using MyFi.TheBadlands.Services;

namespace MyFi.TheBadlands;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<IDbConnectionConfig>(new SqliteConnectionConfig(Configuration["DatabaseName"]));
        services.AddScoped<CommandRepository>();
        services.AddScoped<QueryRepository>();

        services.AddScoped<MonzoService>();
        services.AddScoped<UserService>();

        services.AddOptions<MonzoOptions>()
            .BindConfiguration("Monzo");

        services.AddHttpClient<MonzoClient>();
        // Add retry policy
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}