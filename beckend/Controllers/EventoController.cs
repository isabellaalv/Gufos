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
    public class EventoController : ControllerBase 
    {
        
        GufosContext _contexto = new GufosContext();
        //busca todas tabelas
        // seria o select from, para buscar todas as tabelas do banco de dados
        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Eventos>>> Get()
        {

            var eventos = await _contexto.Eventos.ToListAsync();

            if( eventos == null){
                return NotFound();
            }

            return eventos;

        }
        //busca tabela em especifica
        // seria o select from where do banco de dados
         // GET: api/Evento2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Eventos>> Get(int id)
        {

            var evento = await _contexto.Eventos.FindAsync(id);

            if( evento == null){
                return NotFound();
            }

            return evento;

        }

        // POST api/Evento

        [HttpPost]
        public async Task<ActionResult<Eventos>> Post (Eventos evento){
            try{
                // Tratamos contra ataque de SQL Injection
                await _contexto.AddAsync(evento);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return evento;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Eventos evento){

            //se o id do objeto não existir ele retorna 400
            if(id != evento.EventoId){
                return BadRequest();
            }

            _contexto.Entry(evento).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //verificamos se o objeto inserido realmente existe no banco

                var evento_valido = await _contexto.Eventos.FindAsync(id);

                if(evento_valido == null){
                    return NotFound();
                }else{
                    throw;
                }

            }
            //NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/evento/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Eventos>> Delete(int id){

            var evento = await _contexto.Eventos.FindAsync(id);
            if(evento == null){
                return NotFound();
            }
            
            _contexto.Eventos.Remove(evento);
            await _contexto.SaveChangesAsync();

            return evento;

        }

    }
}
