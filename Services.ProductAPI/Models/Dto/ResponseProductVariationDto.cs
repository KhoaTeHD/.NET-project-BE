﻿namespace Services.ProductAPI.Models.Dto
{
    public class ResponseProductVariationDto
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
