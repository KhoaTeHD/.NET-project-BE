namespace Services.BrandAPI.Models.Dto
{
    public class ResponseBrandDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
