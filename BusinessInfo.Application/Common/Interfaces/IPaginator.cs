namespace BusinessInfo.Application.Common.Interfaces
{
    public interface IPaginator
    {
        int Limit { get; set; }
        int Offset { get; set; }
    }
}
