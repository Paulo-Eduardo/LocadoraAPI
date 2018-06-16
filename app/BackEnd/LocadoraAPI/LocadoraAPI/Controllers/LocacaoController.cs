using LocadoraAPI.Model;
using LocadoraAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LocadoraAPI.Controllers
{
    public class LocacaoController : ApiController
    {
        public IServiceDapper LocacaoService { get; }

        public LocacaoController(IServiceDapper locacaoService)
        {
            LocacaoService = locacaoService;
        }


        [Authorize]
        public IHttpActionResult Get()
        {
            try
            {
                var result = LocacaoService.ObterLocacaos();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        public Locacao Get(int id)
        {
            return LocacaoService.ObterLocacao(id);
        }

        [Authorize]
        public IHttpActionResult Post(Locacao Locacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                LocacaoService.SalvarLocacao(Locacao);
                return Ok("Sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        public IHttpActionResult Delete(Locacao Locacao)
        {
            try
            {
                LocacaoService.DeletarLocacao(Locacao);
                return Ok("Sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
