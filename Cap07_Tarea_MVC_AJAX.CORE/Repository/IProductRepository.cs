using Cap07_Tarea_MVC_AJAX.CORE.Model;
using System.Collections.Generic;

namespace Cap07_Tarea_MVC_AJAX.CORE.Repository
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Delete(int id);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        int GetCountByProductName(string productname);
        IEnumerable<Product> GetPaged(int pageNumber, int pageSize);
        IEnumerable<Product> GetPagedByProductName(int pageNumber, int pageSize, string productname);
        void Update(Product product);
    }
}