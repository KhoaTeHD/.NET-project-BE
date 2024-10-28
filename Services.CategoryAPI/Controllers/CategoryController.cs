using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public ResponseDto Get() 
        {
            try
            {
                IEnumerable<Category> categories = _dbContext.Categories.ToList();
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
        public ResponseDto Get(int id)
        {
            try
            {
                Category category = _dbContext.Categories.First(u => u.Id == id);
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
        public ResponseDto Post([FromBody] CategoryDto categoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDto);
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();

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
        public ResponseDto Put([FromBody] CategoryDto categoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDto);
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();

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
        public ResponseDto Delete(int id)
        {
            try
            {
                Category category = _dbContext.Categories.First(u => u.Id == id);
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
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
