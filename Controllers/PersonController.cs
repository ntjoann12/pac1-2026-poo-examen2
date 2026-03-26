using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;
using PersonsApp.Services.Persons;

namespace PersonsApp.Controllers //Cunado se trabaja en api rest se trabaja de 2 formas, un minimal api y un controlador. Los controladores agrupan un conjunto de recursos que se exponen dentro de al api.
{
    [Route("api/person")] //Decoradores
    [ApiController]
    public class PersonController : ControllerBase  //HERENCIA Todo lo que tiene controller base se lo hereda a PersonController
    {

        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet] //Funcion con Endpoint para personas; Devuelve lista de personas.
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _personService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(Response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _personService.GetOneByIdAsync(id); //
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]//Funcion para creacion de persona (si no existe);
        public async Task<IActionResult> Create(PersonCreateDto dto)
        {

            var result = await _personService.CreateAsync(dto);
            return StatusCode (result.StatusCode, result);
    
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update (string id, PersonEditDto dto)// dni es lo que busca a modificar, person es lo que modifica
        {
           var result = await _personService.EditAsync(id, dto);
           return StatusCode(result.StatusCode, result);
        }
         [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)// dni es lo que busca a modificar, person es lo que modifica
        {
            //Busqueda persona a modificar (si lo encuentra)
           var result = await _personService.DeleteAsync(id);
           return StatusCode(result.StatusCode, result);
        }
    }
}