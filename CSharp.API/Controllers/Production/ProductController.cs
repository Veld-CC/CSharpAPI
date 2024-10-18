using CShapr.API.Helpers;
using CShapr.Models.Production.Product;
using CSharp.Data.Interfaces.Production;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CSharp.API.Controllers.Production
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnviroment;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        private readonly string _internalServerErrorMessage = "No se pudo realizar {0} debido a un error interno del servidor, intenta más tarde";


        public ProductController(IHostEnvironment hostEnviroment, ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _hostEnviroment = hostEnviroment;
            _logger = logger;
            _productRepository = productRepository;
        }


        [HttpGet("GetProductPriceCategory/{ProductID}")]
        [CompressResponse]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductPriceCategory(int ProductID)
        {
            try
            {
                IEnumerable<ProductCategoryPrice>? items = await _productRepository.GetProductPriceCategory(ProductID: null);
                if (items == null || !items.Any()) return NotFound("No se encontraron artículos");
                return Ok(items);
            }
            catch (Exception _)
            {
                WriteLog.Error(_logger, _, "GetArticulo");

                if (_hostEnviroment.EnvironmentName.Equals("Development"))
                    return StatusCode(500, _.Message);
                else
                    return StatusCode(500, string.Format(_internalServerErrorMessage, "la búsqueda del artículo"));
            }
        }


    }
}
