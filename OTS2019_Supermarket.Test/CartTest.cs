using NUnit.Framework;
using OTS_Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS_Supermarket.Test
{
    [TestFixture]
    public class CartTest
    {
        [Test]
        public void AddOneToCart_ShouldAddItemToCart_Success()
        {
            //ARRANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);
            //ACT
            cart.AddOneToCart(monitor);
            //ASSERT
            Assert.That(cart.Size, Is.EqualTo(1));


        }

        [Test]
        public void AddMultipleToCart_Throws_WhenExeedsLimit()
        {
            Cart cart = new Cart();
            cart.Size = 8;
            Keyboard keyboard = new Keyboard();

            Exception exception = Assert.Throws<Exception>(() => cart.AddMultipleToCart(keyboard, 3));
            Assert.That(exception.Message, Is.EqualTo(("Number of items in cart must be 10 or less!"));
        }

        [Test]
        public void Calculate_NotEnoughBudget_Throws()
        {
            Cart cart = new Cart();
            cart.Budget = 100;
            cart.AddOneToCart(new Computer());

            var delivery = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            Exception ex = Assert.Throws<Exception>(() => cart.Calculate(delivery));
            Assert.That(ex.Message, Is.EqualTo("Not enough budget!"));
        }

        [TestCase(0, 1)]
        [TestCase(5, 6)]
        public void AddOneToCart_DataDriven(int initialSize, int expectedSize)
        {
            Cart cart = new Cart();
            cart.Size = initialSize;

            cart.AddOneToCart(new Monitor());

            Assert.That(cart.Size, Is.EqualTo(expectedSize));
        }

        [Test]
        public void Print_Throws_WhenEmpty()
        {
            Cart cart = new Cart();
            Exception ex = Assert.Throws<Exception>(() => cart.Print());
            Assert.That(ex.Message, Is.EqualTo("Cannot print empty cart!"));
        }
    }
}
