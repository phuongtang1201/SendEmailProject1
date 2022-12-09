using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SendEmailProject1.Models;
using SendEmailProject1.Services;


namespace SendEmailProject1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SendEmailProject1", Version = "v1" });
            });

            services.Configure<EmailStoreDatabaseSettings>(
                Configuration.GetSection(nameof(EmailStoreDatabaseSettings)));

            services.AddSingleton<IEmailStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<EmailStoreDatabaseSettings>>().Value);

            services.AddSingleton<IMongoClient>(s =>
                new MongoClient(Configuration.GetValue<string> ("EmailStoreDatabaseSettings:ConnectionString")));

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDatabaseService, DatabaseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SendEmailProject1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
