using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.CompositeModelBinding.SourceResolvers
{
    public class RouteSourceResolver : ISourceResolver
    {
        private readonly ModelBindingContext _bindingContext;

        public RouteSourceResolver(ModelBindingContext bindingContext)
        {
            _bindingContext = bindingContext;
        }

        public bool TryGetValue(string name, out object? value)
        {
            return _bindingContext.ActionContext.RouteData.Values.TryGetValue(name, out value);
        }
    }
}