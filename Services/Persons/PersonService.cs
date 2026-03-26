using Microsoft.EntityFrameworkCore;
using PersonsApp.Constants;
using PersonsApp.DataBase;
using PersonsApp.Dtos.Common;
using PersonsApp.Dtos.Countries;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;
using PersonsApp.Mappers;

namespace PersonsApp.Services.Persons
{
    
    public class PersonService : IPersonService //Clase : Interfaz (contrato que define las reglas de como se va a comportar PersonService)
    {
        private readonly PersonsDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public PersonService(PersonsDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize"); //Hace conversion a entero
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit"); //Hace conversion a entero

        }
        public async Task<ResponseDto<PageDto<List<PersonDto>>>> GetPageAsync(string searchTerm = "", int  page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE  : pageSize;//Si no viene nada en "pageSize" pone el valor por defecto, sino, agarra el valor asignado en pageSize
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT  : pageSize;//Si no viene nada en "pageSize" pone el valor por defecto, sino, agarra el valor asignado en pageSize

            int startIndex = (page - 1 ) * pageSize; // define pagina de inicio

            IQueryable<PersonEntity> peopleQuery = _context.Persons.Include(p => p.Country );

            if(!string.IsNullOrEmpty(searchTerm))
            {
                peopleQuery = peopleQuery.Where( x => (x.DNI + " " + x.FirstName + " " + x.LastName).Contains(searchTerm) ); //() es para concatenear
            }
            int totalRows = await peopleQuery .CountAsync(); //Muestra el total de registros
            var personEntity = await peopleQuery 
                .OrderBy(x => x.FirstName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();
            
            var personDto = PersonMapper.ListEntityToListDto(personEntity);

            // respuesta
            return new ResponseDto<PageDto<List<PersonDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<PersonDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows, 
                    TotalPages = (int)Math.Ceiling((double)totalRows/pageSize), //Cuando se pone entre parentesis, el tipo de dato que este en medio, se va a convertir al dato que escojamos
                    Items = PersonMapper.ListEntityToListDto(personEntity),
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && 
                    page < (int)Math.Ceiling((double)totalRows/pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }
        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(string id)
        {
            Console.WriteLine(id);
            var personEntity = await _context.Persons.Include(p => p.Country).FirstOrDefaultAsync(p => p.Id == id ); //Objeto = espera al contexto de la tabla personas (El primero  que encuentre de manera asincrona)

            //Validcaciones
            if (personEntity is null)
            {
                return new ResponseDto<PersonDto>
                {
                  StatusCode = HttpStatusCode.NOT_FOUND,
                  Message =HttpMessageResponse.REGISTER_NOT_FOUND, 
                  Status = false
                };
            }

        return new ResponseDto<PersonDto>
        {
            StatusCode = HttpStatusCode.OK,
            Message = HttpMessageResponse.REGISTER_FOUND,
            Status = true,
            Data = new PersonDto //MAPEO PARA PODER USAR PERSON DTO
            {
                Id = personEntity.Id,
                DNI = personEntity.DNI,
                FirstName = personEntity.FirstName,
                LastName = personEntity.LastName,
                BirthDate = personEntity.BirthDate,
                Gender = personEntity.Gender,
                Country = new CountryOneDto
                {
                    Id = personEntity.CountryId,
                    Name = personEntity.Country.Name
                }
            }
        };
        
    }
    public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)

    {
        PersonEntity personEntity = PersonMapper.CreateDtoToEntity(dto);

        _context.Persons.Add(personEntity);
        await _context.SaveChangesAsync();
            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = new PersonActionResponseDto
                {
                    Id = personEntity.Id //Esto es lo que el bruno devuelve
                }
            };
    }


    public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(string id, PersonEditDto dto)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

                if (personEntity is null)
                {
                    return new ResponseDto<PersonActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.NOT_FOUND,
                        Status = false,
                        Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    };
                }

                PersonMapper.EditDtoToEntity(personEntity, dto);

                await _context.SaveChangesAsync();

                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = HttpMessageResponse.REGISTER_UPDATED,
                    Data = new PersonActionResponseDto
                    {
                        Id = id
                    }
                };
        }
       
       public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync (string id)
        {
           var personEntity = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                };
            }

            _context.Persons.Remove(personEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new PersonActionResponseDto
                {
                    Id = id
                }
            };
         }
     }
}
