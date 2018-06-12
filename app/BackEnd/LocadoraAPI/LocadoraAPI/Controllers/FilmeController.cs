using LocadoraAPI.Model;
using LocadoraAPI.Service;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LocadoraAPI.Controllers
{
    public class FilmeController : ApiController
    {

        public IService<Filme> FilmeService { get; }

        public FilmeController(IService<Filme> service)
        {
            FilmeService = service;
        }

        [Authorize]
        public IEnumerable<Filme> Get()
        {
            return FilmeService.Obter();
        }

        [Authorize]
        public Filme Get(int id)
        {
            return FilmeService.Obter(id);
        }

        [Authorize]
        public IHttpActionResult Post(Filme filme)
        {
            try
            {
                FilmeService.Salvar(filme);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        public IHttpActionResult Put(Filme filme)
        {
            try
            {
                FilmeService.Atualizar(filme);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        public IHttpActionResult Delete(Filme filme)
        {
            try
            {
                FilmeService.Deletar(filme);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
