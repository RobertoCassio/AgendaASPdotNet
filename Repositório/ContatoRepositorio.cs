using CadastroContatos.Data;
using CadastroContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Repositório
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList(); //Retorna os contatos do Banco já como lista
        }

        public ContatoModel IdList(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id); //Retorna todo mundo da tabela contato onde o id foi o id informado
        }

        public ContatoModel New(ContatoModel contato) //Grava no Banco de Dados
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }
        public ContatoModel Edit(ContatoModel contato)
        {
            ContatoModel contatoDB = IdList(contato.Id);
            if(contatoDB == null) throw new Exception("Houve um erro ao atualizar os dados");
            contatoDB.Nome = contato.Nome;
            contatoDB.Celular = contato.Celular;
            contatoDB.Email = contato.Email;

            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();
            return contatoDB;

        }

        public bool Delete(int id)
        {
            ContatoModel contatoDB = IdList(id);
            if (contatoDB == null) throw new Exception("Houve um erro ao deletar os dados!");

            _bancoContext.Contatos.Remove(contatoDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }   
}
