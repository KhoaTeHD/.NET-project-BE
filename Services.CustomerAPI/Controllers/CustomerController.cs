using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.CustomerAPI.Data;
using Services.CustomerAPI.Models;
using Services.CustomerAPI.Models.Dto;

namespace Services.CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;


        public CustomerController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Customer> customers = await _dbContext.Customers.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Customer customer = await _dbContext.Customers.FirstAsync(u => u.Cus_Id == id);
                _response.Result = _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] CustomerDto customerDto)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerDto);
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] CustomerDto customerDto)
        {
            try
            {

                Customer? customer = await _dbContext.Customers.FindAsync(customerDto.Cus_Id);

                if (customer == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Customer not found.";
                    return _response;
                }
                _mapper.Map(customerDto, customer);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Customer customer = _dbContext.Customers.First(u => u.Cus_Id == id);
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
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
