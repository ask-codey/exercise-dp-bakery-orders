using BakeryOrders;

// A demo: tries your code on a day at the bakery. You don't need to change it.
Menu menu = new Menu();
menu.Add("pie", () => new Pie());
List<IProduct> order = Orders.TakeOrder(menu, new List<string> { "cake", "cupcakes", "cookies" });
Console.WriteLine("Order: " + string.Join(", ", order.Select(product => product.Name)));
Console.WriteLine("Price board: " + string.Join(", ", Orders.PriceBoard(menu, new List<string> { "cake", "cupcakes", "cookies", "pie" })));
Console.WriteLine($"First shop's total: ${new Checkout(new Settings(8)).Total(order)}");
Console.WriteLine($"Second shop's total: ${new Checkout(new Settings(10)).Total(order)}");

// Next month's product, written outside the bakery's code.
class Pie : IProduct
{
    public string Name => "Pie";
    public int Price => 15;
}
