using LocadoraAPI.Data;
using LocadoraAPI.Model;
using System.Collections.Generic;

namespace LocadoraAPI.Service
{
    public class GeneroService : IService<Genero>
    {
        public GeneroRepository Repo { get; }

        public GeneroService(GeneroRepository repo)
        {
            Repo = repo;
        }

        public IEnumerable<Genero> Obter()
        {
            return Repo.GetAll();
        }

        public Genero Obter(int id)
        {
            return Repo.GetSingle(id);
        }

        public void Salvar(Genero entity)
        {
            Repo.Add(entity);
            Repo.Commit();
        }

        public void Atualizar(Genero entity)
        {
            Repo.Update(entity);
            Repo.Commit();
        }

        public void Deletar(Genero entity)
        {
            Repo.Delete(entity);
            Repo.Commit();
        }
    }
}
