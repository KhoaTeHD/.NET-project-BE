using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.CategoryAPI.Data;
using Services.CategoryAPI.Models;
using Services.CategoryAPI.Models.Dto;

namespace Services.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;

        public CategoryController(AppDbContext dbContext, IMapper mapper)
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
                IEnumerable<Category> categories = await _dbContext.Categories.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
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
                Category category = await _dbContext.Categories.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] CategoryDto categoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDto);
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] CategoryDto categoryDto)
        {
            try
            {
                
                Category? category = await _dbContext.Categories.FindAsync(categoryDto.Id);

                if (category == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Category not found.";
                    return _response;
                }
                _mapper.Map(categoryDto, category);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<CategoryDto>(category);
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
                Category category = _dbContext.Categories.First(u => u.Id == id);
                _dbContext.Categories.Remove(category);
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
