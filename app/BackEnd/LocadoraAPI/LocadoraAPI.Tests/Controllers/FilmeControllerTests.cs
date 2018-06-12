using System;
using System.Web.Http.Results;
using LocadoraAPI.Controllers;
using LocadoraAPI.Data;
using LocadoraAPI.Model;
using LocadoraAPI.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LocadoraAPI.Tests.Controllers
{
    [TestClass]
    public class FilmeControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        private Mock<IService<Filme>> service;

        public FilmeControllerTests()
        {
            service = new Mock<IService<Filme>>();
        }

        [TestMethod]
        public void get_call_obter()
        {
            //Arrange
            var controller = new FilmeController(service.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void get_com_id_call_obter()
        {
            //Arrange
            Model.Filme filme = new Model.Filme()
            {
                Id = 1,
                Nome = "teste",
                DateDeCriacao = DateTime.Now,
                GeneroID = 1
            };
            service.Setup(x => x.Obter(1)).Returns(filme);
            var controller = new FilmeController(service.Object);

            //Act
            var result = controller.Get(1);

            //Assert
            Assert.AreEqual(filme, result);
        }

        [TestMethod]
        public void post_call_obter()
        {
            //Arrange
            var controller = new FilmeController(service.Object);
            Model.Filme filme = new Model.Filme()
            {
                Nome = "teste",
                DateDeCriacao = DateTime.Now,
                GeneroID = 1
            };

            //Act
            controller.Post(filme);

            //Assert
            service.Verify(x => x.Salvar(filme), Times.AtLeastOnce());
        }

        [TestMethod]
        public void put_call_obter()
        {
            //Arrange
            var controller = new FilmeController(service.Object);
            Model.Filme filme = new Model.Filme()
            {
                Id = 1,
                Nome = "teste",
                DateDeCriacao = DateTime.Now,
                GeneroID = 1
            };

            //Act
            controller.Put(filme);

            //Assert
            service.Verify(x => x.Atualizar(filme), Times.AtLeastOnce());
        }

        [TestMethod]
        public void delete_call_obter()
        {
            //Arrange
            var controller = new FilmeController(service.Object);
            Model.Filme filme = new Model.Filme()
            {
                Id = 1,
                Nome = "teste",
                DateDeCriacao = DateTime.Now,
                GeneroID = 1
            };

            //Act
            var result = controller.Delete(filme);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(OkResult));
        }
    }
}
