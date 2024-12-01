using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.AddressAPI.Data;
using Services.AddressAPI.Models;
using Services.AddressAPI.Models.Dto;

namespace Services.AddressAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;


        public AddressController(AppDbContext dbContext, IMapper mapper)
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
                IEnumerable<Address> address = await _dbContext.Addresses.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<AddressDto>>(address);
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
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Address address = await _dbContext.Addresses.FirstAsync(u => u.Address_ID == id);
                _response.Result = _mapper.Map<AddressDto>(address);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> Post([FromBody] AddressDto addressDto)
        {
            try
            {
                Address address = _mapper.Map<Address>(addressDto);
                if (address.IsDefault == true)
                {
                    var addresses = await _dbContext.Addresses
                                            .Where(a => a.IsDefault == true && a.Customer_ID == addressDto.Customer_ID)
                                            .ToListAsync();

                    foreach (var addr in addresses)
                    {
                        addr.IsDefault = false; // Reset isDefault
                    }
                }
                await _dbContext.Addresses.AddAsync(address);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<AddressDto>(address);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> Put([FromBody] AddressDto addressDto)
        {
            try
            {

                Address? address = await _dbContext.Addresses.FindAsync(addressDto.Address_ID);

                if (address == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Address not found.";
                    return _response;
                }

                if (addressDto.IsDefault == true)
                {
                    var addresses = await _dbContext.Addresses
                                             .Where(a => a.IsDefault == true && a.Customer_ID == addressDto.Customer_ID)
                                             .ToListAsync();

                    foreach (var addr in addresses)
                    {
                        addr.IsDefault = false; // Reset isDefault
                    }
                }

                _mapper.Map(addressDto, address);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<AddressDto>(address);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Address address = _dbContext.Addresses.First(u => u.Address_ID == id);
                _dbContext.Addresses.Remove(address);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> GetByCustomerId(string customerId)
        {
            try
            {
                // Lấy danh sách địa chỉ theo Customer_ID
                IEnumerable<Address> addresses = await _dbContext.Addresses
                    .Where(a => a.Customer_ID == customerId)
                    .ToListAsync();

                // Map danh sách địa chỉ sang DTO
                _response.Result = _mapper.Map<IEnumerable<AddressDto>>(addresses);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        private async Task ResetDefaultAddresses()
        {
            var addresses = await _dbContext.Addresses
                                             .Where(a => a.IsDefault == true)
                                             .ToListAsync();

            foreach (var addr in addresses)
            {
                addr.IsDefault = false; // Reset isDefault
            }

            await _dbContext.SaveChangesAsync();

        }

        [HttpPut("set-default/{addressId}/{customerId}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> SetDefaultAddress(int addressId, string customerId)
        {
            try
            {
                // Lấy địa chỉ từ database
                Address address = _dbContext.Addresses.First(u => u.Address_ID == addressId);

                if (address == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Address not found.";
                    return _response;
                }

                // Reset tất cả các địa chỉ đang có `isDefault = true`
                var addresses = await _dbContext.Addresses
                                             .Where(a => a.IsDefault == true && a.Customer_ID == customerId)
                                             .ToListAsync();

                foreach (var addr in addresses)
                {
                    addr.IsDefault = false; // Reset isDefault
                }

                // Đặt địa chỉ được chỉ định làm mặc định
                address.IsDefault = true;

                // Lưu thay đổi vào database
                await _dbContext.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Message = "Address has been set as default.";
                _response.Result = _mapper.Map<AddressDto>(address);
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
