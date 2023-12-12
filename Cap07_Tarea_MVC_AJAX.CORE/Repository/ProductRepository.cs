using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap07_Tarea_MVC_AJAX.CORE.Model;
using Dapper;

namespace Cap07_Tarea_MVC_AJAX.CORE.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IEnumerable<Product> GetAll()
        {
            string query = "SELECT * FROM Product";
            return _dbConnection.Query<Product>(query);
        }
        public Product GetById(int id)
        {
            var query = "SELECT * FROM Product WHERE Id = @Id";
            return _dbConnection.QueryFirstOrDefault<Product>(query, new { Id = id });
        }

        public void Add(Product product)
        {
            var query = "INSERT INTO Product (ProductName, SupplierId, UnitPrice, Package, IsDiscontinued) VALUES " +
                "(@ProductName, @SupplierId, @UnitPrice, @Package, @IsDiscontinued)";
            _dbConnection.Execute(query, new
            {
                product.ProductName,
                product.SupplierId,
                product.UnitPrice,
                product.Package,
                product.IsDiscontinued
            });
        }

        public void Update(Product product)
        {
            try 
            { 
            var query = "UPDATE Product SET ProductName=@ProductName, SupplierId=@SupplierId, UnitPrice=@UnitPrice, " +
                "Package=@Package, IsDiscontinued=@IsDiscontinued WHERE Id=@Id";
            _dbConnection.Execute(query, new
            {
                product.ProductName,
                product.SupplierId,
                product.UnitPrice,
                product.Package,
                product.IsDiscontinued,
                product.Id
            });
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, registrándola o lanzándola nuevamente
                Console.WriteLine($"Error al ejecutar la consulta de eliminación: {ex.Message}");
                throw;
            }
           
        }

        public void Delete(int id)
        {
            try
            {
                var query = "DELETE FROM Product WHERE Id = @Id";
                _dbConnection.Execute(query, new { Id = id });
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, registrándola o lanzándola nuevamente
                Console.WriteLine($"Error al ejecutar la consulta de eliminación: {ex.Message}");
                throw;
            }

        }
        public IEnumerable<Product> GetPaged(int pageNumber, int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;
            string query = $"SELECT  * FROM  Product ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
            return _dbConnection.Query<Product>(query);
        }

        public IEnumerable<Product> GetPagedByProductName(int pageNumber, int pageSize, string productname)
        {
            int offset = (pageNumber - 1) * pageSize;
            string query = $"SELECT  * FROM  Product WHERE ProductName LIKE @productname ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
            return _dbConnection.Query<Product>(query, new { ProductName = $"%{productname}%" });
        }

        public int GetCountByProductName(string productname)
        {
            string query = $"SELECT COUNT(*) FROM Product WHERE ProductName LIKE @productname";
            return _dbConnection.ExecuteScalar<int>(query, new { ProductName = $"%{productname}%" });
        }
    }
}
