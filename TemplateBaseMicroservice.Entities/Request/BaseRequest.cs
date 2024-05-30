

using Newtonsoft.Json;

namespace TemplateBaseMicroservice.Entities
{
    public enum Operation : byte
    {
        Undefined,
        Add,
        Edit,
        Delete
    }
    public class Pagination
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int TotalRows { get; set; } = 0;
    }
    public abstract class BaseRequest
    {
        [JsonIgnore]
        public Guid Ticket { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public string ClientName { get; set; } = string.Empty;
        [JsonIgnore]
        public string UserName { get; set; } = string.Empty;
        [JsonIgnore]
        public string ServerName { get; set; } = string.Empty;
        [JsonIgnore]
        public int Resultado { get; set; } = int.MaxValue;
    }
    //public abstract class OperationRequest<T> : BaseRequest
    //{
    //    public T Item { get; set; }
    //    public Operation Operation { get; set; }
    //}
    public abstract class FilterItemRequest<T, F> : BaseRequest
    {
        public T Filter { get; set; }
        public F FilterType { get; set; }
        [JsonIgnore]
        public Pagination Pagination { get; set; } = new Pagination();
    }
    //public abstract class FilterLstItemRequest<T, F> : BaseRequest
    //{
    //    public T Filter { get; set; }
    //    public F FilterType { get; set; }
    //    public Pagination Pagination { get; set; } = new Pagination();
    //}
}

