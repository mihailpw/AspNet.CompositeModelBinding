namespace AspNet.CompositeModelBinding.Sample
{
    public class Dto
    {
        [RouteSource] public int Id { get; set; }
        [RouteSource("id2")] public string Id2 { get; set; }

        [HeaderSource("User-Agent")] public string UserAgent { get; set; }
        [HeaderSource("Content-Type")] public string ContentType { get; set; }

        [QuerySource("queryParamInt")] public int? QueryParamInt { get; set; }
        [QuerySource("queryParamString")] public string QueryParamString { get; set; }

        [FormSource("formParamInt")] public int? FormParamInt { get; set; }
        [FormSource("formParamString")] public int? FormParamString { get; set; }

        public int? BodyFieldInt { get; set; }
        public string BodyFieldString { get; set; }
    }
}