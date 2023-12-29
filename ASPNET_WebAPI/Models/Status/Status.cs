namespace ASPNET_WebAPI.Models.Status
{
    public class Status
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object? FullStatus { get; set; }

        public Status(int code, string message)
        {
            this.StatusCode = code;
            this.StatusMessage = message;
        }

        public Status(int code, string message, object? full)
        {
            this.StatusCode = code;
            this.StatusMessage = message;
            this.FullStatus = full;
        }
    }
}
