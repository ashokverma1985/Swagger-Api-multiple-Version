using System.Web.Http;
using WebActivatorEx;
using WebapiApp;
using Swashbuckle.Application;
using System.Web.Http.Description;
using Swashbuckle.Swagger;
using System.Linq;
//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebapiApp
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var apiExplorer = config.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'V");
            config.EnableSwagger(
                    swagger =>
                    {
                        swagger.MultipleApiVersions(
                            (apiDesc, targetApiVersion) => apiDesc.GetGroupName() == targetApiVersion,
                            versionBuilder =>
                            {
                                foreach (var group in apiExplorer.ApiDescriptions)
                                {
                                    var description = "";
                                    if (group.IsDeprecated) description += "This API deprecated";

                                    versionBuilder.Version(group.Name, $"Service API {group.ApiVersion}")
                                        .Description(description);
                                }
                            });
                        swagger.DocumentFilter<VersionFilter>();
                        swagger.OperationFilter<VersionOperationFilter>();
                    })
                .EnableSwaggerUi(cfg => cfg.EnableDiscoveryUrlSelector());
        }
    }


    public class VersionFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths = swaggerDoc.paths
                .ToDictionary(
                    path => path.Key.Replace("v{version}", swaggerDoc.info.version),
                    path => path.Value
                );
        }
    }
    public class VersionOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var version = operation.parameters?.FirstOrDefault(p => p.name == "version");
            if (version != null)
            {
                operation.parameters.Remove(version);
            }
        }
    }
}
