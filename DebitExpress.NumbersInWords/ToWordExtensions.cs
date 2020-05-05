using System;
using System.Collections.Generic;
using System.Globalization;

namespace DebitExpress.NumbersInWords
{
    public static class ToWordExtensions
    {
        public static string ToWords(this decimal value, Currency currency)
        {
            var fraction = Math.Abs(Math.Round(value, 2));
            var input = Math.Truncate(fraction);
            var decimals = (int)(fraction % 1.0m * 100);

            var strWords = ConvertIntegralPart(currency, input);
            strWords += ConvertDecimalPart(currency, strWords, decimals);
            return strWords;
        }

        private static string ConvertIntegralPart(Currency currency, decimal input)
        {
            if (input <= 0) return string.Empty;

            var strWords = $"{GetWords(input.ToString(CultureInfo.InvariantCulture))}{GetCurrencyWord(currency)}";

            if (input > 1 && currency != Currency.None)
                strWords += "s";
            return strWords;
        }

        private static string ConvertDecimalPart(Currency currency, string baseString, decimal decimals)
        {
            var strWords = string.IsNullOrEmpty(baseString) ? string.Empty : " and ";
            if (decimals <= 0) return string.Empty;

            if (currency == Currency.None)
                strWords += $"{decimals}/100";
            else
                strWords += $"{GetWords(decimals.ToString(CultureInfo.InvariantCulture))} Cent";

            if (decimals > 1 && currency != Currency.None)
                strWords += "s";
            return strWords;
        }

        public static string ToWords(this double value, Currency currency)
        {
            var decValue = (decimal)value;
            return decValue.ToWords(currency);
        }
        
        public static string ToWords(this int value, Currency currency)
        {
            var decValue = (decimal)value;
            return decValue.ToWords(currency);
        }

        private static string GetCurrencyWord(Currency currency)
        {
            var dic = new Dictionary<Currency, string>()
            {
                { Currency.None, "" },
                { Currency.Baht, "Baht" },
                { Currency.Dirham, "Dirham" },
                { Currency.Dollar, "Dollar" },
                { Currency.Dong, "Dong" },
                { Currency.Euro, "Euro" },
                { Currency.Peso, "Peso" },
                { Currency.Pound, "Pound" },
                { Currency.Ringgit, "Ringgit" },
                { Currency.Rupee, "Rupee" },
                { Currency.Rupiah, "Rupiah" },
                { Currency.Won, "Won" },
                { Currency.Yen, "Yen" },
                { Currency.Yuan, "Yuan" }
            };

            var val = dic[currency];
            return string.IsNullOrEmpty(val) ? string.Empty : $" {val}";
        }

        private static string GetWords(string input)
        {
            string[] separators =
            {
                "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion",
                "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion",
            };

            var i = 0;

            var strWords = string.Empty;

            while (input.Length > 0)
            {
                var threeDigits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                int.TryParse(threeDigits, out var num);
                threeDigits = GetWord(num);

                threeDigits += i == 0 ? separators[i]: $" {separators[i]}";
                strWords = string.IsNullOrWhiteSpace(strWords)
                    ? threeDigits
                    : $"{threeDigits}, {strWords}";

                i++;
            }

            return strWords;
        }

        private static string GetWord(int num)
        {
            string[] ones =
            {
                "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
                "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };

            string[] tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            var word = string.Empty;

            if (num > 99 && num < 1000)
            {
                word = SetHundredthsWord(num, ones);
                num %= 100;
            }

            if (num > 19 && num < 100)
            {
                word += SetTenthsWord(num, word, tens);

                num %= 10;

                word += SetOnesWordWithDash(num, word, ones);
                return word;
            }

            word += SetOnesWord(num, word, ones);

            return word;
        }

        private static string SetHundredthsWord(int num, IReadOnlyList<string> ones)
        {
            var i = num / 100;
            return $"{ones[i - 1]} Hundred";
        }

        private static string SetTenthsWord(int num, string word, IReadOnlyList<string> tens)
        {
            var i = num / 10;
            return string.IsNullOrEmpty(word)
                ? tens[i - 1]
                : $" {tens[i - 1]}";
        }

        private static string SetOnesWordWithDash(int num, string word, IReadOnlyList<string> ones)
        {
            if (num > 0 && num < 20)

                return string.IsNullOrEmpty(word)
                    ? ones[num - 1]
                    : $"-{ones[num - 1]}";

            return string.Empty;
        }

        private static string SetOnesWord(int num, string word, IReadOnlyList<string> ones)
        {
            if (num > 0 && num < 20)

                return string.IsNullOrEmpty(word)
                    ? ones[num - 1]
                    : $" {ones[num - 1]}";

            return string.Empty;
        }
    }
}