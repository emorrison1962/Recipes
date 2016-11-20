using Recipes.Services.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
	public static class PageParserFactory
	{
		const string TWELVE_TOMATOES = "12tomatoes.com";
		const string ALL_RECIPES = "allrecipes.com";
		const string ALTON_BROWN = "altonbrown.com";
		const string COOKING_NYTIMES = "cooking.nytimes.com";
		const string EAT_TENDER = "eattender.com";
		const string FOOD_52 = "food52.com";
		const string THE_KITCHEN_MCCABE = "thekitchenmccabe.com";
		const string TWICE_COOKED_HALF_BAKED = "twicecookedhalfbaked.com";
		const string CHEF_STEPS = "www.chefsteps.com";
		const string DAVID_LEBOVITZ = "www.davidlebovitz.com";
		const string ELECTROLUX_APPLIANCES = "www.electroluxappliances.com";
		const string EPICURIOUS = "www.epicurious.com";
		const string FOOD = "www.food.com";
		const string FOOD_NETWORK = "www.foodnetwork.com";
		const string SERIOUS_EATS = "www.seriouseats.com";


		public static PageParserBase Create(string url)
		{
			PageParserBase result = null;

			var uri = new UriBuilder(url);

			switch (uri.Host.ToLower())
			{
				case SERIOUS_EATS:
					result = new SeriousEatsParser();
					break;

				case EPICURIOUS:
					result = new EpicurousParser();
					break;
				case CHEF_STEPS:
					result = new ChefStepsParser();
					break;
				case FOOD_NETWORK:
					result = new FoodNetworkParser();
					break;
				case FOOD:
					result = new FoodDotComParser();
					break;
				case EAT_TENDER:
					result = new EatTenderParser();
					break;
				case ALL_RECIPES:
					result = new AllRecipesParser();
					break;
				case THE_KITCHEN_MCCABE:
					result = new TheKitchenMcCabeParser();
					break;

				case TWELVE_TOMATOES:
					result = new TwelveTomatoesParser();
					break;

				case ALTON_BROWN:
					result = new AltonBrownParser();
					break;

				case COOKING_NYTIMES:
					result = new NyTimesParser();
					break;

				case FOOD_52:
					result = new Food52Parser();
					break;

				case TWICE_COOKED_HALF_BAKED:
					result = new TwiceCookedHalfBakedParser();
					break;

				case DAVID_LEBOVITZ:
					result = new DavidLebowitzParser();
					break;

				case ELECTROLUX_APPLIANCES:
					result = new ElectroluxParser();
					break;

				default:
					throw new NotImplementedException(url);
					//Debug.WriteLine(uri.Host);
					break;
			}

			return result;
		}

	}//class
}//ns
