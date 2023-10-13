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

    }
}
