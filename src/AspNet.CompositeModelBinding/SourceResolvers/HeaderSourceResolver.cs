using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.CompositeModelBinding.SourceResolvers
{
    public class HeaderSourceResolver : ISourceResolver
    {
        private readonly ModelBindingContext _bindingContext;

        public HeaderSourceResolver(ModelBindingContext bindingContext)
        {
            _bindingContext = bindingContext;
        }

        public bool TryGetValue(string name, out object? value)
        {
            if (_bindingContext.HttpContext.Request.Headers.TryGetValue(name, out var headerValue))
            {
                value = headerValue;
                return true;
            }

            value = default;
            return false;
        }
    }
}