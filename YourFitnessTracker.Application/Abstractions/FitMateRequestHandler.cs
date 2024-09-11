using MediatR;
using YourFitnessTracker.Infrastructure.Exceptions;

namespace YourFitnessTracker.Application.Abstractions
{
    internal abstract class FitMateRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected static async Task<IEnumerable<T>> TryGetCollectionAsync<T>(Task<IEnumerable<T>> action) where T : class
        {
            IEnumerable<T> result;

            try
            {
                result = await action;
            }
            catch (EntityNotFoundException)
            {
                result = Enumerable.Empty<T>();
            }

            return result;
        }

        protected static async Task<T?> TryGetModelAsync<T>(Task<T> action) where T : class
        {
            T? result;

            try
            {
                result = await action;
            }
            catch (EntityNotFoundException)
            {
                result = default;
            }

            return result;
        }
    }
}