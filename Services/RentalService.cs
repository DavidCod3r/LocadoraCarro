using Locadora.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Services
{
    class RentalService
    {
        public double PricePerHour { get; set; }
        public double PricePerDay { get; set; }

        private BrazilTaxServices _brazilTaxServices = new BrazilTaxServices();
        public RentalService(double pricePerHour, double pricePerDay)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
        }

        public void ProcessInvoice(CarRental carRental)
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);

            double basicPayment = 0.0;
            if (duration.TotalHours <= 12.0)
            {
                basicPayment = PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }

            double tax = _brazilTaxServices.Tax(basicPayment);

            carRental.Invoice = new Invoice(basicPayment, tax);
 
        }

    }
}
