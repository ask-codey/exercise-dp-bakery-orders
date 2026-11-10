namespace BakeryOrders;

// Sunrise Bakery's order code. Read the ticket in README.md first.

public interface IProduct
{
    string Name { get; }
    int Price { get; }
}

public class Cake : IProduct { public string Name => "Cake"; public int Price => 30; }
public class Cupcakes : IProduct { public string Name => "Cupcakes"; public int Price => 12; }
public class CookieBox : IProduct { public string Name => "Cookie box"; public int Price => 8; }

// The shop's settings. The whole app reaches them through Settings.Instance.
public class Settings
{
    private static Settings? instance;

    public Settings(int taxPercent) => TaxPercent = taxPercent;

    public static Settings Instance => instance ??= new Settings(8);

    public int TaxPercent { get; set; }
}

// What the bakery sells.
public class Menu
{
    // The new product code calls this: kind is the product's word ("pie"), maker a function that makes one.
    // It does nothing yet.
    public void Add(string kind, Func<IProduct> maker)
    {
    }
}

public static class Orders
{
    // The products for the words the customer tapped, in order. Words the bakery doesn't sell are skipped.
    public static List<IProduct> TakeOrder(Menu menu, List<string> kinds)
    {
        List<IProduct> products = new List<IProduct>();
        foreach (string kind in kinds)
        {
            if (kind == "cake") { products.Add(new Cake()); }
            else if (kind == "cupcakes") { products.Add(new Cupcakes()); }
            else if (kind == "cookies") { products.Add(new CookieBox()); }
        }
        return products;
    }

    // One line for each product the bakery sells, such as "Cake: $30", for the price board.
    public static List<string> PriceBoard(Menu menu, List<string> kinds)
    {
        List<string> lines = new List<string>();
        foreach (string kind in kinds)
        {
            IProduct? product = null;
            if (kind == "cake") { product = new Cake(); }
            else if (kind == "cupcakes") { product = new Cupcakes(); }
            else if (kind == "cookies") { product = new CookieBox(); }
            if (product != null) { lines.Add($"{product.Name}: ${product.Price}"); }
        }
        return lines;
    }
}

public class Checkout
{
    // The second shop's code passes its own settings in here. They aren't used yet.
    public Checkout(Settings settings)
    {
    }

    // The order's total with the shop's sales tax, in whole dollars.
    public int Total(List<IProduct> products)
    {
        int subtotal = products.Sum(product => product.Price);
        return subtotal + subtotal * Settings.Instance.TaxPercent / 100;
    }
}
