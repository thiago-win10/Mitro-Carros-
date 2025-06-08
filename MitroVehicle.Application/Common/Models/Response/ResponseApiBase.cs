using Newtonsoft.Json;

namespace MitroVehicle.Application.Common.Models.Response
{
    public class ResponseApiBase<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("errors")]
        public List<ResponseApiError> Errors { get; set; } = new List<ResponseApiError>();

        public void AddSuccess(T data) => this.Data = data;
        public void AddError(string key, string value) => this.Errors.Add(new ResponseApiError { Key = key, Value = value });
        public void AddError(string value) => this.Errors.Add(new ResponseApiError { Value = value });
    }
}
