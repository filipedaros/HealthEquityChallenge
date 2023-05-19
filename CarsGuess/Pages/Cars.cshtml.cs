using CarsGuess.Domain.Commands;
using CarsGuess.Domain.Model;
using CarsGuess.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace CarsGuess.Pages
{
    public class CarsModel : PageModel
    {
        #region Injected interfaces
        private readonly IMediator mediator;
        #endregion

        #region Page Properties
        public IEnumerable<Car> Cars { get; set; }
        public Car SelectedCar { get; set; }
        public string GuessResult { get; set; }
        public decimal PriceGuess { get; set; }
        #endregion

        public CarsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnGet()
        {
            var allCars = await mediator.Send(new ListAllCarsQueryRequest());
            Cars = allCars.Cars;
        }

        public async Task OnPostSelectedCar(int id)
        {
            //Cache this request or change front end to an Ajax request
            var allCars = await mediator.Send(new ListAllCarsQueryRequest());
            Cars = allCars.Cars;
            SelectedCar = allCars.Cars.FirstOrDefault(c => c.Id == id);
        }

        public async Task OnPostGuess(int id, decimal priceGuess)
        {
            var guessResult = await mediator.Send(new GuessCarPriceCommandRequest() { CarId = id, GuessPrice = priceGuess });

            //Cache this request or change front end to an Ajax request
            var allCars = await mediator.Send(new ListAllCarsQueryRequest());
            Cars = allCars.Cars;

            //Guess result
            GuessResult = guessResult.GoodGuess ? "Good job!!" : "Sory, not even close";
        }
    }
}
