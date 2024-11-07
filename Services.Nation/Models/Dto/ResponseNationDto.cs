namespace Services.NationAPI.Models.Dto
{
    public class ResponseNationDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
