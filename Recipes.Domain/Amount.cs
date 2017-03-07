namespace Recipes.Domain
{
    public enum UnitsEnum
    {
        Teaspoon,
        Tablespoon,
        Cup,
        Ounce,
        Gram,
        Pound,
        Each,
        Pinch,
        AFew,
        Small,
        Large,
        ExtraLarge,
        Whole,
        Clove,
        ToTaste,
    };
    public class Amount
    {
        decimal Quantity { get; set; }
        UnitsEnum Unit { get; set; }
    }
}
