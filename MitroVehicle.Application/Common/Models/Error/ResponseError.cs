namespace MitroVehicle.Application.Common.Models.Error
{
    public class ResponseError
    {
        public string Message { get; set; }
        public IEnumerable<ResponseErrorItem> Errors { get; set; }
    }
}
