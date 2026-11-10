import java.util.List;

// A demo: tries your code on a day at the bakery. You don't need to change it.
// Run it with: java src/main/java/Demo.java
public class Demo {
    // Next month's product, written outside the bakery's code.
    static class Pie implements Product {
        public String name() { return "Pie"; }
        public int price() { return 15; }
    }

    public static void main(String[] args) {
        Menu menu = new Menu();
        menu.add("pie", () -> new Pie());
        List<Product> order = Orders.takeOrder(menu, List.of("cake", "cupcakes", "cookies"));
        System.out.println("Order: " + String.join(", ", order.stream().map(Product::name).toList()));
        System.out.println("Price board: " + String.join(", ", Orders.priceBoard(menu, List.of("cake", "cupcakes", "cookies", "pie"))));
        System.out.println("First shop's total: $" + new Checkout(new Settings(8)).total(order));
        System.out.println("Second shop's total: $" + new Checkout(new Settings(10)).total(order));
    }
}
