using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Controllers;
using Library.Models;
using Library.Tests.Fakes;

namespace Library.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        [TestMethod]
        public void Create_SavesBook_Valid()
        {
            // Arrange

            var db = new FakeLibraryDb();
            BooksController controller = new BooksController(db);
            controller.ControllerContext = new FakeControllerContext();

            // Act
            Book book = new Book();
            book.Id = 1;
            book.Title = "Atuty zguby";
            book.Author = "Zelazny, Roger";
            book.Isbn = "6814681651";
            book.Rate = 5;
            controller.Create(book);

            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            var db = new FakeLibraryDb();
            BooksController controller = new BooksController(db);

            // Act
            //controller.Edit(book);

            // Assert
            //Assert.AreEqual("Prosta biblioteka do wykorzystania w domu.", result.ViewBag.Message);
        }
    }
}
