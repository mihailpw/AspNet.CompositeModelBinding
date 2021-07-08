namespace AspNet.CompositeModelBinding.SourceResolvers
{
    public interface ISourceResolver
    {
        public bool TryGetValue(string name, out object? value);
    }
}