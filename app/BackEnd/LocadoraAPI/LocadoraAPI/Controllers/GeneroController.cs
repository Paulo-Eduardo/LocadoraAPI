using LocadoraAPI.Model;
using LocadoraAPI.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LocadoraAPI.Controllers
{
    public class GeneroController : ApiController
    {
        public IService<Genero> GeneroService { get; }

        public GeneroController(IService<Genero> generoService)
        {
            GeneroService = generoService;
        }


        [Authorize]
        public IEnumerable<Genero> Get()
        {
            return GeneroService.Obter();
        }

        public Genero Get(int id)
        {
            return GeneroService.Obter(id);
        }

        [Authorize]
        public IHttpActionResult Post(Genero Genero)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                GeneroService.Salvar(Genero);
                return Ok("Sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        public IHttpActionResult Put(Genero Genero)
        {
            try
            {
                GeneroService.Atualizar(Genero);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize]
        public IHttpActionResult Delete(int[] Generos)
        {
            try
            {
                foreach (var item in Generos)
                {
                    GeneroService.Deletar(item);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
