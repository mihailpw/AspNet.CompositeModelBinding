using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AspNet.CompositeModelBinding.SourceResolvers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet.CompositeModelBinding
{
    public class CompositeModelBinder : IModelBinder
    {
        private static readonly Dictionary<SourceType, Func<ModelBindingContext, ISourceResolver>> SourceFactories = new()
        {
            [SourceType.Route] = c => new RouteSourceResolver(c),
            [SourceType.Query] = c => new QuerySourceResolver(c),
            [SourceType.Header] = c => new HeaderSourceResolver(c),
            [SourceType.Form] = c => new FormSourceResolver(c),
        };

        private readonly IServiceProvider _serviceProvider;

        public CompositeModelBinder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string bodyString;
            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body, Encoding.UTF8))
            {
                bodyString = await reader.ReadToEndAsync();
            }

            var modelInstance = !string.IsNullOrEmpty(bodyString)
                ? JsonSerializer.Deserialize(
                    bodyString,
                    bindingContext.ModelType,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                : ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, bindingContext.ModelType);
            if (modelInstance == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            var properties = bindingContext.ModelType.GetProperties();
            foreach (var propertyInfo in properties)
            {
                TryUpdateSource(bindingContext, propertyInfo, modelInstance);
            }

            bindingContext.Result = ModelBindingResult.Success(modelInstance);
        }

        private static void TryUpdateSource(ModelBindingContext bindingContext, PropertyInfo propertyInfo, object instance)
        {
            var sourceAttribute = propertyInfo.GetCustomAttribute<SourceAttribute>();
            if (sourceAttribute == null)
            {
                return;
            }

            var propertyName = !string.IsNullOrEmpty(sourceAttribute.Name) ? sourceAttribute.Name : propertyInfo.Name;
            var sourceResolver = SourceFactories[sourceAttribute.SourceType](bindingContext);
            object? propertyValue = null;
            if (sourceResolver.TryGetValue(propertyName, out var value)
                && value != null)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyValue = value.ToString();
                }
                else
                {
                    var valueType = value.GetType();
                    var propertyTypeConverter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                    if (propertyTypeConverter.CanConvertFrom(valueType))
                    {
                        try
                        {
                            propertyValue = propertyTypeConverter.ConvertFrom(value);
                        }
                        catch (ArgumentException)
                        {
                            // log
                        }
                    }
                    else
                    {
                        // log
                    }
                }
            }

            if (propertyValue == null)
            {
                if (!sourceAttribute.ForceDrop)
                {
                    return;
                }

                if (propertyInfo.PropertyType.IsValueType)
                {
                    propertyValue = Activator.CreateInstance(propertyInfo.PropertyType);
                }
            }

            propertyInfo.SetValue(instance, propertyValue);
        }
    }
}