namespace Services.BrandAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
