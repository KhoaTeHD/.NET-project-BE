using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.SupplierAPI.Data;
using Service.SupplierAPI.Models.Dto;
using Services.SupplierAPI.Models;

namespace Service.SupplierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;


        public SupplierController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Supplier> suppliers = await _dbContext.Suppliers.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Supplier supplier = await _dbContext.Suppliers.FirstAsync(u => u.Supplier_ID == id);
                _response.Result = _mapper.Map<SupplierDto>(supplier);
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
        public async Task<ResponseDto> Post([FromBody] SupplierDto supplierDto)
        {
            try
            {
                Supplier supplier = _mapper.Map<Supplier>(supplierDto);
                await _dbContext.Suppliers.AddAsync(supplier);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<SupplierDto>(supplier);
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
        public async Task<ResponseDto> Put([FromBody] SupplierDto supplierDto)
        {
            try
            {

                Supplier? supplier = await _dbContext.Suppliers.FindAsync(supplierDto.Supplier_ID);

                if (supplier == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Supplier not found.";
                    return _response;
                }
                _mapper.Map(supplierDto, supplier);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<SupplierDto>(supplier);
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
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Supplier supplier = _dbContext.Suppliers.First(u => u.Supplier_ID == id);
                _dbContext.Suppliers.Remove(supplier);
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
