using CarsGuess.Data;
using CarsGuess.Domain.Model;
using MediatR;

namespace CarsGuess.Domain.Commands
{
    /// <summary>
    /// We could also use this class to query a service somewhere else, using http requests
    /// </summary>
    public class GuessCarPriceCommandRequest : IRequest<GuessCarPriceCommandResponse>
    {
        public int CarId { get; set; }
        public decimal GuessPrice { get; set; }
    }

    public class GuessCarPriceCommandResponse
    {
        public bool GoodGuess { get; set; }
    }

    public class GuessCarPriceCommandHandler : IRequestHandler<GuessCarPriceCommandRequest, GuessCarPriceCommandResponse>
    {
        private readonly IRepository<Car> repository;

        public GuessCarPriceCommandHandler(IRepository<Car> repository)
        {
            this.repository = repository;
        }

        public async Task<GuessCarPriceCommandResponse> Handle(GuessCarPriceCommandRequest request, CancellationToken cancellationToken)
        {
            //Add validations for the request here using Fluent

            var car = await repository.Get(request.CarId);
            if (car == null)
                throw new ArgumentException($"Car {request.CarId} not found");

            if(Math.Abs(request.GuessPrice - car.Price) <= 5000)
            {
                return new GuessCarPriceCommandResponse() { GoodGuess = true };
            }


            return new GuessCarPriceCommandResponse() { GoodGuess = false };
        }
    }
}
