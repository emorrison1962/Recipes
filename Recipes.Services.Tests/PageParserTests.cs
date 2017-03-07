using KitchenPC.NLP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Services.Parsers;
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
				//"https://food52.com/recipes/34179-fried-goat-cheese-with-honey-and-black-pepper?utm_source=Facebook&utm_medium=SocialMarketing&utm_campaign=Social",
				//"http://eattender.com/recipes/patatas-bravas",
				//"http://allrecipes.com/recipe/222319/white-chocolate-and-raspberry-ice-cream/",
				//"http://altonbrown.com/serious-vanilla-ice-cream-recipe/",


				////success
				//"http://www.foodnetwork.com/recipes/giada-de-laurentiis/roasted-fennel-with-parmesan-recipe.html#inline-recipe-player-channel",
				//"http://www.foodnetwork.com/recipes/alton-brown/homemade-italian-breadcrumbs-recipe.html",
				//"http://www.seriouseats.com/recipes/2016/07/gyudon-japanese-simmered-beef-and-rice-bowl-recipe.html",
				//"http://www.foodnetwork.com/recipes/alton-brown/chocolate-ice-cream-recipe.html",

				//"http://www.epicurious.com/recipes/food/views/sausage-cheese-and-basil-lasagna-103005",

				//"http://www.seriouseats.com/recipes/2016/02/slow-roasted-bacon-wrapped-pineapple-recipe.html",
				//"http://www.seriouseats.com/recipes/2015/05/browned-butter-pecan-ice-cream-recipe.html#comments",
				//"http://www.seriouseats.com/recipes/2014/09/the-best-slow-cooked-italian-american-tomato-sauce-red-sauce-recipe.html#comments",
				//"http://www.seriouseats.com/recipes/2016/07/indonesian-balinese-pork-satay-sate-babi-soy-glaze-peanut-recipe.html",

				//"http://www.food.com/recipe/spicy-mexican-chocolate-ice-cream-287657",
				//"http://www.food.com/recipe/german-applesauce-meatloaf-181692",

				//"http://thekitchenmccabe.com/2015/05/20/balsamic-roasted-strawberry-mascarpone-ice-cream/",
				//"http://www.davidlebovitz.com/2007/09/pistachio-gelat/",
				"https://cooking.nytimes.com/recipes/1014851-tomato-and-watermelon-salad",
				"https://twicecookedhalfbaked.com/2011/04/04/roasted-slab-bacon-cant-get-any-better-than-this/",
				//"http://www.electroluxappliances.com/live-love-lux/roasted-cherry-coconut-ice-cream?utm_campaign=9346966&utm_medium=display&utm_source=n5271.913336.aodsocial&utm_content=%ecid&utm_term=135072326",
				//"http://12tomatoes.com/frito-taco-casserole/?utm_source=12t-12t&utm_medium=social-fb&utm_term=20160715&utm_content=link&utm_campaign=frito-taco-casserole&origin=12t_12t_social_fb_link_frito-taco-casserole_20160715",


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
					foreach (var ingredient in recipe.GetIngredientStrings())
					{
						sb.AppendLine(ingredient);
					}

					sb.AppendLine(string.Empty);
					sb.AppendLine("Procedure:");
					foreach (var step in recipe.GetProcedureStrings())
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

		[Ignore]
		[TestMethod()]
		public void AmountParserTest()
		{

			#region Ingredients
			var ingredients = new List<string>() {
"3 cups all-purpose flour, divided",
"1 teaspoon baking powder",
"1 1/2 teaspoons salt, divided",
"4 tablespoons honey, divided",
"2 1/4 cups very cold soda water",
"1/8 teaspoon finely ground black pepper",
"8 ounces fresh goat cheese, cold (the dryer the better, we like the Laura Chenel Chevre)",
"6 to 8 cups canola oil, for frying",
"1/2 teaspoon coarsely ground black pepper",
"Sous Vide Potatoes Ingredients",
"1.5 lbs small red skinned potatoes",
"2-3 Tbsp olive oil",
"1 Tbsp salt",
"Canola oil",
"Aioli Ingredients",
"1 large egg yolk",
"1 garlic clove",
"1/4 teaspoon sea salt",
"1/2 cup extra-virgin olive oil",
"Pinch of cayenne pepper",
"Fresh lemon juice - 1/2 lemon",
"Freshly ground black pepper",
"Cooking in Tanzania",
"altonbrown.com FAILED.",
"4 tablespoons olive oil",
"4 fennel bulbs, cut horizontally into 1/3-inch thick slices, fronds reserved",
"Salt and freshly ground black pepper",
"1/3 cup freshly shredded Parmesan",
"4 ounces stale bread, cut or torn into 1-inch pieces",
"1 tablespoon Italian seasoning",
"1/2 teaspoon garlic powder",
"1/4 teaspoon kosher salt",
"1 small onion, slivered (about 4 ounces; 120g)",
"1/2 cup (120ml) homemade dashi, or the equivalent in Hondashi (see note above)",
"1/4 cup (60ml) dry sake",
"2 tablespoons (30ml) soy sauce",
"1 tablespoon (12g) sugar, plus more to taste",
"1/2 pound (225g) thinly shaved beef ribeye or chuck steak (see note above)",
"1 teaspoon (5ml) grated fresh ginger",
"Salt",
"To Serve:",
"2 cups cooked white rice",
"2 poached eggs (optional)",
"Sliced scallions",
"Beni-shoga (see note above)",
"Togarashi (see note above)",
"1 1/2 ounces unsweetened cocoa powder, approximately 1/2 cup",
"3 cups half-and-half",
"1 cup heavy cream",
"8 large egg yolks",
"9 ounces sugar",
"2 teaspoons pure vanilla extract",
"Sauce:",
"2 tablespoons olive oil",
"1 pound spicy Italian sausages, casings removed",
"1 cup chopped onion",
"3 large garlic cloves, chopped",
"2 teaspoons dried oregano",
"1/4 teaspoon dried crushed red pepper",
"1 28-ounce can crushed tomatoes with added puree",
"1 14 1/2-ounce can diced tomatoes with green pepper and onion (do not drain)",
"Filling:",
"1 1/2 cups (packed) fresh basil leaves",
"1 15-ounce container plus 1 cup part-skim ricotta cheese",
"1 1/2 cups (packed) grated mozzarella cheese (about 6 ounces)",
"3/4 cup grated Parmesan cheese (about 2 ounces)",
"1 large egg",
"1/2 teaspoon salt",
"1/4 teaspoon ground black pepper",
"Assembly:",
"12 no-boil lasagna noodles from one 8-ounce package",
"3 cups (packed) grated mozzarella cheese (about 12 ounces)",
"1 cup grated Parmesan cheese (about 3 ounces)",
"Nonstick olive oil spray",
"For the Adobo Marinade:",
"6 whole dried ancho chilies, stems and seeds removed",
"2 whole chipotle peppers, canned in adobo",
"2 tablespoons (30ml) extra-virgin olive oil",
"4 medium cloves garlic, minced",
"1 teaspoon (2g) oregano, preferably Mexican",
"1 tablespoon (6g) whole cumin seed, toasted and ground",
"1 teaspoon (2g) whole coriander seed, toasted and ground",
"Small pinch ground cloves",
"2 tablespoons (25g) dark brown sugar",
"3 tablespoons (45ml) cider vinegar",
"1 tablespoon (15ml) soy sauce",
"1 tablespoon (15ml) Asian fish sauce",
"Kosher salt",
"For the Pineapple:",
"1 pineapple, skin and core removed, cut into 4 large chunks (about 2 1/2 pounds; 1.15kg pineapple meat)",
"8 ounces (225g) sliced bacon",
"For Serving:",
"40 corn tortillas, heated and kept warm",
"Charred Salsa Verde",
"Crumbled cotija cheese",
"Thinly sliced red or green jalapeño or serrano chilies",
"Roughly chopped fresh cilantro leaves",
"8 ounces unsalted pecans, divided",
"2 cups heavy cream",
"1 cup whole milk",
"4 tablespoons unsalted butter",
"6 large egg yolks",
"3/4 cup raw (turbinado) sugar",
"1/2 to 1 teaspoon kosher salt, to taste",
"4 (28-ounce) cans whole peeled tomatoes, preferably imported D.O.P. San Marzano tomatoes (see note above)",
"1/4 cup extra-virgin olive oil, plus more for finishing.",
"4 tablespoons butter",
"8 cloves garlic, minced (about 3 tablespoons)",
"1 teaspoon red pepper flakes",
"1 tablespoon dried oregano",
"1 medium carrot, cut into large chunks",
"1 medium onion, split in half",
"1 large stem fresh basil",
"Kosher salt and freshly ground black pepper",
"1 tablespoon fish sauce (optional)",
"1/2 cup minced fresh parsley or basil leaves (or a mix of the two)",
"For the Spice Paste:",
"1 (1-inch) knob fresh turmeric, peeled (about 10g), or 1 teaspoon (4g) ground turmeric",
"2 stalks lemongrass, bottom 4 inches only, outer layers and root removed, thinly sliced (about 80g)",
"8 medium cloves garlic, sliced (about 60g)",
"2 small shallots, sliced (about 75g)",
"3 whole dried pasilla or guajillo chilies, stems and seeds removed, roughly chopped (about 40g)",
"2 tablespoons (about 30g) palm sugar or brown sugar",
"2 teaspoons (about 6g) whole coriander seed",
"1 tablespoon (about 9g) whole white peppercorns",
"Kosher salt",
"2 pounds (1kg) boneless pork shoulder, cut into 3/4-inch cubes",
"For the Glaze:",
"1 cup kecap manis (8 ounces; 240ml) (see note above)",
"1/4 cup sugar (about 2 ounces; 50g), plus more if needed",
"1 (2-inch) knob ginger, roughly chopped",
"4 medium cloves garlic, roughly chopped",
"For the Dipping Sauce:",
"10 ounces roasted peanuts (285g; about 1 1/2 cups)",
"1/4 cup (60ml) vegetable or canola oil, divided",
"1 ounce (30g) tamarind pulp, soaked and strained (see note above), or 2 teaspoons (10ml) tamarind concentrate",
"1 tablespoon (15ml) kecap manis or fish sauce",
"Water, as necessary",
"Sugar, to taste",
"1 cup heavy cream",
"1 cup whole milk",
"1 cup skim milk",
"2 ounces unsweetened chocolate",
"1⁄2 cup unsweetened cocoa powder",
"1  vanilla bean",
"2  eggs",
"1 cup sugar",
"1⁄8 teaspoon salt",
"1 teaspoon cinnamon",
"1⁄8 teaspoon cayenne pepper",
"1 1⁄2 lbs hamburger",
"1⁄2 lb ground pork",
"1⁄2 cup onion, chopped",
"1 cup applesauce",
"1 cup breadcrumbs",
"3 tablespoons ketchup",
"2 teaspoons salt",
"1⁄4 teaspoon pepper",
"1 pound Strawberries, washed and hulled",
"1 cup sugar, divided",
"1½ tablespoons Balsamic Vinegar",
"1 cup Whole Milk",
"3 Egg Yolks",
"pinch of salt",
"1½ cups Heavy Cream",
"6 ounces Mascarpone",
"2 cups (½ liter) whole milk",
"1/3 cup (65 gr) sugar",
"2 tablespoons (16 gr) cornstarch (also known as corn flour)",
"7 ounces (200 gr) Bronte pistachio paste (see Note)",
"a few drops of lemon or orange juice",
"4 to 6 large tomatoes, ideally heirloom varieties, cut into 1 1/4-inch cubes tomatoes",
"1 small seedless watermelon, cut into 1 1/4-inch cubes seedless watermelon",
"1 teaspoon kosher salt kosher salt",
"¼ cup extra-virgin olive oil extra-virgin olive oil",
"2 tablespoons sherry vinegar sherry vinegar",
"Kosher salt and freshly ground black pepper to taste",
"1 cup feta cheese, torn into large crumbles feta cheese",
"2 cups cherries, pitted",
"1 Tbsp. melted coconut or olive oil",
"2 tsp. agave nectar",
"pinch of sea salt",
"2 cans coconut milk",
"1/4 cup natural cane sugar",
"1/3 cup agave nectar",
"2 Tbsp. cornstarch",
"1/2 tsp. vanilla extract",
"1/2 cup toasted coconut chips",
"2 ounces chopped dark chocolate",
"1 1/4 pounds ground beef",
"1 (16 oz.) can refried beans",
"1 (4 oz.) can mild green chiles, diced",
"2 cups cooked rice",
"2 cups cheddar cheese, grated, or Mexican cheese blend",
"2 cups Fritos (or corn chips of your choice), divided",
"1 cup salsa",
"1/4 cup water",
"1 medium yellow onion, chopped",
"1 (1.25 oz.) package taco seasoning",
"kosher salt and freshly ground pepper, to taste",

			};
			#endregion

			var parser = new Parser();
			parser.LoadTemplates(
			   "[ING]: [AMT] [UNIT]", //cheddar cheese: 5 cups
			   "[AMT] [UNIT] [FORM] [ING]", //5 cups melted cheddar cheese
			   "[AMT] [UNIT] [ING]", //5 cups cheddar cheese
			   "[AMT] [UNIT] of [ING]", //5 cups of cheddar cheese
			   "[AMT] [UNIT] of [FORM] [ING]", //two cups of shredded cheddar cheese
			   "[AMT] [ING]", //5 eggs
			   "[ING]: [AMT]", //eggs: 5
			   "[FORM] [ING]: [AMT]", //shredded cheddar cheese: 1 cup
			   "[FORM] [ING]: [AMT] [UNIT]", //shredded cheddar cheese: 1 cup

			   "[ING]: [AMT] [UNIT], [PREP]", //cheddar cheese: 5 cups
			   "[AMT] [UNIT] [FORM] [ING], [PREP]", //5 cups melted cheddar cheese
			   "[AMT] [UNIT] [ING], [PREP]", //5 cups cheddar cheese
			   "[AMT] [UNIT] of [ING], [PREP]", //5 cups of cheddar cheese
			   "[AMT] [UNIT] of [FORM] [ING], [PREP]", //two cups of shredded cheddar cheese
			   "[AMT] [ING], [PREP]", //5 eggs
			   "[ING]: [AMT], [PREP]", //eggs: 5
			   "[FORM] [ING]: [AMT], [PREP]", //shredded cheddar cheese: 1 cup
			   "[FORM] [ING]: [AMT] [UNIT], [PREP]" //shredded cheddar cheese: 1 cup
			   );

			foreach (var ingredient in ingredients)
			{
				var result = parser.Parse(ingredient);

				new object();
				//AmountParser.TryParse(ingredient);
			}

		}

	}//class
}//ns