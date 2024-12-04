using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.CouponAPI.Data;
using Services.CouponAPI.Models;
using Services.CouponAPI.Models.Dto;

namespace Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : Controller
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;


        public CouponController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN, CUSTOMER")]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = await _dbContext.Coupons.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Get(string id)
        {
            try
            {
                Coupon coupon = await _dbContext.Coupons.FirstAsync(u => u.Coupon_Code == id);
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                await _dbContext.Coupons.AddAsync(coupon);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CouponDto>(coupon);
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
        public async Task<ResponseDto> Put([FromBody] CouponDto couponDto)
        {
            try
            {

                Coupon? coupon = await _dbContext.Coupons.FindAsync(couponDto.Coupon_Code);

                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found.";
                    return _response;
                }
                _mapper.Map(couponDto, coupon);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Delete(string id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(u => u.Coupon_Code == id);
                _dbContext.Coupons.Remove(coupon);
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
