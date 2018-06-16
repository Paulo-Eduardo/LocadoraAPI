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
    public class LocacaoControllerTests
    {
        private Mock<IServiceDapper> service;

        public LocacaoControllerTests()
        {
            service = new Mock<IServiceDapper>();
        }

        [TestMethod]
        public void get_call_obter()
        {
            //Arrange
            var controller = new LocacaoController(service.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void post_call_obter()
        {
            //Arrange
            var controller = new LocacaoController(service.Object);

            var locacao = new Locacao()
            {
                CpfCliente = "08595979979",
                DataDeCriacao = DateTime.Now,
            };            

            //Act
            controller.Post(locacao);

            //Assert
            service.Verify(x => x.SalvarLocacao(locacao), Times.AtLeastOnce());
        }

        [TestMethod]
        public void delete_call_obter()
        {
            //Arrange
            var controller = new LocacaoController(service.Object);
            var locacao = new Locacao()
            {
                Id = 1,
                CpfCliente = "08595979979",
                DataDeCriacao = DateTime.Now,
            };


            //Act
            var result = controller.Delete(locacao);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(OkResult));
        }
    }
}
