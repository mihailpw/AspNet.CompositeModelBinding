using System;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.CompositeModelBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class BindSourceAttribute : ModelBinderAttribute
    {
        public BindSourceAttribute()
            : base(typeof(CompositeModelBinder))
        {
        }
    }
}