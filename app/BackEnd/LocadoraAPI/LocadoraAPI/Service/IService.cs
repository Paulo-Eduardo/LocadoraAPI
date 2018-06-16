using LocadoraAPI.Model;
using System.Collections.Generic;

namespace LocadoraAPI.Service
{
    public interface IService<T>
    { 
        IEnumerable<T> Obter();
        T Obter(int id);
        void Salvar(T entity);
        void Atualizar(T entity);
        void Deletar(int entity);
    }
}