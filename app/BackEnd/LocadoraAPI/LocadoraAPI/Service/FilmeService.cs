using LocadoraAPI.Data;
using LocadoraAPI.Model;
using System;
using System.Collections.Generic;

namespace LocadoraAPI.Service
{
    public class FilmeService : IService<Filme>
    {
        public FilmeRepository Repo { get; }

        public FilmeService(FilmeRepository repo)
        {
            Repo = repo;
        }


        public IEnumerable<Filme> Obter()
        {
            return Repo.GetAll();
        }

        public Filme Obter(int id)
        {
            return Repo.GetSingle(id);
        }

        public void Salvar(Filme entity)
        {
            Repo.Add(entity);
            Repo.Commit();
        }

        public void Atualizar(Filme entity)
        {
            Repo.Update(entity);
            Repo.Commit();
        }

        public void Deletar(int entity)
        {
            Repo.Delete(entity);
            Repo.Commit();
        }

        public void RollBack(int entity)
        {
            Repo.Rollback(entity);
            Repo.Commit();
        }
    }
}
