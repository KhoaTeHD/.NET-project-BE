using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.GoodsReceiptAPI.Data;
using Services.GoodsReceiptAPI.Models;
using Services.GoodsReceiptAPI.Models.Dto;
using Services.GoodsReceiptAPI.Service.IService;

namespace Services.GoodsReceiptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsReceiptController : Controller
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;
        private IProductVariationService _productVariationService;
        private ISupplierService _supplierService;

        public GoodsReceiptController(AppDbContext dbContext, IMapper mapper, IProductVariationService productVariationService, ISupplierService supplierService)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
            _productVariationService = productVariationService;
            _supplierService = supplierService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<GoodsReceipt> goodsReceipts = await _dbContext.GoodsReceipts
                    .Include(gr => gr.DetailGoodsReceipts)
                    .ToListAsync();
                var receipts = _mapper.Map<IEnumerable<GoodsReceiptDto>>(goodsReceipts);

                IEnumerable<ProductVariationDto> productVariationDtos = await _productVariationService.GetProductVariations();
                IEnumerable<SupplierDto> supplierDtos = await _supplierService.GetSuppliers();

                foreach (var receipt in receipts)
                {
                    receipt.Supplier = supplierDtos.FirstOrDefault(u => u.Supplier_ID == receipt.Supplier_ID);
                    foreach (var detail in receipt.DetailGoodsReceipts)
                    {
                        //Console.WriteLine(productVariationDtos.FirstOrDefault(u => u.Id == detail.Product_ID).Id);
                        detail.ProductVariation = productVariationDtos.FirstOrDefault(u => u.Id == detail.Product_ID);
                    }
                }

                _response.Result = receipts;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                GoodsReceipt? goodsReceipt = await _dbContext.GoodsReceipts
                    .Include(gr => gr.DetailGoodsReceipts)
                    .FirstOrDefaultAsync(gr => gr.Goo_ID == id);

                if (goodsReceipt == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "GoodsReceipt not found.";
                    return _response;
                }

                var receipt = _mapper.Map<GoodsReceiptDto>(goodsReceipt);
                IEnumerable<ProductVariationDto> productVariationDtos = await _productVariationService.GetProductVariations();
                IEnumerable<SupplierDto> supplierDtos = await _supplierService.GetSuppliers();

                receipt.Supplier = supplierDtos.FirstOrDefault(u => u.Supplier_ID == receipt.Supplier_ID);
                foreach (var detail in receipt.DetailGoodsReceipts)
                {
                    detail.ProductVariation = productVariationDtos.FirstOrDefault(u => u.Id == detail.Product_ID);
                }

                _response.Result = receipt;
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
        public async Task<ResponseDto> Post([FromBody] GoodsReceiptDto goodsReceiptDto)
        {
            try
            {
                if (goodsReceiptDto.DetailGoodsReceipts == null || !goodsReceiptDto.DetailGoodsReceipts.Any())
                {
                    _response.IsSuccess = false;
                    _response.Message = "A GoodsReceipt must have at least one DetailGoodsReceipt.";
                    return _response;
                }

                GoodsReceipt goodsReceipt = _mapper.Map<GoodsReceipt>(goodsReceiptDto);
                await _dbContext.GoodsReceipts.AddAsync(goodsReceipt);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<GoodsReceiptDto>(goodsReceipt);
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
        public async Task<ResponseDto> Put([FromBody] GoodsReceiptDto goodsReceiptDto)
        {
            try
            {
                GoodsReceipt? goodsReceipt = await _dbContext.GoodsReceipts
                    .Include(gr => gr.DetailGoodsReceipts)
                    .FirstOrDefaultAsync(gr => gr.Goo_ID == goodsReceiptDto.Goo_ID);

                if (goodsReceipt == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "GoodsReceipt not found.";
                    return _response;
                }

                // Update GoodsReceipt properties
                _mapper.Map(goodsReceiptDto, goodsReceipt);

                // Update existing details and add new ones
                var existingDetails = goodsReceipt.DetailGoodsReceipts.ToList();
                foreach (var detail in goodsReceiptDto.DetailGoodsReceipts)
                {
                    var existingDetail = existingDetails.FirstOrDefault(d => d.Product_ID == detail.Product_ID);
                    if (existingDetail != null)
                    {
                        _mapper.Map(detail, existingDetail);
                    }
                    else
                    {
                        var newDetail = _mapper.Map<DetailGoodsReceipt>(detail);
                        newDetail.Goo_ID = goodsReceipt.Goo_ID; // Ensure foreign key is set
                        _dbContext.DetailGoodsReceipts.Add(newDetail);
                    }
                }

                // Remove details that no longer exist
                foreach (var detail in existingDetails)
                {
                    if (!goodsReceiptDto.DetailGoodsReceipts.Any(d => d.Product_ID == detail.Product_ID))
                    {
                        _dbContext.DetailGoodsReceipts.Remove(detail);
                    }
                }

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<GoodsReceiptDto>(goodsReceipt);
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
                GoodsReceipt? goodsReceipt = await _dbContext.GoodsReceipts
                    .Include(gr => gr.DetailGoodsReceipts)
                    .FirstOrDefaultAsync(gr => gr.Goo_ID == id);

                if (goodsReceipt == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "GoodsReceipt not found.";
                    return _response;
                }

                _dbContext.GoodsReceipts.Remove(goodsReceipt);
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
