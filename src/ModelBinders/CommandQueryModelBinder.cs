using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;
using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.ModelBinders;

public class CommandQueryModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        string valueFromBody;
        using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
        {
            valueFromBody = await streamReader.ReadToEndAsync();
        }

        //Deserilaize body content to model instance  
        var modelType = bindingContext.ModelMetadata.UnderlyingOrModelType;
        if (string.IsNullOrWhiteSpace(valueFromBody)) valueFromBody = "{}";
        var modelInstance = JsonSerializer.Deserialize(valueFromBody, modelType)??Activator.CreateInstance(modelType);
        
        var props = modelType.GetProperties().Where(
                        prop => Attribute.IsDefined(prop, typeof(ParameterLocationAttribute)));

        foreach (var prop in props)
        {
            object paramValue = new object();
            switch (prop.GetCustomAttribute<ParameterLocationAttribute>()!.ParameterLocation)
            {
                case ParameterLocation.Query:
                    paramValue = bindingContext.HttpContext.Request.Query.TryGetValue(prop.Name, out StringValues value) ? value : new StringValues();
                    break;
                case ParameterLocation.Header:
                    paramValue = bindingContext.ActionContext.HttpContext.Request.Headers.TryGetValue(prop.Name, out StringValues hValue) ? hValue : new StringValues();
                    break;
                case ParameterLocation.Path:
                    bindingContext.ActionContext.RouteData.Values.TryGetValue(prop.Name, out paramValue);
                    break;
                case ParameterLocation.Cookie:
                    paramValue = bindingContext.ActionContext.HttpContext.Request.Cookies.TryGetValue(prop.Name, out string cValue) ? cValue : String.Empty;
                    break;
            }

            prop.ForceSetValue(modelInstance, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(paramValue.ToString()));
        }


        bindingContext.Result = ModelBindingResult.Success(modelInstance);

    }
}

