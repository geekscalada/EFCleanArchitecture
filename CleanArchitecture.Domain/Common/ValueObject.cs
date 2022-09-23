namespace CleanArchitecture.Domain.Common
{

    // una clase ValueObject es un requerimiento básico para la arquitectura DDD.
    // es un tipo inmutable que solo es distinguido por el valor de sus propiedades
    // ¿como maneras una direccion postal? La unión de esos valores por ejemplo zipcode numero y puerta
    // es unica
    // si un día te mudas, cambas toda la dirección. Es decir el value Object es inmutable, no se 
    // cambia, se crea una nueva. 
    // en una misma dirección pueden vivir 2 personas, es reutilizable.
    // un value object puede estar vinculado a más de una entidad del clietne
    // más info en la doc oficial de Microsoft. 
    // esta es la implementación que recomienda microsoft


    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
        // Other utility methods
    }
}
