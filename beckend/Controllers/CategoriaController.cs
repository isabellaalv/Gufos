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
    public class CategoriaController : ControllerBase 
    {
        
        GufosContext _contexto = new GufosContext();
        //busca todas tabelas
        // seria o select from, para buscar todas as tabelas do banco de dados
        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {

            var categorias = await _contexto.Categoria.ToListAsync();

            if( categorias == null)
            {
                return NotFound();
            }

            return categorias;

        }
        //busca tabela em especifica
        // seria o select from where do banco de dados
         // GET: api/Categoria2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {

            var categoria = await _contexto.Categoria.FindAsync(id);

            if( categoria == null)
            {
                return NotFound();
            }

            return categoria;

        }

        // POST api/Categoria

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post (Categoria categoria)
        {
            try{
                // Tratamos contra ataque de SQL Injection
                await _contexto.AddAsync(categoria);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return categoria;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Categoria categoria){

            //se o id do objeto não existir ele retorna 400
            if(id != categoria.CategoriaId){
                return BadRequest();
            }

            _contexto.Entry(categoria).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //verificamos se o objeto inserido realmente existe no banco

                var categoria_valido = await _contexto.Categoria.FindAsync(id);

                if(categoria_valido == null){
                    return NotFound();
                }else{
                    throw;
                }

            }
            //NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/categoria/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){

            var categoria = await _contexto.Categoria.FindAsync(id);
            if(categoria == null){
                return NotFound();
            }
            
            _contexto.Categoria.Remove(categoria);
            await _contexto.SaveChangesAsync();

            return categoria;

        }

    }
}
