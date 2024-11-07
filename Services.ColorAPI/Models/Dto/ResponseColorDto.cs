namespace Services.ColorAPI.Models.Dto
{
    public class ResponseColorDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
