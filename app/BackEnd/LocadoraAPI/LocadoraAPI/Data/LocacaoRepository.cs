using Dapper;
using LocadoraAPI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LocadoraAPI.Data
{
    public class LocacaoRepository
    {
        public virtual IEnumerable<Locacao> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Contexto"].ConnectionString))
            {
                var lookup = new Dictionary<int, Locacao>();
                return db.Query<Locacao, Filme, Locacao>(@"
                SELECT l.*, f.*
                FROM Locacao l
                INNER JOIN Locacao_Filme lf ON l.Id = lf.Locacao_Id
                INNER JOIN Filme f ON f.Id = lf.Filme_Id
                ", (l, f) =>
                {
                    Locacao locacao;
                    if (!lookup.TryGetValue(l.Id, out locacao))
                    {
                        lookup.Add(l.Id, locacao = l);
                    }
                    if (locacao.Filmes == null)
                        locacao.Filmes = new List<Filme>();
                    locacao.Filmes.Add(f);
                    return locacao;
                }).AsQueryable();
            }
        }

        public virtual int Count()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Contexto"].ConnectionString))
            {
                return db.Query("Select * From Locacao").Count();
            }
        }
        public Locacao GetSingle(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Contexto"].ConnectionString))
            {
                var lookup = new Dictionary<int, Locacao>();
                return db.Query<Locacao, Filme, Locacao>($@"
                SELECT l.*, f.*
                FROM Locacao l
                INNER JOIN Locacao_Filme lf ON l.Id = lf.Locacao_Id
                INNER JOIN Filme f ON f.Id = lf.Filme_Id
                where l.Id = {id}
                ", (l, f) =>
                {
                    Locacao locacao;
                    if (!lookup.TryGetValue(l.Id, out locacao))
                    {
                        lookup.Add(l.Id, locacao = l);
                    }
                    if (locacao.Filmes == null)
                        locacao.Filmes = new List<Filme>();
                    locacao.Filmes.Add(f);
                    return locacao;
                }).AsQueryable().First();
            }
        }

        public virtual void Add(Locacao entity)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Contexto"].ConnectionString))
            {
                var id = db.Query<int>(@"Insert into Locacao values(@cpfCliente, @dataDeCriacao);
                                        Select CAST(SCOPE_IDENTITY() as int);", new { cpfCliente = entity.CpfCliente, dataDeCriacao = entity.DataDeCriacao }).First();


                foreach (var filme in entity.Filmes)
                {
                    db.Execute("Insert into Locacao_Filme values(@idLocacao, @idFilme)", new { idLocacao = id, idFilme = filme.Id });
                }
            }
        }

        public virtual void Delete(Locacao entity)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Contexto"].ConnectionString))
            {
                db.Execute("delete from Locacao_Filme where Id = @id", new { id = entity.Id });
                db.Execute("delete from Locacao where Id = @id", new { id = entity.Id });
            }
        }
    }
}
