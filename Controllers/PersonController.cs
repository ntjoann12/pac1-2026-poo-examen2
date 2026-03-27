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
using PersonsApp.Services.Employees;

namespace PersonsApp.Controllers //Cunado se trabaja en api rest se trabaja de 2 formas, un minimal api y un controlador. Los controladores agrupan un conjunto de recursos que se exponen dentro de al api.
{
    [Route("api/employees")] //Decoradores
    [ApiController]
    public class PersonController : ControllerBase  //HERENCIA Todo lo que tiene controller base se lo hereda a PersonController
    {

        private readonly IEmployeeService _personService;
        public PersonController(IEmployeeService personService)
        {
            _personService = personService;
        }
        [HttpGet] //Funcion con Endpoint para personas; Devuelve lista de personas.
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _personService.GetAllEmployeesAsync(searchTerm, page, pageSize);
            return StatusCode(Response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _personService.GetOneEmployeeByIdAsync(id); //
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]//Funcion para creacion de persona (si no existe);
        public async Task<IActionResult> Create(EmployeeCreateDto dto)
        {

            var result = await _personService.CreateEmployeeAsync(dto);
            return StatusCode (result.StatusCode, result);
    
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update (int id, EmployeeEditDto dto)// dni es lo que busca a modificar, person es lo que modifica
        {
           var result = await _personService.EditEmployeeAsync(id, dto);
           return StatusCode(result.StatusCode, result);
        }
         [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)// dni es lo que busca a modificar, person es lo que modifica
        {
            //Busqueda persona a modificar (si lo encuentra)
           var result = await _personService.DeleteEmployeeAsync(id);
           return StatusCode(result.StatusCode, result);
        }
    }
}