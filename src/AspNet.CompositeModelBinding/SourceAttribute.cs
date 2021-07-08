using System;

namespace AspNet.CompositeModelBinding
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SourceAttribute : Attribute
    {
        public SourceAttribute(SourceType sourceType)
            : this(null, sourceType)
        {
        }

        public SourceAttribute(string name, SourceType sourceType)
        {
            Name = name;
            SourceType = sourceType;
        }

        public string Name { get; }
        public SourceType SourceType { get; }
        public bool ForceDrop { get; set; } = true;
    }
}