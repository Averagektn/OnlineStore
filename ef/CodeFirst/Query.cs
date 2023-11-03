using CodeFirst.Model;

namespace CodeFirst
{
    public class Query
    {
        private readonly ClothesStoreContext _dbContext;

        public Query(ClothesStoreContext dbc)
        {
            _dbContext = dbc;
        }

        /// <summary>
        ///     Get all products of selected brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public ICollection<Product> Get_Products(string brandName)
        {
            var result = _dbContext.Products
                .Where(pro => pro.ProBrandNavigation.BraName == brandName)
                .Select(pro => new Product
                {
                    ProId = pro.ProId,
                    ProBrand = pro.ProBrand,
                    ProCategory = pro.ProCategory,
                    ProName = pro.ProName,
                    ProPrice = pro.ProPrice,
                    ProAverageRating = pro.ProAverageRating
                })
                .ToList();

            return result;
        }

        /// <summary>
        ///     Get all product variations for the selected product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ICollection<ProductVariant> Get_ProductVariants(string productName)
        {
            var result = _dbContext.ProductVariants
                    .Where(prv => prv.PrvProductNavigation.ProName == productName)
                    .Select(prv => new ProductVariant
                    {
                        PrvId = prv.PrvId,
                        PrvColor = prv.PrvColor,
                        PrvSize = prv.PrvSize,
                        PrvSku = prv.PrvSku,
                        PrvQuantity = prv.PrvQuantity,
                        PrvProduct = prv.PrvProduct,
                    })
                    .ToList();

            return result;
        }

        /// <summary>
        ///     Select all brands with the number of their products respectively.Order by the number of products.
        /// </summary>
        /// <returns></returns>
        public ICollection<Brand> Get_Brands()
        {
            var result = _dbContext.Brands
                .Select(brand => new
                {
                    Brand = brand,
                    ProductCount = _dbContext.Products.Count(product => product.ProBrand == brand.BraId)
                })
                .OrderByDescending(brandWithCount => brandWithCount.ProductCount)
                .Select(brandWithCount => brandWithCount.Brand)
                .ToList();

            return result;
        }

        /// <summary>
        ///     Get all products for a given category and section.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public ICollection<Product> Get_Products(Category category, Section section)
        {
            var products = _dbContext.Products
                .Where(pro =>
                    pro.ProCategoryNavigation.CatName == category.CatName &&
                    pro.ProCategoryNavigation.SectionSecs.Any(cs => cs.SecName == section.SecName))
                .Select(pro => new Product
                {
                    ProId = pro.ProId,
                    ProBrand = pro.ProBrand,
                    ProCategory = pro.ProCategory,
                    ProName = pro.ProName,
                    ProPrice = pro.ProPrice,
                    ProAverageRating = pro.ProAverageRating
                })
                .ToList();

            return products;
        }



        /// <summary>
        ///     Get all completed orders with a given product. Order from newest to latest.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ICollection<Order> Get_CompletedOrders(Product product)
        {
            var result = _dbContext.Orders
                .Join(_dbContext.OrderProductVariants,
                    ord => ord.OrdId,
                    opv => opv.OpvOrder,
                    (ord, opv) => new { Order = ord, OrderProductVariant = opv })
                .Join(_dbContext.ProductVariants,
                    opv => opv.OrderProductVariant.OpvProductVariant,
                    prv => prv.PrvId,
                    (pair, prv) => new { pair.Order, pair.OrderProductVariant, ProductVariant = prv })
                .Join(_dbContext.Products,
                    prv => prv.ProductVariant.PrvProduct,
                    pro => pro.ProId,
                    (triple, pro) => new { triple.Order, triple.OrderProductVariant, triple.ProductVariant, Product = pro })
                .Where(res => res.Product.ProId == product.ProId)
                .OrderByDescending(res => res.Order.OrdDate)
                .Select(res => new Order
                {
                    OrdId = res.Order.OrdId,
                    OrdDate = res.Order.OrdDate,
                    OrdAddress = res.Order.OrdAddress,
                    OrdPrice = res.Order.OrdPrice,
                    OrdUser = res.Order.OrdUser,
                })
                .ToList();

            return result;
        }

        /// <summary>
        ///     Get all reviews for a given product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ICollection<Review> Get_Reviews(Product product)
        {
            var result = _dbContext.Reviews
                .Where(rev => rev.RevProduct == product.ProId)
                .Select(rev => new Review
                {
                    RevProduct = product.ProId,
                    RevAuthor = rev.RevAuthor,
                    RevDate = rev.RevDate,
                    RevComment = rev.RevComment,
                    RevId = rev.RevId,
                    RevRating = rev.RevRating,
                    RevTitle = rev.RevTitle,
                })
                .ToList();

            return result;
        }

    }

}
