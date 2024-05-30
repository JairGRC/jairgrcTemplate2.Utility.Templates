namespace TemplateBaseMicroservice.Entities
{
    public abstract class BaseResponse : BaseRequest
    {
        public bool IsSuccess { get; set; } = false;
        public List<EResponse> LstError { get; set; } = new List<EResponse>();
        public List<EResponse> Warnings { get; set; } = new List<EResponse>();
    }
    public class EResponse
    {
        public string cDescripcion { get; set; }
        public string? Info { get; set; }
 
    }
    public abstract class ItemResponse<T> : BaseResponse
    {
        public T Item { get; set; }
        public Pagination Pagination { get; set; } 
    }
    public abstract class LstItemResponse<T> : BaseResponse
    {
        public IEnumerable<T> LstItem { get; set; } = new List<T>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
}

