using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services.Tests
{
    static class RecipeUrls
    {
        static List<string> _recipes_raw = new List<string>() {
                "http://www.seriouseats.com/recipes/2016/06/cuban-roast-pork-shoulder-mojo-recipe.html"
                ,"http://www.seriouseats.com/recipes/2015/04/foolproof-bearnaise-sauce-recipe.html"
                ,"http://www.food.com/recipe/german-applesauce-meatloaf-181692"
                ,"http://www.seriouseats.com/recipes/2016/02/slow-roasted-bacon-wrapped-pineapple-recipe.html"
                ,"http://www.epicurious.com/recipes/food/views/Sausage-Cheese-and-Basil-Lasagna-103005"
                ,"http://www.seriouseats.com/recipes/2015/05/browned-butter-pecan-ice-cream-recipe.html#comments"
                ,"https://www.chefsteps.com/activities/smashed-potatoes"
                ,"http://www.foodnetwork.com/recipes/alton-brown/chocolate-ice-cream-recipe.html"
                ,"http://www.food.com/recipe/spicy-mexican-chocolate-ice-cream-287657"
                ,"http://eattender.com/recipes/patatas-bravas"
                ,"http://allrecipes.com/recipe/222319/white-chocolate-and-raspberry-ice-cream/"
                ,"http://thekitchenmccabe.com/2015/05/20/balsamic-roasted-strawberry-mascarpone-ice-cream/"
                ,"http://www.davidlebovitz.com/2007/09/pistachio-gelat/"
                ,"http://cooking.nytimes.com/recipes/1014851-tomato-and-watermelon-salad?action=click&module=Collection+Page+Recipe+Card&region=What+to+Cook+When+It’s+Too+Hot+to+Move&pgType=collection&rank=10"
                ,"https://twicecookedhalfbaked.com/2011/04/04/roasted-slab-bacon-cant-get-any-better-than-this/"
                ,"http://www.electroluxappliances.com/live-love-lux/roasted-cherry-coconut-ice-cream?utm_campaign=9346966&utm_medium=display&utm_source=N5271.913336.AODSOCIAL&utm_content=%ecid&utm_term=135072326"
                ,"http://www.seriouseats.com/recipes/2014/09/the-best-slow-cooked-italian-american-tomato-sauce-red-sauce-recipe.html#comments"
                ,"http://www.foodnetwork.com/recipes/giada-de-laurentiis/roasted-fennel-with-parmesan-recipe.html#inline-recipe-player-channel"
                ,"http://12tomatoes.com/frito-taco-casserole/?utm_source=12t-12t&utm_medium=social-fb&utm_term=20160715&utm_content=link&utm_campaign=frito-taco-casserole&origin=12t_12t_social_fb_link_frito-taco-casserole_20160715"
                ,"https://www.chefsteps.com/activities/sous-vide-burgers"
                ,"http://www.ebay.com/gds/Crockpot-Turtles-Candy-/10000000206545881/g.html?roken2=ti.pRG9ubmEgRWxpY2s="
                ,"http://www.seriouseats.com/recipes/2016/07/indonesian-balinese-pork-satay-sate-babi-soy-glaze-peanut-recipe.html"
                ,"http://altonbrown.com/serious-vanilla-ice-cream-recipe/"
                ,"https://food52.com/recipes/34179-fried-goat-cheese-with-honey-and-black-pepper?utm_source=Facebook&utm_medium=SocialMarketing&utm_campaign=Social"
                ,"http://www.foodnetwork.com/recipes/alton-brown/homemade-italian-breadcrumbs-recipe.html"
                ,"http://www.seriouseats.com/recipes/2016/07/gyudon-japanese-simmered-beef-and-rice-bowl-recipe.html"
                ,"http://www.seriouseats.com/recipes/2011/05/thai-style-marinated-flank-steak-and-herb-sal.html"
                ,"http://www.seriouseats.com/recipes/2016/02/slow-roasted-bacon-wrapped-pineapple-recipe.html"
                ,"http://www.epicurious.com/recipes/food/views/Sausage-Cheese-and-Basil-Lasagna-103005"
                ,"http://www.seriouseats.com/recipes/2015/05/browned-butter-pecan-ice-cream-recipe.html#comments"
                ,"https://www.chefsteps.com/activities/smashed-potatoes"
                ,"http://www.foodnetwork.com/recipes/alton-brown/chocolate-ice-cream-recipe.html"
                ,"http://www.food.com/recipe/spicy-mexican-chocolate-ice-cream-287657"
                ,"http://eattender.com/recipes/patatas-bravas"
                ,"http://allrecipes.com/recipe/222319/white-chocolate-and-raspberry-ice-cream/"
                ,"http://thekitchenmccabe.com/2015/05/20/balsamic-roasted-strawberry-mascarpone-ice-cream/"
                ,"http://www.davidlebovitz.com/2007/09/pistachio-gelat/"
                ,"http://cooking.nytimes.com/recipes/1014851-tomato-and-watermelon-salad?action=click&module=Collection+Page+Recipe+Card&region=What+to+Cook+When+It’s+Too+Hot+to+Move&pgType=collection&rank=10"
                ,"https://twicecookedhalfbaked.com/2011/04/04/roasted-slab-bacon-cant-get-any-better-than-this/"
                ,"http://www.electroluxappliances.com/live-love-lux/roasted-cherry-coconut-ice-cream?utm_campaign=9346966&utm_medium=display&utm_source=N5271.913336.AODSOCIAL&utm_content=%ecid&utm_term=135072326"
                ,"http://www.seriouseats.com/recipes/2014/09/the-best-slow-cooked-italian-american-tomato-sauce-red-sauce-recipe.html#comments"
                ,"http://www.foodnetwork.com/recipes/giada-de-laurentiis/roasted-fennel-with-parmesan-recipe.html#inline-recipe-player-channel"
                ,"http://12tomatoes.com/frito-taco-casserole/?utm_source=12t-12t&utm_medium=social-fb&utm_term=20160715&utm_content=link&utm_campaign=frito-taco-casserole&origin=12t_12t_social_fb_link_frito-taco-casserole_20160715"
                ,"https://www.chefsteps.com/activities/sous-vide-burgers"
                ,"http://www.ebay.com/gds/Crockpot-Turtles-Candy-/10000000206545881/g.html?roken2=ti.pRG9ubmEgRWxpY2s="
                ,"http://www.seriouseats.com/recipes/2016/07/indonesian-balinese-pork-satay-sate-babi-soy-glaze-peanut-recipe.html"
                ,"http://altonbrown.com/serious-vanilla-ice-cream-recipe/"
                ,"https://food52.com/recipes/34179-fried-goat-cheese-with-honey-and-black-pepper?utm_source=Facebook&utm_medium=SocialMarketing&utm_campaign=Social"
                ,"http://www.food.com/recipe/german-applesauce-meatloaf-181692"
                ,"http://www.foodnetwork.com/recipes/alton-brown/homemade-italian-breadcrumbs-recipe.html"
                ,"http://www.seriouseats.com/recipes/2016/07/gyudon-japanese-simmered-beef-and-rice-bowl-recipe.html"
                ,"http://www.seriouseats.com/recipes/2011/05/thai-style-marinated-flank-steak-and-herb-sal.html"
                ,"http://www.seriouseats.com/recipes/2016/02/slow-roasted-bacon-wrapped-pineapple-recipe.html"
                ,"http://www.epicurious.com/recipes/food/views/Sausage-Cheese-and-Basil-Lasagna-103005"
                ,"http://www.seriouseats.com/recipes/2015/05/browned-butter-pecan-ice-cream-recipe.html#comments"
                ,"https://www.chefsteps.com/activities/smashed-potatoes"
                ,"http://www.foodnetwork.com/recipes/alton-brown/chocolate-ice-cream-recipe.html"
                ,"http://www.food.com/recipe/spicy-mexican-chocolate-ice-cream-287657"
                ,"http://eattender.com/recipes/patatas-bravas"
                ,"http://allrecipes.com/recipe/222319/white-chocolate-and-raspberry-ice-cream/"
                ,"http://thekitchenmccabe.com/2015/05/20/balsamic-roasted-strawberry-mascarpone-ice-cream/"
                ,"http://www.davidlebovitz.com/2007/09/pistachio-gelat/"
                ,"http://cooking.nytimes.com/recipes/1014851-tomato-and-watermelon-salad?action=click&module=Collection+Page+Recipe+Card&region=What+to+Cook+When+It’s+Too+Hot+to+Move&pgType=collection&rank=10"
                ,"https://twicecookedhalfbaked.com/2011/04/04/roasted-slab-bacon-cant-get-any-better-than-this/"
                ,"http://www.electroluxappliances.com/live-love-lux/roasted-cherry-coconut-ice-cream?utm_campaign=9346966&utm_medium=display&utm_source=N5271.913336.AODSOCIAL&utm_content=%ecid&utm_term=135072326"
                ,"http://www.foodnetwork.com/recipes/alton-brown/homemade-italian-breadcrumbs-recipe.html"
                ,"http://www.seriouseats.com/recipes/2014/09/the-best-slow-cooked-italian-american-tomato-sauce-red-sauce-recipe.html#comments"
                ,"http://www.foodnetwork.com/recipes/giada-de-laurentiis/roasted-fennel-with-parmesan-recipe.html#inline-recipe-player-channel"
                ,"http://12tomatoes.com/frito-taco-casserole/?utm_source=12t-12t&utm_medium=social-fb&utm_term=20160715&utm_content=link&utm_campaign=frito-taco-casserole&origin=12t_12t_social_fb_link_frito-taco-casserole_20160715"
                ,"https://www.chefsteps.com/activities/sous-vide-burgers"
                ,"http://www.ebay.com/gds/Crockpot-Turtles-Candy-/10000000206545881/g.html?roken2=ti.pRG9ubmEgRWxpY2s="
                ,"http://www.seriouseats.com/recipes/2016/07/indonesian-balinese-pork-satay-sate-babi-soy-glaze-peanut-recipe.html"
                ,"http://altonbrown.com/serious-vanilla-ice-cream-recipe/"
                ,"https://food52.com/recipes/34179-fried-goat-cheese-with-honey-and-black-pepper?utm_source=Facebook&utm_medium=SocialMarketing&utm_campaign=Social"
                ,"http://www.food.com/recipe/german-applesauce-meatloaf-181692"
                ,"http://www.seriouseats.com/recipes/2016/07/gyudon-japanese-simmered-beef-and-rice-bowl-recipe.html"
                ,"http://www.seriouseats.com/recipes/2011/05/thai-style-marinated-flank-steak-and-herb-sal.html"
                ,"http://www.seriouseats.com/recipes/2015/03/ricotta-gnocchi-homemade-food-lab-recipe.html"
                ,"http://www.seriouseats.com/recipes/2016/06/cuban-roast-pork-shoulder-mojo-recipe.html"
                ,"https://www.chefsteps.com/activities/75-c-egg"
                ,"http://www.seriouseats.com/recipes/2016/10/pressure-cooker-tomato-sauce.html"
                ,"http://www.seriouseats.com/recipes/2016/10/pressure-cooker-tomato-sauce.html"
                //,"http://www.astray.com/recipes/?show=Sweet%20potatoes%20in%20praline%20sauce"
                ,"http://www.foodnetwork.com/recipes/guy-fieri/red-devil-cranberries-recipe.html"
                ,"https://www.chefsteps.com/activities/can-t-f-it-up-eggs-benedict"
                ,"http://www.seriouseats.com/recipes/2016/01/chicken-tinga-spicy-mexican-shredded-chicken-recipe.html"
                ,"http://www.food.com/recipe/vietnamese-ground-pork-173038"
                ,"http://www.bonappetit.com/recipe/garlicky-bok-choy"
                ,"http://www.eatingwell.com/recipe/249251/roasted-baby-bok-choy/"
                ,"https://www.mygourmetconnection.com/recipes/main-courses/pork/vietnamese-ground-pork-tomato-sauce.php"
                ,"http://www.seriouseats.com/recipes/2016/01/pressure-cooker-french-onion-soup-recipe.html"
            };
        private static List<string> _recipes;

        public static List<string> Catalog
        {
            get
            {
                if (null == _recipes)
                    _recipes = GetRecipes();
                return new List<string>(_recipes);
            }
        }

        private static List<string> GetRecipes()
        {
            var result = new List<string>();
            result = new HashSet<string>(_recipes_raw).ToList();
            result.Sort();
            return result;
        }
    }
}
