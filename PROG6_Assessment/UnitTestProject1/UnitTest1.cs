using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG6_Assessment.ViewModel;
using Moq;
using DomainModel.Repository;
using System.Collections.Generic;
using DomainModel.Model;
using PROG6_Assessment.Model;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod2()
        {

            //Arrange

            Mock<IMerkRepository> moq = new Mock<IMerkRepository>();

            IList<Merk> merken = new List<Merk>
                {
                    new Merk { MerkId = 1, MerkNaam = "Nam" },
                };

            moq.Setup(m => m.GetAllMerken()).Returns(merken);

            var MerkVM = new MerkViewModel(moq.Object);

            Console.Write(MerkVM.GetMerkNaamMock());

            //Act
            var naam = MerkVM.GetMerkNaamMock();

            //Assert
            Assert.AreEqual("Nam", naam);

        }

        [TestMethod]
        public void TestMethod3()
        {

            //Arrange
            Mock<IProductRepository> moq = new Mock<IProductRepository>();

            IList<Product> products = new List<Product>
            {

                new Product { ProductId = 1, ProductNaam = "Bedorven brood", Prijs = 20},
                new Product { ProductId = 2, ProductNaam = "Tijgerbrood", Prijs=10}
            };

            moq.Setup(m => m.GetAllProducts()).Returns(products);

            var ProductVM = new ProductViewModel(moq.Object);

            //Act
            var prijs = ProductVM.TotalePrijs();

            //Assert
            Assert.AreEqual(30, prijs);

        }
    }
}
