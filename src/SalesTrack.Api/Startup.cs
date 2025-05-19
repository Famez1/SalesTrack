using FluentValidation;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SalesTrack.Api.Behaviors;
using SalesTrack.Api.Extensions;
using SalesTrack.Api.Middleware;
using SalesTrack.Application.Handlers.Authentication.Commands.SignUpUser;
using SalesTrack.Domain.Options;
using SalesTrack.Infrastructure.Hubs;
using Serilog;

namespace SalesTrack.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services.Configure<JwtTokenOptions>(Configuration.GetSection(JwtTokenOptions.SectionName));
        services.Configure<SafeCookieOptions>(Configuration.GetSection(SafeCookieOptions.SectionName));
        services.Configure<ExpiredNoteJobOptions>(Configuration.GetSection(ExpiredNoteJobOptions.SectionName));

        services.AddHttpContextAccessor();
        services.AddSwaggerDocs(Configuration);

        services.AddSignalR();
        services.AddControllers();

        services.AddSalesTrackDbContext(Configuration);
        services.AddControllers().AddOData(opt => 
        opt.AddRouteComponents("odata", GetEdmModel())
            .Filter()
            .Select()
            .Expand()
            .Count()
            .OrderBy());

        services.AddJwtTokenAuth(Configuration);

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(SingUpUserCommand).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(SingUpUserCommand).Assembly);

        services.AddQuartz(Configuration);

        services.ConfigureAutoMapper(Configuration);

        services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseSwaggerDocumentation();

        app.UseSerilogRequestLogging();

        app.UseODataBatching();
        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<TestHub>("/api/signalr");
        });
    }

    private IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();

        return odataBuilder.GetEdmModel();
    }
}
