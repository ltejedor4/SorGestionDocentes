using System.Net;

namespace Sor.Models
{
    /// <summary>
    /// Objeto que se configura dependiendo del resultado de la ApiApp
    /// </summary>
    public class ResultApi
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Almacena el objeto ResultApiApp que devuelve la ApiApp
        /// </summary>
        public object Result { get; set; }
        public HttpStatusCode EstadoPeticion { get; set; }

    }

    /// <summary>
    /// Objeto de respuesta que se recibe al consultar la ApiApp
    /// </summary>
    public class ResultApiApp
    {

        public int Result { get; set; }
        public string ProgrammerMessage { get; set; }
        public string UserMessage { get; set; }
        public object ResponseObject { get; set; }
    }

    /// <summary>
    /// Clase para retornar estado de una petición, validación y un mensaje.
    /// </summary>
    public class ValidationCustomResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
