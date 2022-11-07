using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;
using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Filters.Swashbuckle;

public class CommandQueryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var modelProperties = context.MethodInfo.GetParameters()
                                .SelectMany(p =>
                                            p.ParameterType.GetProperties());

        if (!modelProperties.Any()) return;

        
        if (modelProperties.Any(prop => Attribute.IsDefined(prop, typeof(ParameterLocationAttribute))))
        {
            var props = modelProperties;

            operation.Parameters = props.Where(
                            prop => Attribute.IsDefined(prop, typeof(ParameterLocationAttribute)))
                            .Select(p => new OpenApiParameter
                                    {
                                        Schema = context.SchemaGenerator.GenerateSchema(p.PropertyType, context.SchemaRepository, p),
                                        In = (Microsoft.OpenApi.Models.ParameterLocation)p.GetCustomAttribute<ParameterLocationAttribute>()!.ParameterLocation,
                                        Name = p.Name.ToLowerFirstChar(),
                                        Required = !p.IsNullable() && p.GetCustomAttribute<ParameterLocationAttribute>()!.ParameterLocation == Attributes.ParameterLocation.Path,
                                    }).ToList();

            if(operation.Parameters.Count == modelProperties.Count()) 
            { 
                operation.RequestBody = null;
            }
        }
    }
}
