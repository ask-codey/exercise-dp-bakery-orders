using BakeryOrders;

namespace BakeryOrders.Tests;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.

[TestClass]
public class BakeryTests
{
    private class TestPie : IProduct
    {
        public string Name => "Pie";
        public int Price => 15;
    }

    private static List<string> Names(List<IProduct> products) => products.Select(product => product.Name).ToList();

    private static List<IProduct> ThreeThings() => Orders.TakeOrder(new Menu(), new List<string> { "cake", "cupcakes", "cookies" });

    // What the bakery does today. These pass before you change anything: keep them passing.

    [TestMethod]
    public void AnOrderHoldsTheProductsInTheOrderTapped()
    {
        CollectionAssert.AreEqual(new List<string> { "Cake", "Cupcakes", "Cookie box" }, Names(ThreeThings()),
            "Keep this working: an order for cake, cupcakes and cookies holds Cake, Cupcakes and Cookie box, in that order.");
    }

    [TestMethod]
    public void WordsTheBakeryDoesNotSellAreSkipped()
    {
        CollectionAssert.AreEqual(new List<string> { "Cake" }, Names(Orders.TakeOrder(new Menu(), new List<string> { "bagel", "cake" })),
            "Keep this working: the bakery doesn't sell bagels, so an order for a bagel and a cake holds only the Cake.");
    }

    [TestMethod]
    public void ThePriceBoardShowsEachProductAndItsPrice()
    {
        CollectionAssert.AreEqual(new List<string> { "Cake: $30", "Cookie box: $8" }, Orders.PriceBoard(new Menu(), new List<string> { "cake", "cookies" }),
            "Keep this working: the price board for cake and cookies says \"Cake: $30\" and \"Cookie box: $8\".");
    }

    [TestMethod]
    public void CheckoutAddsTheShopsTax()
    {
        Assert.AreEqual(54, new Checkout(new Settings(8)).Total(ThreeThings()),
            "Keep this working: $50 of cake, cupcakes and cookies with 8% tax is $54.");
    }

    // The ticket's new requirements. These fail until the work is done.

    [TestMethod]
    public void AProductAddedToTheMenuCanBeOrdered()
    {
        Menu menu = new Menu();
        menu.Add("pie", () => new TestPie());
        CollectionAssert.AreEqual(new List<string> { "Pie", "Cake" }, Names(Orders.TakeOrder(menu, new List<string> { "pie", "cake" })),
            "Not yet: after menu.Add(\"pie\", ...), an order for pie and cake holds Pie and Cake.");
    }

    [TestMethod]
    public void AProductAddedToTheMenuIsOnThePriceBoard()
    {
        Menu menu = new Menu();
        menu.Add("pie", () => new TestPie());
        CollectionAssert.AreEqual(new List<string> { "Cake: $30", "Pie: $15" }, Orders.PriceBoard(menu, new List<string> { "cake", "pie" }),
            "Not yet: after menu.Add(\"pie\", ...), the price board shows \"Pie: $15\" too, without anyone editing the price board.");
    }

    [TestMethod]
    public void TwoShopsChargeTheirOwnTax()
    {
        Assert.AreEqual(54, new Checkout(new Settings(8)).Total(ThreeThings()), "Not yet: the first shop's 8% tax makes $50 into $54.");
        Assert.AreEqual(55, new Checkout(new Settings(10)).Total(ThreeThings()), "Not yet: the second shop's 10% tax makes $50 into $55.");
    }

    [TestMethod]
    public void ChangingOneShopsTaxLeavesTheOtherAlone()
    {
        Settings first = new Settings(8);
        Settings second = new Settings(8);
        Checkout firstCheckout = new Checkout(first);
        Checkout secondCheckout = new Checkout(second);
        second.TaxPercent = 10;
        Assert.AreEqual(55, secondCheckout.Total(ThreeThings()), "Not yet: the second shop changed its tax to 10%, so its $50 order costs $55.");
        Assert.AreEqual(54, firstCheckout.Total(ThreeThings()), "Not yet: the first shop's tax is still 8%, so its $50 order still costs $54.");
    }
}
