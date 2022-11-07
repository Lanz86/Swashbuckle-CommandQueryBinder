using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;
using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Filters.Swashbuckle;

public class CommandQuerySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var excludeProperties = context.Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ParameterLocationAttribute)));
        if (excludeProperties != null && excludeProperties.Any())
        {
            foreach (var property in excludeProperties)
            {
                var propertyName = property.Name.ToLowerFirstChar();
                if (schema.Properties.ContainsKey(propertyName))
                {
                    schema.Properties.Remove(propertyName);
                }
            }
        }
    }
}
