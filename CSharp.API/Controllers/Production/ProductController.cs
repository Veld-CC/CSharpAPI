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

        /// <summary>
        /// Obtiene los negocios asignados a un artículo
        /// </summary>
        /// <param name="IIDEmpresa">Id de la empresa de Friadsys</param>
        /// <param name="IIDArticulo">Id del articulo</param>
        /// <returns>Retorna un IEnumerable de CatArticulosNegocio</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     All Products
        ///     GET: /api/Product/GetProductPriceCategory/0
        ///     One Product for ID
        ///     GET: /api/Product/GetProductPriceCategory/839
        ///		[
        ///         {
        ///         "productID": 839,
        ///         "name": "HL Road Frame - Black, 48",
        ///         "productNumber": "FR-R92B-48",
        ///         "color": "Black",
        ///         "standardCost": 868.6342,
        ///         "listPrice": 1431.5,
        ///         "size": "48",
        ///         "sizeUnitMeasureCode": "CM ",
        ///         "productCategoryID": 2,
        ///         "productCategory": "Components",
        ///         "productSubcategoryID": 14,
        ///         "productSubcategory": "Road Frames"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna un IEnumerable de CatArticulosNegocio</response>
        /// <response code="400">Devuelve un mensaje de error de validación de datos</response>
        /// <response code="500">Retorna un status code 500 con el mensaje de error correspondiente</response>
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
                IEnumerable<ProductCategoryPrice>? items = await _productRepository.GetProductPriceCategory(ProductID);
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
