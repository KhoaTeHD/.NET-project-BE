namespace Services.SizeAPI.Models.Dto
{
    public class ResponseSizeDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
