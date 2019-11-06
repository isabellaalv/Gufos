using System.Collections.Generic;
using System.Threading.Tasks;
using beckend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beckend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API 
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase 
    {
        
        GufosContext _contexto = new GufosContext();
        //busca todas tabelas
        // seria o select from, para buscar todas as tabelas do banco de dados
        // GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get()
        {

            var localizacoes = await _contexto.Localizacao.ToListAsync();

            if( localizacoes == null){
                return NotFound();
            }

            return localizacoes;

        }
        //busca tabela em especifica
        // seria o select from where do banco de dados
         // GET: api/Localizacao2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id)
        {

            var localizacoes = await _contexto.Localizacao.FindAsync(id);

            if( localizacoes == null){
                return NotFound();
            }

            return localizacoes;

        }

        // POST api/Localizacao

        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post (Localizacao localizacao){
            try{
                // Tratamos contra ataque de SQL Injection
                await _contexto.AddAsync(localizacao);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return localizacao;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Localizacao localizacao){

            //se o id do objeto não existir ele retorna 400
            if(id != localizacao.LocalizacaoId){
                return BadRequest();
            }

            _contexto.Entry(localizacao).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //verificamos se o objeto inserido realmente existe no banco

                var localizacao_valido = await _contexto.Localizacao.FindAsync(id);

                if(localizacao_valido == null){
                    return NotFound();
                }else{
                    throw;
                }

            }
            //NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/localizacao/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id){

            var localizacao = await _contexto.Localizacao.FindAsync(id);
            if(localizacao == null){
                return NotFound();
            }
            
            _contexto.Localizacao.Remove(localizacao);
            await _contexto.SaveChangesAsync();

            return localizacao;

        }

    }
}