using SplittingBillsLibrary;
namespace splittingBillsTests;

[TestClass]
public class BillSplitterTests
{
    [TestMethod]
    public void SplitAmount_ValidInput_ReturnsCorrectResult()
    {
        // Test that the SplitAmount method returns the correct amount per person for a valid input.
        BillSplitter splitter = new BillSplitter();
        decimal totalAmount = 100;
        int numberOfPeople = 5;
        decimal expected = 20;

        decimal result = splitter.SplitAmount(totalAmount, numberOfPeople);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SplitAmount_ZeroPeople_ThrowsArgumentException()
    {
        // Test that the SplitAmount method throws an ArgumentException when the number of people is zero.
        BillSplitter splitter = new BillSplitter();
        decimal totalAmount = 100;
        int numberOfPeople = 0;

        splitter.SplitAmount(totalAmount, numberOfPeople);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SplitAmount_WhenNumberOfPeopleIsNegative_ShouldThrowArgumentException()
    {
        // Test that the SplitAmount method throws an ArgumentException when the number of people is negative.
        BillSplitter splitter = new BillSplitter();
        decimal totalAmount = 100.0m;
        int numberOfPeople = -2;

        splitter.SplitAmount(totalAmount, numberOfPeople);
    }

    [TestMethod]
    public void CalculateTipPerPerson_ValidInput_ShouldCalculateTipPerPerson()
    {
        // Test that the CalculateTipPerPersonForMeals method correctly calculates the tip per person for a valid input.
        BillSplitter splitter = new BillSplitter();
        // Arrange
        var mealCosts = new Dictionary<string, decimal>
        {
            { "John", 40.0m },
            { "Emily", 30.0m },
            { "Michael", 50.0m }
        };
        float tipPercentage = 15.0f;

        var expectedTipPerPerson = new Dictionary<string, decimal>
        {
            { "John", 6.0m },
            { "Emily", 4.5m },
            { "Michael", 7.5m }
        };

        // Round both the expected and actual results to 2 decimal places
        var roundedExpected = expectedTipPerPerson.ToDictionary(kvp => kvp.Key, kvp => Math.Round(kvp.Value, 2));
        var result = splitter.CalculateTipPerPersonForMeals(mealCosts, tipPercentage);
        var roundedResult = result.ToDictionary(kvp => kvp.Key, kvp => Math.Round(kvp.Value, 2));

        CollectionAssert.AreEqual(roundedExpected, roundedResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTipPerPerson_EmptyMealCostsDictionary_ShouldThrowArgumentException()
    {
        // Test that the CalculateTipPerPersonForMeals method throws an ArgumentException when the meal costs dictionary is empty.
        BillSplitter splitter = new BillSplitter();
        var mealCosts = new Dictionary<string, decimal>();
        float tipPercentage = 10.0f;

        splitter.CalculateTipPerPersonForMeals(mealCosts, tipPercentage);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTipPerPerson_InvalidTipPercentage_ShouldThrowArgumentException()
    {
        // Test that the CalculateTipPerPersonForMeals method throws an ArgumentException when the tip percentage is invalid.
        BillSplitter splitter = new BillSplitter();
        // Arrange
        var mealCosts = new Dictionary<string, decimal>
        {
            { "Sarah", 40.0m }
        };
        float tipPercentage = -5.0f;

        splitter.CalculateTipPerPersonForMeals(mealCosts, tipPercentage);
    }

    [TestMethod]
    public void CalculateTipPerPersonForBill_WhenNumberOfPatronsIsGreaterThanZero_ShouldReturnCorrectValue()
    {
        // Test that the CalculateTipPerPersonForBill method returns the correct tip per person when the number of patrons is greater than zero.
        BillSplitter splitter = new BillSplitter();
        decimal price = 100.0m;
        int numberOfPatrons = 5;
        float tipPercentage = 15.0f;
        decimal expected = 3.0m; // 15% tip on $100, split among 5 people

        // Act
        decimal result = splitter.CalculateTipPerPersonForBill(price, numberOfPatrons, tipPercentage);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTipPerPersonForBill_WhenNumberOfPatronsIsZero_ShouldThrowArgumentException()
    {
        // Test that the CalculateTipPerPersonForBill method throws an ArgumentException when the number of patrons is zero.
        BillSplitter splitter = new BillSplitter();
        decimal price = 100.0m;
        int numberOfPatrons = 0;
        float tipPercentage = 15.0f;

        splitter.CalculateTipPerPersonForBill(price, numberOfPatrons, tipPercentage);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTipPerPersonForBill_WhenTipPercentageIsOutOfRange_ShouldThrowArgumentException()
    {
        // Test that the CalculateTipPerPersonForBill method throws an ArgumentException when the tip percentage is out of the valid range.
        BillSplitter splitter = new BillSplitter();
        decimal price = 100.0m;
        int numberOfPatrons = 4;
        float tipPercentage = -5.0f;

        splitter.CalculateTipPerPersonForBill(price, numberOfPatrons, tipPercentage);
    }
}
