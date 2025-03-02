using Xunit;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            Product prod1 = new Product { ProductId = 1, Name = "Prod1" };
            Product prod2 = new Product { ProductId = 2, Name = "Prod2" };

            var cart = new Cart();
            cart.AddItem(prod1, 1);
            cart.AddItem(prod2, 1);
            CartLine[] results = cart.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(prod1, results[0].Product);
            Assert.Equal(prod2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product prod1 = new Product { ProductId = 1, Name = "Prod1" };
            Product prod2 = new Product { ProductId = 2, Name = "Prod2" };

            var cart = new Cart();
            cart.AddItem(prod1, 1);
            cart.AddItem(prod2, 1);
            cart.AddItem(prod1, 10);
            CartLine[] results = cart.Lines
                .OrderBy(c => c.Product.ProductId).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Product p3 = new Product { ProductId = 3, Name = "P3" };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);

            Assert.Equal(0, target.Lines.Where(c => c.Product == p2).Count());
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();

            Assert.Equal(0, target.Lines.Count());
        }
    }
}
