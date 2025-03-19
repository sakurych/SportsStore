﻿using System.Reflection.Metadata.Ecma335;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            this.context = ctx;
        }
        public IEnumerable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var dbEntry = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
        public Product DeleteProduct(int productId)
        {
            var dbEntry = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (dbEntry != null)
            {
                context.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
