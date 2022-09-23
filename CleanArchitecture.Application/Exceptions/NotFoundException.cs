namespace CleanArchitecture.Application.Exceptions
{

    // Heredamos del propio system
    public class NotFoundException : ApplicationException
    {

        // Constructor
        // Key hace referencia al ID
        // Con base estamos pasando estos valores hacia la clase padre, Applicationexception
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key})  no fue encontrado")
        {
        }
    }
}
