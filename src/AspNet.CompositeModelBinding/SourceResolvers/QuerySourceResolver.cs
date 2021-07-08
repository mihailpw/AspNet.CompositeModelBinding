using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.CompositeModelBinding.SourceResolvers
{
    public class QuerySourceResolver : ISourceResolver
    {
        private readonly ModelBindingContext _bindingContext;

        public QuerySourceResolver(ModelBindingContext bindingContext)
        {
            _bindingContext = bindingContext;
        }

        public bool TryGetValue(string name, out object? value)
        {
            if (_bindingContext.HttpContext.Request.Query.TryGetValue(name, out var headerValue))
            {
                value = headerValue.ToString();
                return true;
            }

            value = default;
            return false;
        }
    }
}