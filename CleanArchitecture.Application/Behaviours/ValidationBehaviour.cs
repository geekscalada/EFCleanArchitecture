using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;


namespace CleanArchitecture.Application.Behaviours
{

    // Aquí TRequest y TResponse llevan la T porque son genéricos. 
    // El where es porque nos obliga a definir el tipo de dato para el TRequest.
    // Entonces con el where le decimos que el TRequest es de tipo IRequest casteando a un TResponse
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            // Miramos si tenemos alguna validación escrita en nuestro código
            if (_validators.Any())
            { 
                var context = new ValidationContext<TRequest>(request);

                // Aquí nos traemos las validaciones. Y las ejecutamos, estamos en el tubo, estamos
                // ejecutando las validaciones en el tubo. 

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            
            }

            return await next();
        }
    }
}
