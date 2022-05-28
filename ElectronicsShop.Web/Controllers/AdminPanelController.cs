using ElectronicsShop.DAL.Data;
using ElectronicsShop.Models;
using ElectronicsShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsShop.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminPanelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Views/AdminPanel/AdminPanel.cshtml");
        }
        public IActionResult GetProducts(int pageNum, int typeId)
        {
            var data = _unitOfWork.ProductRepository.GetProductsWithPaging(pageNum, 10, typeId);
            var culture = Request.Cookies[".AspNetCore.Culture"];
            var productsRowViewModel = data.Select(a => new ProductRowViewModel(a, culture)).ToList();
            var totalCount = _unitOfWork.ProductRepository.GetGridCount(typeId);
            var pagesCount = Math.Ceiling(totalCount / 10d);
            var isLastPage = pageNum * 10 >= totalCount && (pageNum - 1) * 10 < totalCount;
            var gridViewModel = new GridViewModel<ProductRowViewModel>
                (productsRowViewModel, pageNum, pageNum == 1, isLastPage, pagesCount);
            return new JsonResult(gridViewModel);
        }
        [AllowAnonymous]
        public IActionResult GetProductCategories()
        {
            var culture = Request.Cookies[".AspNetCore.Culture"];
            var types = _unitOfWork.ProductCategoryRepository.All.Select(a => new ProductCategoryViewModel(a, culture)).ToList();
            return new JsonResult(types);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            return View("~/Views/AdminPanel/AddNewProduct.cshtml"); ;
        }

        [HttpPost]
        public IActionResult AddNewProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newProduct = new Product
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                DescriptionAr = model.DescriptionAr,
                DescriptionEn = model.DescriptionEn,
                OriginalPrice = model.OriginalPrice,
                QuantityInStock = model.QuantityInStock,
                Discount = model.Discount is null ? 0 : model.Discount.Value,
                DiscountOfTwo = model.DiscountOfTwo is null ? 0 : model.DiscountOfTwo.Value,
                ProductCategory = _unitOfWork.ProductCategoryRepository.Where(a => a.ProductCategoryId == model.TypeId).FirstOrDefault(),
            };
            _unitOfWork.ProductRepository.Add(newProduct);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var dbProduct = _unitOfWork.ProductRepository.Where(p => p.ProductId == id).FirstOrDefault();
            if (dbProduct == null) return NotFound();
            UpdateProductViewModel model = new UpdateProductViewModel();
            model.ProductId = dbProduct.ProductId;
            model.NameAr = dbProduct.NameAr;
            model.NameEn = dbProduct.NameEn;
            model.DescriptionAr = dbProduct.DescriptionAr;
            model.DescriptionEn = dbProduct.DescriptionEn;
            model.OriginalPrice = dbProduct.OriginalPrice;
            model.QuantityInStock = dbProduct.QuantityInStock;
            model.Discount = dbProduct.Discount;
            model.DiscountOfTwo = dbProduct.DiscountOfTwo;
            model.TypeId = dbProduct.ProductCategoryId;

            return View("~/Views/AdminPanel/UpdateProduct.cshtml", model);
        }

        [HttpPost]
        public IActionResult UpdateProduct(UpdateProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dbProduct = _unitOfWork.ProductRepository.Where(p => p.ProductId == model.ProductId).FirstOrDefault();
            if (dbProduct == null) return NotFound();

            
            dbProduct.NameAr = model.NameAr;
            dbProduct.NameEn = model.NameEn;
            dbProduct.DescriptionAr = model.DescriptionAr;
            dbProduct.DescriptionEn = model.DescriptionEn;
            dbProduct.OriginalPrice = model.OriginalPrice;
            dbProduct.QuantityInStock = model.QuantityInStock;
            dbProduct.Discount = model.Discount is null ? 0 : model.Discount.Value;
            dbProduct.DiscountOfTwo = model.DiscountOfTwo is null ? 0 : model.DiscountOfTwo.Value;
            dbProduct.ProductCategory = _unitOfWork.ProductCategoryRepository.Where(a => a.ProductCategoryId == model.TypeId).FirstOrDefault();

            _unitOfWork.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }

        public IActionResult AllOrders()
        {
            return View("~/Views/AdminPanel/AllOrders.cshtml");
        }
        public IActionResult GetOrdersGridData(int pageNum, int? typeId = null)
        {
            var data = _unitOfWork.ProductOrderRepository.GetProductOrdersWithPaging(pageNum, 10, typeId);
            var culture = Request.Cookies[".AspNetCore.Culture"];
            var orderRowViewModel = data.Select(a => new ProductOrdersRowViewModel(a, culture)).ToList();
            var totalCount = _unitOfWork.ProductOrderRepository.All.Count();
            var pagesCount = Math.Ceiling(totalCount / 10d);
            var isLastPage = pageNum * 10 >= totalCount && (pageNum - 1) * 10 < totalCount;
            var gridViewModel = new GridViewModel<ProductOrdersRowViewModel>(orderRowViewModel, pageNum, pageNum == 1, isLastPage, pagesCount);
            return new JsonResult(gridViewModel);
        }
    }
}
