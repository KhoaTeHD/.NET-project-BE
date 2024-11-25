using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.OrderAPI.Data;
using Services.OrderAPI.Models;
using Services.OrderAPI.Models.Dto;

namespace Services.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;

        public OrderController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Order> orders = await _dbContext.Orders.Include(gr => gr.DetailOrders).ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Order? order = await _dbContext.Orders
                    .Include(gr => gr.DetailOrders)
                    .FirstOrDefaultAsync(gr => gr.Order_ID == id);

                if (order == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Order not found.";
                    return _response;
                }

                _response.Result = _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<ResponseDto> Post([FromBody] OrderDto orderDto)
        {
            try
            {
                if (orderDto.DetailOrders== null || !orderDto.DetailOrders.Any())
                {
                    _response.IsSuccess = false;
                    _response.Message = "An order must have at least one DetailOrder.";
                    return _response;
                }

                Order order = _mapper.Map<Order>(orderDto);
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Put([FromBody] OrderDto orderDto)
        {
            try
            {
                Order? order = await _dbContext.Orders
                    .Include(gr => gr.DetailOrders)
                    .FirstOrDefaultAsync(gr => gr.Order_ID== orderDto.Order_ID);

                if (order == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Order not found.";
                    return _response;
                }

                // Update GoodsReceipt properties
                _mapper.Map(orderDto, order);

                // Update existing details and add new ones
                var existingDetails = order.DetailOrders.ToList();
                foreach (var detail in orderDto.DetailOrders)
                {
                    var existingDetail = existingDetails.FirstOrDefault(d => d.Product_ID == detail.Product_ID);
                    if (existingDetail != null)
                    {
                        _mapper.Map(detail, existingDetail);
                    }
                    else
                    {
                        var newDetail = _mapper.Map<DetailOrder>(detail);
                        newDetail.Order_ID = order.Order_ID; // Ensure foreign key is set
                        _dbContext.DetailOrders.Add(newDetail);
                    }
                }

                // Remove details that no longer exist
                foreach (var detail in existingDetails)
                {
                    if (!orderDto.DetailOrders.Any(d => d.Product_ID == detail.Product_ID))
                    {
                        _dbContext.DetailOrders.Remove(detail);
                    }
                }

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Order? order = await _dbContext.Orders
                    .Include(gr => gr.DetailOrders)
                    .FirstOrDefaultAsync(gr => gr.Order_ID == id);

                if (order == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Order not found.";
                    return _response;
                }

                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("customer/{customerId}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> GetOrdersByCustomerId(string customerId)
        {
            try
            {
                // Lấy danh sách Order theo Customer_ID
                IEnumerable<Order> orders = await _dbContext.Orders
                    .Include(o => o.DetailOrders) // Bao gồm các chi tiết của Order
                    .Where(o => o.Customer_ID == customerId) // Lọc theo Customer_ID
                    .ToListAsync();

                // Map danh sách Order sang DTO
                _response.Result = _mapper.Map<IEnumerable<OrderDto>>(orders);
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
