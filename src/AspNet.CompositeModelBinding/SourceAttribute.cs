using System;

namespace AspNet.CompositeModelBinding
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class SourceAttribute : Attribute
    {
        protected SourceAttribute(SourceType sourceType)
            : this(null, sourceType)
        {
        }

        protected SourceAttribute(string? name, SourceType sourceType)
        {
            Name = name;
            SourceType = sourceType;
        }

        public string? Name { get; }
        public SourceType SourceType { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class RouteSourceAttribute : SourceAttribute
    {
        public RouteSourceAttribute() : base(SourceType.Route)
        {
        }

        public RouteSourceAttribute(string? name) : base(name, SourceType.Route)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class QuerySourceAttribute : SourceAttribute
    {
        public QuerySourceAttribute() : base(SourceType.Query)
        {
        }

        public QuerySourceAttribute(string? name) : base(name, SourceType.Query)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderSourceAttribute : SourceAttribute
    {
        public HeaderSourceAttribute() : base(SourceType.Header)
        {
        }

        public HeaderSourceAttribute(string? name) : base(name, SourceType.Header)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FormSourceAttribute : SourceAttribute
    {
        public FormSourceAttribute() : base(SourceType.Form)
        {
        }

        public FormSourceAttribute(string? name) : base(name, SourceType.Form)
        {
        }
    }
}