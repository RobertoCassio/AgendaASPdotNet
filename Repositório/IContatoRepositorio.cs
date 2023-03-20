using CadastroContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Repositório
{
    public interface IContatoRepositorio
    {
        ContatoModel IdList(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel New(ContatoModel contato);
        ContatoModel Edit(ContatoModel contato);

        bool Delete(int id);
    }
}
