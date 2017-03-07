using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Recipes.Services.Parsers
{
    public class AmountParser
    {
        static public Amount TryParse(string ingredient)
        {
            var result = new AmountParser().TryParseImpl(ingredient);
            return result;
        }

        Amount TryParseImpl(string ingredient)
        {
            ingredient = ingredient.ToLower();

            //var arr = ingredient.Split(' ');

            var unitNames = this.GetUnitStrings();

            const string DECIMAL_AND_FRACTION = @"^\d+(?:\.?\d*|\s\d+\/\d+)$";
            var match = Regex.Split(ingredient, DECIMAL_AND_FRACTION);

            //½ special character
            //2 1/2 cups
            //1 14 1/2-ounce can
            //4 medium cloves 

            return null;
        }


        List<string> GetUnitStrings()
        {
            var result = new List<string>();
            var arr = Enum.GetNames(typeof(UnitsEnum));
            foreach (var s in arr)
                result.Add(s.ToLower());
            return result;
        }


    }
}
