using LocadoraAPI.Model;
using System.Collections.Generic;

namespace LocadoraAPI.Service
{
    public interface IServiceDapper
    {
        IEnumerable<Locacao> ObterLocacaos();
        Locacao ObterLocacao(int idLocacao);
        void SalvarLocacao(Locacao novoLocacao);        
        void DeletarLocacao(Locacao LocacaoDeletado);
        
    }
}