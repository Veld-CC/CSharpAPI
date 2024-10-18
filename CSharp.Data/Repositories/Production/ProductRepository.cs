using CShapr.Models.Production.Product;
using CSharp.Data.Helpers;
using CSharp.Data.Interfaces.Production;
using GSM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Data.Repositories.Production
{
    public class ProductRepository : IProductRepository
    {

        private readonly SqlConfigurationData _sqlConfiguration;

        public ProductRepository(SqlConfigurationData sqlConfiguration) => _sqlConfiguration = sqlConfiguration;

        protected SqlConnection DbConnection() => new SqlConnection(_sqlConfiguration.ConnectionString);

        public async Task<IEnumerable<ProductCategoryPrice>> GetProductPriceCategory(int? ProductID)
        {
            ProductID = ProductID == 0 ? null: ProductID;

            using SqlDataReader reader = await SqlHelper.ExecuteReaderAsync(DbConnection(), $"Production.{StoreProcedure.GetProductPriceCategory}", CommandType.StoredProcedure,
            new { ProductID });
            return SqlReader<ProductCategoryPrice>.Read(reader).ToArray();
        }
    }
}
