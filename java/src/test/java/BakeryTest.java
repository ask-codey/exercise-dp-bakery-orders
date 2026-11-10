import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.List;
import org.junit.jupiter.api.Test;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
class BakeryTest {
    static class TestPie implements Product {
        public String name() { return "Pie"; }
        public int price() { return 15; }
    }

    private static List<String> names(List<Product> products) {
        return products.stream().map(Product::name).toList();
    }

    private static List<Product> threeThings() {
        return Orders.takeOrder(new Menu(), List.of("cake", "cupcakes", "cookies"));
    }

    // What the bakery does today. These pass before you change anything: keep them passing.

    @Test
    void anOrderHoldsTheProductsInTheOrderTapped() {
        assertEquals(List.of("Cake", "Cupcakes", "Cookie box"), names(threeThings()),
            "Keep this working: an order for cake, cupcakes and cookies holds Cake, Cupcakes and Cookie box, in that order.");
    }

    @Test
    void wordsTheBakeryDoesNotSellAreSkipped() {
        assertEquals(List.of("Cake"), names(Orders.takeOrder(new Menu(), List.of("bagel", "cake"))),
            "Keep this working: the bakery doesn't sell bagels, so an order for a bagel and a cake holds only the Cake.");
    }

    @Test
    void thePriceBoardShowsEachProductAndItsPrice() {
        assertEquals(List.of("Cake: $30", "Cookie box: $8"), Orders.priceBoard(new Menu(), List.of("cake", "cookies")),
            "Keep this working: the price board for cake and cookies says \"Cake: $30\" and \"Cookie box: $8\".");
    }

    @Test
    void checkoutAddsTheShopsTax() {
        assertEquals(54, new Checkout(new Settings(8)).total(threeThings()),
            "Keep this working: $50 of cake, cupcakes and cookies with 8% tax is $54.");
    }

    // The ticket's new requirements. These fail until the work is done.

    @Test
    void aProductAddedToTheMenuCanBeOrdered() {
        Menu menu = new Menu();
        menu.add("pie", () -> new TestPie());
        assertEquals(List.of("Pie", "Cake"), names(Orders.takeOrder(menu, List.of("pie", "cake"))),
            "Not yet: after menu.add(\"pie\", ...), an order for pie and cake holds Pie and Cake.");
    }

    @Test
    void aProductAddedToTheMenuIsOnThePriceBoard() {
        Menu menu = new Menu();
        menu.add("pie", () -> new TestPie());
        assertEquals(List.of("Cake: $30", "Pie: $15"), Orders.priceBoard(menu, List.of("cake", "pie")),
            "Not yet: after menu.add(\"pie\", ...), the price board shows \"Pie: $15\" too, without anyone editing the price board.");
    }

    @Test
    void twoShopsChargeTheirOwnTax() {
        assertEquals(54, new Checkout(new Settings(8)).total(threeThings()), "Not yet: the first shop's 8% tax makes $50 into $54.");
        assertEquals(55, new Checkout(new Settings(10)).total(threeThings()), "Not yet: the second shop's 10% tax makes $50 into $55.");
    }

    @Test
    void changingOneShopsTaxLeavesTheOtherAlone() {
        Settings first = new Settings(8);
        Settings second = new Settings(8);
        Checkout firstCheckout = new Checkout(first);
        Checkout secondCheckout = new Checkout(second);
        second.taxPercent = 10;
        assertEquals(55, secondCheckout.total(threeThings()), "Not yet: the second shop changed its tax to 10%, so its $50 order costs $55.");
        assertEquals(54, firstCheckout.total(threeThings()), "Not yet: the first shop's tax is still 8%, so its $50 order still costs $54.");
    }
}
