using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Communication
{
    public class ResponseResult
    {
        [JsonConstructor]
        public ResponseResult()
        {
            Status = 400;
        }
        public ResponseResult(int status = 400, IDictionary<string, string[]>? errors = null, string title = "")
        {
            Title = title;
            Status = status;
            Errors = errors;
        }

        public string? Title { get; set; }
        public int Status { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }
    }
}
