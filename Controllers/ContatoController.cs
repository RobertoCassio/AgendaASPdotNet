using CadastroContatos.Models;
using CadastroContatos.Repositório;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
         List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(); //Chama a lista feita no contato repositório para retornar no Index
            return View(contatos);
        }
        public IActionResult New()
        {

            return View();
        }
        public IActionResult Edit(int id)
        {
            ContatoModel contato = _contatoRepositorio.IdList(id);
            return View(contato);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Delete(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                    return RedirectToAction("Index"); //Retorna para o index ao encerrar
                }
                else
                {
                    TempData["MensagemErro"] = $"Falha ao apagar contato";
                    return RedirectToAction("Index");
                }
                
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao apagar contato, mais detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult DeleteConfirm(int id) 
        {
            ContatoModel contato = _contatoRepositorio.IdList(id);
            return View(contato);
        }
        // Métodos Acima são Get
        //Métodos Abaixo são Post -> Recebem e cadastram informação

        [HttpPost]
        public IActionResult New(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.New(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao cadastrar contato, mais detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Edit(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Edit", contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao atualizar contato, mais detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
            }
    }
}
