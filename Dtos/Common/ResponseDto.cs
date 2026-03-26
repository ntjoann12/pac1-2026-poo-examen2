
namespace PersonsApp.Dtos.Common
{
    public class ResponseDto<T> //Tipo generico (puede ser un string, fecha, algo x)
    {
        public int StatusCode { get; set; } //Codigo de respuesta 
        public string Message { get; set; } //Mensaje de la respuesta ("")
        public bool Status { get; set; } // V = Para respuestas sin errores SINO !V o F
        public T Data { get; set; } //  La T hace referencia a que puede ser generico el tipo de datos
    }
}