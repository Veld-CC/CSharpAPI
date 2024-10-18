using CShapr.Models.Production.Product;

namespace CSharp.Data.Interfaces.Production
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductCategoryPrice>> GetProductPriceCategory(int? ProductID);
    }
}
