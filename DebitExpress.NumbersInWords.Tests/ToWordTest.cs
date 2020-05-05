using Shouldly;
using Xunit;

namespace DebitExpress.NumbersInWords.Tests
{
    public class ToWordTest
    {
        [Theory]
        [InlineData(0.0, Currency.Peso, "")]
        [InlineData(0.01, Currency.Peso, "One Cent")]
        [InlineData(0.10, Currency.Peso, "Ten Cents")]
        [InlineData(0.50, Currency.Peso, "Fifty Cents")]
        [InlineData(0.52, Currency.Peso, "Fifty-Two Cents")]
        [InlineData(1, Currency.Peso, "One Peso")]
        [InlineData(5, Currency.Peso, "Five Pesos")]
        [InlineData(50, Currency.Peso, "Fifty Pesos")]
        [InlineData(52, Currency.Peso, "Fifty-Two Pesos")]
        [InlineData(150, Currency.Peso, "One Hundred Fifty Pesos")]
        [InlineData(152, Currency.Peso, "One Hundred Fifty-Two Pesos")]
        [InlineData(1000, Currency.Peso, "One Thousand Pesos")]
        [InlineData(1500, Currency.Peso, "One Thousand, Five Hundred Pesos")]
        [InlineData(1520, Currency.Peso, "One Thousand, Five Hundred Twenty Pesos")]
        [InlineData(1525, Currency.Peso, "One Thousand, Five Hundred Twenty-Five Pesos")]
        [InlineData(1001525, Currency.Peso, "One Million, One Thousand, Five Hundred Twenty-Five Pesos")]
        [InlineData(1501525, Currency.Peso, "One Million, Five Hundred One Thousand, Five Hundred Twenty-Five Pesos")]
        [InlineData(1521525, Currency.Peso, "One Million, Five Hundred Twenty-One Thousand, Five Hundred Twenty-Five Pesos")]
        [InlineData(1521525.50, Currency.Peso, "One Million, Five Hundred Twenty-One Thousand, Five Hundred Twenty-Five Pesos and Fifty Cents")]
        public void WhenCurrencyIsPeso_ShouldReturnExpectedValue(decimal amount, Currency currency,
            string expectedResult)
        {
            var str = amount.ToWords(currency);

            str.ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(0.0, Currency.Peso, "")]
        [InlineData(0.01, Currency.None, "1/100")]
        [InlineData(0.10, Currency.None, "10/100")]
        [InlineData(0.50, Currency.None, "50/100")]
        [InlineData(0.52, Currency.None, "52/100")]
        [InlineData(1, Currency.None, "One")]
        [InlineData(5, Currency.None, "Five")]
        [InlineData(50, Currency.None, "Fifty")]
        [InlineData(52, Currency.None, "Fifty-Two")]
        [InlineData(150, Currency.None, "One Hundred Fifty")]
        [InlineData(152, Currency.None, "One Hundred Fifty-Two")]
        [InlineData(1500, Currency.None, "One Thousand, Five Hundred")]
        [InlineData(1520, Currency.None, "One Thousand, Five Hundred Twenty")]
        [InlineData(1525, Currency.None, "One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1001525, Currency.None, "One Million, One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1501525, Currency.None, "One Million, Five Hundred One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1521525, Currency.None, "One Million, Five Hundred Twenty-One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1521525.50d, Currency.None, "One Million, Five Hundred Twenty-One Thousand, Five Hundred Twenty-Five and 50/100")]
        public void WhenCurrencyIsNone_ShouldReturnExpectedValue(double amount, Currency currency,
            string expectedResult)
        {
            var str = amount.ToWords(currency);

            str.ShouldBe(expectedResult);
        }
        
        [Theory]
        [InlineData(1, Currency.None, "One")]
        [InlineData(5, Currency.None, "Five")]
        [InlineData(50, Currency.None, "Fifty")]
        [InlineData(52, Currency.None, "Fifty-Two")]
        [InlineData(150, Currency.None, "One Hundred Fifty")]
        [InlineData(152, Currency.None, "One Hundred Fifty-Two")]
        [InlineData(1500, Currency.None, "One Thousand, Five Hundred")]
        [InlineData(1520, Currency.None, "One Thousand, Five Hundred Twenty")]
        [InlineData(1525, Currency.None, "One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1001525, Currency.None, "One Million, One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1501525, Currency.None, "One Million, Five Hundred One Thousand, Five Hundred Twenty-Five")]
        [InlineData(1521525, Currency.None, "One Million, Five Hundred Twenty-One Thousand, Five Hundred Twenty-Five")]
        public void WhenUsingInteger_ShouldReturnExpectedValue(int amount, Currency currency,
            string expectedResult)
        {
            var str = amount.ToWords(currency);

            str.ShouldBe(expectedResult);
        }
    }
}