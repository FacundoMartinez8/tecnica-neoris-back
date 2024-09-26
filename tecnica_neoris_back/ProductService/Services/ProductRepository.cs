using System.Collections.Generic;
using System.Linq;
using ProductMicroservice.Models;

namespace ProductMicroservice.Services
{
    public class ProductRepository
    {
        private List<Product> _products = new List<Product>();

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            Product? existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
        }

        public void Delete(int id)
        {
            Product? product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}