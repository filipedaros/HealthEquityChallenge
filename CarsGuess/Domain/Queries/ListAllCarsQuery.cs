using CarsGuess.Data;
using CarsGuess.Domain.Commands;
using CarsGuess.Domain.Model;
using MediatR;

namespace CarsGuess.Domain.Queries
{
    /// <summary>
    /// We could also use this class to query a service somewhere else, using http requests
    /// </summary>
    public class ListAllCarsQueryRequest : IRequest<ListAllCarsQueryResponse>
    {
    }

    public class ListAllCarsQueryResponse
    {
        public IEnumerable<Car> Cars { get; set; }
    }

    public class ListAllCarsQueryHandler : IRequestHandler<ListAllCarsQueryRequest, ListAllCarsQueryResponse>
    {
        private readonly IRepository<Car> repository;

        public ListAllCarsQueryHandler(IRepository<Car> repository)
        {
            this.repository = repository;
        }

        public async Task<ListAllCarsQueryResponse> Handle(ListAllCarsQueryRequest request, CancellationToken cancellationToken)
        {
            //Add validations for the request here using Fluent

            var cars = await repository.GetAll();
            return new ListAllCarsQueryResponse { Cars= cars };
        }
    }

}
