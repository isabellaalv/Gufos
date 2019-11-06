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
    public class PresencaController : ControllerBase 
    {
        
        GufosContext _contexto = new GufosContext();
        //busca todas tabelas
        // seria o select from, para buscar todas as tabelas do banco de dados
        // GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get()
        {

            var presencas = await _contexto.Presenca.ToListAsync();

            if( presencas == null){
                return NotFound();
            }

            return presencas;

        }
        //busca tabela em especifica
        // seria o select from where do banco de dados
         // GET: api/Presenca2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id)
        {

            var presenca = await _contexto.Presenca.FindAsync(id);

            if( presenca == null){
                return NotFound();
            }

            return presenca;

        }

        // POST api/Presenca

        [HttpPost]
        public async Task<ActionResult<Presenca>> Post (Presenca presenca){
            try{
                // Tratamos contra ataque de SQL Injection
                await _contexto.AddAsync(presenca);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return presenca;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presenca presenca){

            //se o id do objeto não existir ele retorna 400
            if(id != presenca.PresencaId){
                return BadRequest();
            }

            _contexto.Entry(presenca).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //verificamos se o objeto inserido realmente existe no banco

                var presenca_valido = await _contexto.Presenca.FindAsync(id);

                if(presenca_valido == null){
                    return NotFound();
                }else{
                    throw;
                }

            }
            //NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/presenca/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id){

            var presenca = await _contexto.Presenca.FindAsync(id);
            if(presenca == null){
                return NotFound();
            }
            
            _contexto.Presenca.Remove(presenca);
            await _contexto.SaveChangesAsync();

            return presenca;

        }

    }
}