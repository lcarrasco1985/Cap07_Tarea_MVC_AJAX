using Cap07_Tarea_MVC_AJAX.CORE.Model;
using Cap07_Tarea_MVC_AJAX.CORE.Repository;
using Cap07_Tarea_MVC_AJAX.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap07_Tarea_MVC_AJAX.WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository ProductRepository)
        {
            _productRepository = ProductRepository;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Product/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult List(int pageNumber = 1, int pageSize = 10, string searchText = "")
        {
            //var products = _productRepository.GetAll();
            //var productList = new List<ProductViewModel>();
            //foreach(var item in products)
            //{
            //    var product = new ProductViewModel();
            //    product.Id = item.Id;
            //    product.ProductName = item.ProductName;
            //    product.SupplierId = item.SupplierId;
            //    product.UnitPrice = item.UnitPrice;
            //    product.Package = item.Package;
            //    product.IsDiscontinued = item.IsDiscontinued;

            //    productList.Add(product);
            //}
            //return View(productList);

            var products = string.IsNullOrEmpty(searchText) ?
                            _productRepository.GetPaged(pageNumber, pageSize) :
                            _productRepository.GetPagedByProductName(pageNumber, pageSize, searchText);

            var customerList = products.Select(item => new ProductViewModel
            {
                Id = item.Id,
                ProductName = item.ProductName,
                SupplierId = item.SupplierId,
                UnitPrice = item.UnitPrice,
                Package = item.Package,
                IsDiscontinued = item.IsDiscontinued
            }).ToList();

            var totalCustomers = string.IsNullOrEmpty(searchText) ?
                                    _productRepository.GetAll().Count() :
                                    _productRepository.GetCountByProductName(searchText);


            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);
            ViewBag.TotalRows = totalCustomers;

            return PartialView(customerList);


        }

        //Insert or Update
        public JsonResult Save(ProductViewModel productViewModel)
        {
            bool isSuccess = true;

            try
            {
                if (productViewModel.Id == -1)
                {
                    //Es un nuevo registro
                    var product = new Product()
                    {
                        ProductName = productViewModel.ProductName,
                        SupplierId = productViewModel.SupplierId,
                        UnitPrice = productViewModel.UnitPrice,
                        Package = productViewModel.Package,
                        IsDiscontinued = productViewModel.IsDiscontinued
                    };

                    _productRepository.Add(product);
                }
                else
                {
                    //Un cliente existente
                    var product = new Product()
                    {
                        Id = productViewModel.Id,
                        ProductName = productViewModel.ProductName,
                        SupplierId = productViewModel.SupplierId,
                        UnitPrice = productViewModel.UnitPrice,
                        Package = productViewModel.Package,
                        IsDiscontinued = productViewModel.IsDiscontinued
                    };

                    _productRepository.Update(product);
                }

            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return Json(isSuccess);

        }

        public JsonResult GetProduct(int id)
        {
            var product = _productRepository.GetById(id);
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                UnitPrice = product.UnitPrice,
                Package = product.Package,
                IsDiscontinued = product.IsDiscontinued
            };
            return Json(productViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            bool isSuccess = true;
            try
            {
                _productRepository.Delete(id);
            }
            catch (Exception)
            {

                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }
    }
}