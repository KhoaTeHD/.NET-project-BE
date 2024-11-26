﻿using AutoMapper;
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

        [HttpDelete]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Address customer = _dbContext.Addresses.First(u => u.Address_ID == id);
                _dbContext.Addresses.Remove(customer);
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

    }
}
