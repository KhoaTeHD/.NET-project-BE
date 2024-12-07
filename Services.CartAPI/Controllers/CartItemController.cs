using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.CartItemAPI.Data;
using Services.CartItemAPI.Models;
using Services.CartItemAPI.Models.Dto;
using Services.CartItemAPI.Service.IService;
using ResponseCartItemDto = Services.CartItemAPI.Models.Dto.ResponseCartItemDto;

namespace Services.CartItemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseCartItemDto _response;
        private IMapper _mapper;
        private IProductVariationService _productVariationService;
        public CartItemController(AppDbContext dbContext, IMapper mapper, IProductVariationService productVariationService)
        {
            _dbContext = dbContext;
            _response = new ResponseCartItemDto();
            _mapper = mapper;
            _productVariationService = productVariationService;
        }


        [HttpGet]
        public async Task<ResponseCartItemDto> Get()
        {
            try
            {
                IEnumerable<CartItem> cartItems = await _dbContext.CartItems.ToListAsync();
                var cartDtos = _mapper.Map<IEnumerable<CartItemDto>>(cartItems);

                IEnumerable<ProductVariationDto> productVariationDtos = await _productVariationService.GetProductVariations();
                if (productVariationDtos == null || !productVariationDtos.Any())
                {
                    throw new Exception("No products found.");
                }

                foreach (var cartDto in cartDtos)
                {
                    cartDto.ProductVariation = productVariationDtos.FirstOrDefault(u => u.Id == cartDto.Item_Id);
                    if (cartDto.ProductVariation == null)
                    {
                        throw new Exception($"Product not found for Cart {cartDto.Item_Id} ");
                    }
                }

                _response.Result = cartDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[HttpGet]
        //[Route("{id:int}")]
        //public async Task<ResponseCartItemDto> Get(string cus_id)
        //{
        //    try
        //    {
        //        List<CartItem> cartItems = await _dbContext.CartItems
        //            .Where(u => u.Cus_Id == cus_id)  
        //            .ToListAsync();  
        //        _response.Result = _mapper.Map<List<CartItemDto>>(cartItems);  
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        [HttpGet]
        [Route("{cus_id}")]
        public async Task<ResponseCartItemDto> GetByCustomerId(string cus_id)
        {
            try
            {
                List<CartItem> cartItems = await _dbContext.CartItems
                    .Where(u => u.Cus_Id == cus_id)
                    .ToListAsync();
                IEnumerable<CartItemDto> cartDtos = _mapper.Map<IEnumerable<CartItemDto>>(cartItems);

                IEnumerable<ProductVariationDto> productVariationDtos = await _productVariationService.GetProductVariations();
                if (productVariationDtos == null || !productVariationDtos.Any())
                {
                    throw new Exception("No products found.");
                }

                foreach (var cartDto in cartDtos)
                {
                    cartDto.ProductVariation = productVariationDtos.FirstOrDefault(u => u.Id == cartDto.Item_Id);
                    if (cartDto.ProductVariation == null)
                    {
                        throw new Exception($"Product not found for Cart {cartDto.Item_Id} ");
                    }
                }

                _response.Result = cartDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        public async Task<ResponseCartItemDto> Post([FromBody] CartItemDto cartItemDTO)
        {
            try
            {
                // Get the list of cart items for the customer
                var existingCartItemsResponse = await GetByCustomerId(cartItemDTO.Cus_Id);
                if (!existingCartItemsResponse.IsSuccess)
                {
                    throw new Exception(existingCartItemsResponse.Message);
                }

                var existingCartItems = existingCartItemsResponse.Result as IEnumerable<CartItemDto>;
                var existingCartItem = existingCartItems?.FirstOrDefault(c => c.Item_Id == cartItemDTO.Item_Id);

                if (existingCartItem != null)
                {
                    // If the item already exists, update the quantity
                    existingCartItem.Quantity += 1;
                    return await Put(existingCartItem);
                }
                else
                {
                    // If the item does not exist, add it to the database
                    CartItem cartItem = _mapper.Map<CartItem>(cartItemDTO);
                    await _dbContext.CartItems.AddAsync(cartItem);
                    await _dbContext.SaveChangesAsync();

                    _response.Result = _mapper.Map<CartItemDto>(cartItem);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        public async Task<ResponseCartItemDto> Put([FromBody] CartItemDto cartItemDTO)
        {
            try
            {
                CartItem? cartItem = await _dbContext.CartItems
                    .FirstOrDefaultAsync(c => c.Cus_Id == cartItemDTO.Cus_Id && c.Item_Id == cartItemDTO.Item_Id);

                if (cartItem == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cart item not found.";
                    return _response;
                }

                cartItem.Quantity = cartItemDTO.Quantity;
                _mapper.Map(cartItemDTO, cartItem);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CartItemDto>(cartItem);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseCartItemDto> Delete([FromBody] CartItemDto cartItemDTO)
        {
            try
            {
                CartItem cartItem = await _dbContext.CartItems
                    .FirstOrDefaultAsync(c => c.Cus_Id == cartItemDTO.Cus_Id && c.Item_Id == cartItemDTO.Item_Id);

                if (cartItem == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cart item not found.";
                    return _response;
                }

                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Message = "Cart item deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles= "CUSTOMER")]
        public async Task<ResponseCartItemDto> DeleteCartItemById(int id)
        {
            try
            {
                // Tìm CartItem theo Id
                CartItem? cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Item_Id == id);

                if (cartItem == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cart item not found.";
                    return _response;
                }

                // Xóa CartItem khỏi database
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Message = "Cart item deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


    }
}
