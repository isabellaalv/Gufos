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
    public class TipoUsuarioController : ControllerBase 
    {
        
        GufosContext _contexto = new GufosContext();
        //busca todas tabelas
        // seria o select from, para buscar todas as tabelas do banco de dados
        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {

            var tipoUsuarios = await _contexto.TipoUsuario.ToListAsync();

            if( tipoUsuarios == null){
                return NotFound();
            }

            return tipoUsuarios;

        }
        //busca tabela em especifica
        // seria o select from where do banco de dados
         // GET: api/TipoUsuario2
        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {

            var tipoUsuario = await _contexto.TipoUsuario.FindAsync(id);

            if( tipoUsuario == null){
                return NotFound();
            }

            return tipoUsuario;

        }

        // POST api/TipoUsuario

        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipoUsuario){
            try{
                // Tratamos contra ataque de SQL Injection
                await _contexto.AddAsync(tipoUsuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return tipoUsuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TipoUsuario tipoUsuario){

            //se o id do objeto não existir ele retorna 400
            if(id != tipoUsuario.TipoUsuarioId){
                return BadRequest();
            }

            _contexto.Entry(tipoUsuario).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //verificamos se o objeto inserido realmente existe no banco

                var tipoUsuario_valido = await _contexto.TipoUsuario.FindAsync(id);

                if(tipoUsuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }

            }
            //NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/tipoUsuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){

            var tipoUsuario = await _contexto.TipoUsuario.FindAsync(id);
            if(tipoUsuario == null){
                return NotFound();
            }
            
            _contexto.TipoUsuario.Remove(tipoUsuario);
            await _contexto.SaveChangesAsync();

            return tipoUsuario;

        }

    }
}  