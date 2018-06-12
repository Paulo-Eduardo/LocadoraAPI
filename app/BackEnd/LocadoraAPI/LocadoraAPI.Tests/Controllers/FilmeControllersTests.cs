using LocadoraAPI.Controllers;
using LocadoraAPI.Service;
using Moq;
using System;
using Xunit;

namespace LocadoraAPI.Tests.Controllers
{
    public class FilmeControllersTests
    {
        private Mock<FilmeService> service = new Mock<FilmeService>();

        [Fact]
        public void get_call_obter()
        {
            //Arrange
            var controller = new FilmeController(service.Object);

            //Act
            controller.Get();

            //Assert
            service.Verify(x => x.ObterFilmes(), Times.AtLeastOnce());
        }

        [Fact]
        public void get_com_id_call_obter()
        {
            //Arrange
            var controller = new FilmeController(service.Object);

            //Act
            controller.Get(1);

            //Assert
            service.Verify(x => x.ObterFilme(1), Times.AtLeastOnce());
        }

        [Fact]
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
            service.Verify(x => x.SalvarFilme(filme), Times.AtLeastOnce());
        }

        [Fact]
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
            service.Verify(x => x.AtualizarFilme(filme), Times.AtLeastOnce());
        }

        [Fact]
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
            controller.Delete(filme);

            //Assert
            service.Verify(x => x.DeletarFilme(filme), Times.AtLeastOnce());
        }
    }
}
