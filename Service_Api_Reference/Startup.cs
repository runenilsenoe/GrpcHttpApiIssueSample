using Microsoft.OpenApi.Models;
using Service_Api_Reference.Services;

namespace Service_Api_Reference;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    private const string ApiName = "Service_Api_Reference";
    private const string ApiVersion = "v1";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); 
        services.AddGrpc();
        services.AddGrpcHttpApi();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = ApiName, Version = ApiVersion });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
        services.AddGrpcSwagger();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"{ApiName} {ApiVersion}"); });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<GreeterService>();
        });
    }
}
