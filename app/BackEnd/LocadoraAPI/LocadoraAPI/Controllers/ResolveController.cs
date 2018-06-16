using LocadoraAPI.Controllers;
using LocadoraAPI.Data;
using LocadoraAPI.Service;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace LocadoraAPI
{
    public class ResolveController : IDependencyResolver
    {
        private static readonly Contexto contexto = new Contexto();
        private static readonly FilmeRepository filmeRepo = new FilmeRepository(contexto);
        private static readonly FilmeService filmeService = new FilmeService(filmeRepo);

        private static GeneroRepository generoRepo = new GeneroRepository(contexto);
        private GeneroService generoService = new GeneroService(generoRepo);
        private static LocacaoRepository locacaoRepo = new LocacaoRepository();
        private LocacaoService locacaoService = new LocacaoService(locacaoRepo);

        public object GetService(Type serviceType)
        {
            contexto.Database.CreateIfNotExists();
            if (serviceType == typeof(FilmeController))
                return new FilmeController(filmeService);
            else if (serviceType == typeof(GeneroController))
                return new GeneroController(generoService);
            else if (serviceType == typeof(LocacaoController))
                return new LocacaoController(locacaoService);
            else if (serviceType == typeof(AccountController))
                return new AccountController();
            else
                return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }


        public void Dispose()
        {

        }
    }
}