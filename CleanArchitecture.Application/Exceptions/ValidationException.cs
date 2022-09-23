using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        // Constructor sin parámetros y sorbecargamos al padre con base
        public ValidationException() : base("Se presentaron uno o mas errores de validacion")
        {
            Errors = new Dictionary<string, string[]>();
        }


        // Otro constructor
        // Usamos el ValidationFailures que es de la libreria FluentValidation.Results;
        public ValidationException(IEnumerable<ValidationFailure>  failures) : this()
        {
            // Que nos devuelva la propiedad y el error de mensaje
            // y estos dos valores que vayan al diccionario, que llenen el diccionario con el ToDiccionary
            // failureGroup representa a cada elemento de este diccionario
            // seteamos a key y después seteamos a un Array
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            //Entonces lo que ocurre es que las excepciones van a ser leídas por esta clase
            // serán inicializadas con este contructor de abajo haciendo que las excepciones se almacenen
            // en la propiedad errors que es finalmente el valor que devolveremos al cliente. 

        }


        // Se pueden disparar varias excepciones a la vez.
        // Creamos un diccionario para almacenar varias

        // Aquí el IDictionari tendra como key una string y como value un string[]

        public IDictionary<string, string[]> Errors { get; }

    }
}
