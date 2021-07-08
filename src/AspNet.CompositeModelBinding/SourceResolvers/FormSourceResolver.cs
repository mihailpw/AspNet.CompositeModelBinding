using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.CompositeModelBinding.SourceResolvers
{
    public class FormSourceResolver : ISourceResolver
    {
        private readonly ModelBindingContext _bindingContext;

        public FormSourceResolver(ModelBindingContext bindingContext)
        {
            _bindingContext = bindingContext;
        }

        public bool TryGetValue(string name, out object? value)
        {
            if (_bindingContext.HttpContext.Request.HasFormContentType
                && _bindingContext.HttpContext.Request.Form.TryGetValue(name, out var headerValue))
            {
                value = headerValue.ToString();
                return true;
            }

            value = default;
            return false;
        }
    }
}