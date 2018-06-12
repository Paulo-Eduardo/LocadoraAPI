using System;
using System.Web.Http.Results;
using LocadoraAPI.Controllers;
using LocadoraAPI.Model;
using LocadoraAPI.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LocadoraAPI.Tests.Controllers
{
    [TestClass]
    public class GeneroControllerTests
    {
        private Mock<IService<Genero>> service;

        public GeneroControllerTests()
        {
            service = new Mock<IService<Genero>>();
        }

        [TestMethod]
        public void get_call_obter()
        {
            //Arrange
            var controller = new GeneroController(service.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void get_com_id_call_obter()
        {
            //Arrange
            Genero genero = new Genero()
            {
                Id = 1,
                Nome = "teste",
                DataDeCriacao = DateTime.Now
            };
            service.Setup(x => x.Obter(1)).Returns(genero);

            var controller = new GeneroController(service.Object);

            //Act
            var result = controller.Get(1);

            //Assert
            Assert.AreEqual(genero, result);
        }

        [TestMethod]
        public void post_call_obter()
        {
            //Arrange
            var controller = new GeneroController(service.Object);
            Genero genero = new Genero()
            {
                Nome = "teste",
                DataDeCriacao = DateTime.Now
            };

            //Act
            controller.Post(genero);

            //Assert
            service.Verify(x => x.Salvar(genero), Times.AtLeastOnce());
        }

        [TestMethod]
        public void put_call_obter()
        {
            //Arrange
            var controller = new GeneroController(service.Object);
            Genero genero = new Genero()
            {
                Id = 1,
                Nome = "teste",
                DataDeCriacao = DateTime.Now
            };

            //Act
            controller.Put(genero);

            //Assert
            service.Verify(x => x.Atualizar(genero), Times.AtLeastOnce());
        }

        [TestMethod]
        public void delete_call_obter()
        {
            //Arrange
            var controller = new GeneroController(service.Object);
            Genero genero = new Genero()
            {
                Id = 1,
                Nome = "teste",
                DataDeCriacao = DateTime.Now
            };

            //Act
            var result = controller.Delete(genero);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(OkResult));
        }
    }
}
