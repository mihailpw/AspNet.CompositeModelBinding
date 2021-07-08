namespace AspNet.CompositeModelBinding.Sample
{
    public class Dto
    {
        [Source(SourceType.Route)] public int Id { get; set; }
        [Source("id2", SourceType.Route, ForceDrop = false)] public string Id2 { get; set; }

        [Source("User-Agent", SourceType.Header)] public string UserAgent { get; set; }
        [Source("Content-Type", SourceType.Header)] public string ContentType { get; set; }

        [Source("queryParamInt", SourceType.Query)] public int? QueryParamInt { get; set; }
        [Source("queryParamString", SourceType.Query)] public string QueryParamString { get; set; }

        [Source("formParamInt", SourceType.Form)] public int? FormParamInt { get; set; }
        [Source("formParamString", SourceType.Form)] public int? FormParamString { get; set; }

        public int? BodyFieldInt { get; set; }
        public string BodyFieldString { get; set; }
    }
}