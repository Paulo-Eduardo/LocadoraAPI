using LocadoraAPI.Data;
using LocadoraAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraAPI.Service
{
    public class LocacaoService : IServiceDapper
    {
        public LocacaoRepository Repo { get; }

        public LocacaoService(LocacaoRepository repo)
        {
            Repo = repo;
        }

        public IEnumerable<Locacao> ObterLocacaos()
        {
            return Repo.GetAll();
        }

        public Locacao ObterLocacao(int idLocacao)
        {
            return Repo.GetSingle(idLocacao);
        }

        public void SalvarLocacao(Locacao novoLocacao)
        {
            Repo.Add(novoLocacao);
        }

        public void DeletarLocacao(Locacao LocacaoDeletado)
        {
            Repo.Delete(LocacaoDeletado);
        }
    }
}
