using Microsoft.OpenApi.Models;
using YesterdaylandAPI.Models;
using Microsoft.EntityFrameworkCore;
using YesterdaylandAPI.Data;


namespace Yesterdayland
{

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServe(Configuration.GetConnectionString("ConexaoDB")));
        //     services.AddDbContext<YesterdaylandContext>(options =>
        //         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //     services.AddControllers();
        //     //Swagger configuration
        //     services.AddSwaggerGen(c =>
        //     {
        //         c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yesterdayland API", Version = "v1" });
        //     });
            
            
        // }
         // Configure CORS to allow all origins
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddDbContext<YesterdaylandContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yesterdayland API v1"));
            } else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }



            // Seed the database
            // using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            // {
            //     var context = serviceScope.ServiceProvider.GetRequiredService<YesterdaylandContext>();
            //     DataSeeder.Seed(context);
            // }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}