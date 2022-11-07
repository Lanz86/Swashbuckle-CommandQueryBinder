using LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Reflection;

namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.ModelBinders;

public class CommandQueryModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType.GetProperties().Any(x => x.IsDefined(typeof(ParameterLocationAttribute))))
        {
            return new BinderTypeModelBinder(typeof(CommandQueryModelBinder));
        }

        return null;
    }

}
