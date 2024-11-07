namespace Services.CartItemAPI.Models.Dto
{
    public class ResponseCartItemDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
