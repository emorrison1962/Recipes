using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Recipes.Services.Tests
{
	[TestClass()]
	public class PageParserTests
	{
		[Ignore]
		[TestMethod()]
		public void ParseTest()
		{
			var list = new List<string>()
			{
				"https://food52.com/recipes/34179-fried-goat-cheese-with-honey-and-black-pepper?utm_source=Facebook&utm_medium=SocialMarketing&utm_campaign=Social",
				"http://eattender.com/recipes/patatas-bravas",
				"http://allrecipes.com/recipe/222319/white-chocolate-and-raspberry-ice-cream/",
				"http://altonbrown.com/serious-vanilla-ice-cream-recipe/",


				//success
				"http://www.foodnetwork.com/recipes/giada-de-laurentiis/roasted-fennel-with-parmesan-recipe.html#inline-recipe-player-channel",
				"http://www.foodnetwork.com/recipes/alton-brown/homemade-italian-breadcrumbs-recipe.html",
				"http://www.seriouseats.com/recipes/2016/07/gyudon-japanese-simmered-beef-and-rice-bowl-recipe.html",
				"http://www.foodnetwork.com/recipes/alton-brown/chocolate-ice-cream-recipe.html",

				"http://www.epicurious.com/recipes/food/views/sausage-cheese-and-basil-lasagna-103005",

				"http://www.seriouseats.com/recipes/2016/02/slow-roasted-bacon-wrapped-pineapple-recipe.html",
				"http://www.seriouseats.com/recipes/2015/05/browned-butter-pecan-ice-cream-recipe.html#comments",
				"http://www.seriouseats.com/recipes/2014/09/the-best-slow-cooked-italian-american-tomato-sauce-red-sauce-recipe.html#comments",
				"http://www.seriouseats.com/recipes/2016/07/indonesian-balinese-pork-satay-sate-babi-soy-glaze-peanut-recipe.html",

				"http://www.food.com/recipe/spicy-mexican-chocolate-ice-cream-287657",
				"http://www.food.com/recipe/german-applesauce-meatloaf-181692",

				"http://thekitchenmccabe.com/2015/05/20/balsamic-roasted-strawberry-mascarpone-ice-cream/",
				"http://www.davidlebovitz.com/2007/09/pistachio-gelat/",
				"http://cooking.nytimes.com/recipes/1014851-tomato-and-watermelon-salad?action=click&module=collection+page+recipe+card&region=what+to+cook+when+it’s+too+hot+to+move&pgtype=collection&rank=10",
				"https://twicecookedhalfbaked.com/2011/04/04/roasted-slab-bacon-cant-get-any-better-than-this/",
				"http://www.electroluxappliances.com/live-love-lux/roasted-cherry-coconut-ice-cream?utm_campaign=9346966&utm_medium=display&utm_source=n5271.913336.aodsocial&utm_content=%ecid&utm_term=135072326",
				"http://12tomatoes.com/frito-taco-casserole/?utm_source=12t-12t&utm_medium=social-fb&utm_term=20160715&utm_content=link&utm_campaign=frito-taco-casserole&origin=12t_12t_social_fb_link_frito-taco-casserole_20160715",


				//failure
				//"https://www.chefsteps.com/activities/sous-vide-burgers",
				//"https://www.chefsteps.com/activities/smashed-potatoes",
			};

			foreach (var s in list)
			{
				var parser = PageParserFactory.Create(s);
				var host = new UriBuilder(s).Host;
				try
				{
					Debug.WriteLine(s);
					var recipe = parser.TryParse(s);
					if (recipe.IsValid)
					{
						Debug.WriteLine(string.Format("{0} IS VALID.", host));
					}
					else
					{
						Debug.WriteLine(string.Format("{0} is NOT valid.", host));
					}
				}
				catch (Exception)
				{
					Debug.WriteLine(string.Format("{0} FAILED.", new UriBuilder(s).Host));
				}
			}

		}

		[TestMethod()]
		public void PrintRecipeTest()
		{
			var list = new List<string>()
			{
				"https://food52.com/recipes/34179-fried-goat-cheese-with-honey-and-black-pepper?utm_source=Facebook&utm_medium=SocialMarketing&utm_campaign=Social",
				"http://eattender.com/recipes/patatas-bravas",
				"http://allrecipes.com/recipe/222319/white-chocolate-and-raspberry-ice-cream/",
				"http://altonbrown.com/serious-vanilla-ice-cream-recipe/",


				//success
				"http://www.foodnetwork.com/recipes/giada-de-laurentiis/roasted-fennel-with-parmesan-recipe.html#inline-recipe-player-channel",
				"http://www.foodnetwork.com/recipes/alton-brown/homemade-italian-breadcrumbs-recipe.html",
				"http://www.seriouseats.com/recipes/2016/07/gyudon-japanese-simmered-beef-and-rice-bowl-recipe.html",
				"http://www.foodnetwork.com/recipes/alton-brown/chocolate-ice-cream-recipe.html",

				"http://www.epicurious.com/recipes/food/views/sausage-cheese-and-basil-lasagna-103005",

				"http://www.seriouseats.com/recipes/2016/02/slow-roasted-bacon-wrapped-pineapple-recipe.html",
				"http://www.seriouseats.com/recipes/2015/05/browned-butter-pecan-ice-cream-recipe.html#comments",
				"http://www.seriouseats.com/recipes/2014/09/the-best-slow-cooked-italian-american-tomato-sauce-red-sauce-recipe.html#comments",
				"http://www.seriouseats.com/recipes/2016/07/indonesian-balinese-pork-satay-sate-babi-soy-glaze-peanut-recipe.html",

				"http://www.food.com/recipe/spicy-mexican-chocolate-ice-cream-287657",
				"http://www.food.com/recipe/german-applesauce-meatloaf-181692",

				"http://thekitchenmccabe.com/2015/05/20/balsamic-roasted-strawberry-mascarpone-ice-cream/",
				"http://www.davidlebovitz.com/2007/09/pistachio-gelat/",
				"http://cooking.nytimes.com/recipes/1014851-tomato-and-watermelon-salad?action=click&module=collection+page+recipe+card&region=what+to+cook+when+it’s+too+hot+to+move&pgtype=collection&rank=10",
				"https://twicecookedhalfbaked.com/2011/04/04/roasted-slab-bacon-cant-get-any-better-than-this/",
				"http://www.electroluxappliances.com/live-love-lux/roasted-cherry-coconut-ice-cream?utm_campaign=9346966&utm_medium=display&utm_source=n5271.913336.aodsocial&utm_content=%ecid&utm_term=135072326",
				"http://12tomatoes.com/frito-taco-casserole/?utm_source=12t-12t&utm_medium=social-fb&utm_term=20160715&utm_content=link&utm_campaign=frito-taco-casserole&origin=12t_12t_social_fb_link_frito-taco-casserole_20160715",


				//failure
				//"https://www.chefsteps.com/activities/sous-vide-burgers",
				//"https://www.chefsteps.com/activities/smashed-potatoes",
			};

			foreach (var s in list)
			{
				var parser = PageParserFactory.Create(s);
				var host = new UriBuilder(s).Host;
				try
				{
					Debug.WriteLine(s);
					var recipe = parser.TryParse(s);

					var sb = new StringBuilder();

					sb.AppendLine(recipe.Name);

					sb.AppendLine("Ingredients:");
					foreach (var ingredient in recipe.Ingredients)
					{
						sb.AppendLine(ingredient);
					}

					sb.AppendLine("Procedure:");
					foreach (var step in recipe.Procedure)
					{
						sb.AppendLine(step);
					}
					sb.AppendLine();

					Debug.WriteLine(sb.ToString());
				}
				catch (Exception)
				{
					Debug.WriteLine(string.Format("{0} FAILED.", new UriBuilder(s).Host));
				}
			}

		}

		public string GetHost(string url)
		{
			var uri = new UriBuilder(url);
			return uri.Host.ToLower();
		}


	}//class
}//ns