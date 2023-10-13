using System;
using System.Collections.Generic;
using System.Linq;

namespace SplittingBillsLibrary
{
    public class BillSplitter
    {
        public decimal SplitAmount(decimal totalAmount, int numberOfPeople)
        {
            if (numberOfPeople <= 0)
                throw new ArgumentException("Number of people must be greater than zero.");

            return totalAmount / numberOfPeople;
        }

        public Dictionary<string, decimal> CalculateTipPerPersonForMeals(Dictionary<string, decimal> mealCosts, float tipPercentage)
        {
            if (mealCosts == null || mealCosts.Count == 0)
                throw new ArgumentException("Meal costs dictionary must not be empty.");
            if (tipPercentage < 0 || tipPercentage > 100)
                throw new ArgumentException("Tip percentage must be between 0 and 100.");

            decimal sumWx = 0; // Sum of (meal cost * tip percentage)
            decimal sumW = 0;  // Sum of meal cost

            // Calculate the sums
            foreach (var kvp in mealCosts)
            {
                sumWx += kvp.Value * (decimal)tipPercentage / 100;
                sumW += kvp.Value;
            }

            // Calculate the tip per person using the weighted average formula
            Dictionary<string, decimal> tipPerPerson = new Dictionary<string, decimal>();
            foreach (var kvp in mealCosts)
            {
                decimal personTip = (kvp.Value / sumW) * sumWx;
                tipPerPerson.Add(kvp.Key, personTip);
            }

            return tipPerPerson;
        }

        public decimal CalculateTipPerPersonForBill(decimal price, int numberOfPatrons, float tipPercentage)
        {
            if (numberOfPatrons <= 0)
                throw new ArgumentException("Number of patrons must be greater than zero.");
            if (tipPercentage < 0 || tipPercentage > 100)
                throw new ArgumentException("Tip percentage must be between 0 and 100.");

            decimal tipAmount = (price * (decimal)tipPercentage / 100);
            return tipAmount / numberOfPatrons;
        }

    }
}
