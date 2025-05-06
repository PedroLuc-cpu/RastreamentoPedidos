using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace RastreamentoPedidos.API.Configuration.ModelBinders
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (DateTimeModelBinder.SUPPORTED_DATETIME_TYPES.Contains(context.Metadata.ModelType))
            {
                return new BinderTypeModelBinder(typeof(DateTimeModelBinder));
            }

            return null;
        }
    }
}
