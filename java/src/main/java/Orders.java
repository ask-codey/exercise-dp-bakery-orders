import java.util.ArrayList;
import java.util.List;

class Orders {
    // The products for the words the customer tapped, in order. Words the bakery doesn't sell are skipped.
    static List<Product> takeOrder(Menu menu, List<String> kinds) {
        List<Product> products = new ArrayList<>();
        for (String kind : kinds) {
            if (kind.equals("cake")) { products.add(new Cake()); }
            else if (kind.equals("cupcakes")) { products.add(new Cupcakes()); }
            else if (kind.equals("cookies")) { products.add(new CookieBox()); }
        }
        return products;
    }

    // One line for each product the bakery sells, such as "Cake: $30", for the price board.
    static List<String> priceBoard(Menu menu, List<String> kinds) {
        List<String> lines = new ArrayList<>();
        for (String kind : kinds) {
            Product product = null;
            if (kind.equals("cake")) { product = new Cake(); }
            else if (kind.equals("cupcakes")) { product = new Cupcakes(); }
            else if (kind.equals("cookies")) { product = new CookieBox(); }
            if (product != null) { lines.add(product.name() + ": $" + product.price()); }
        }
        return lines;
    }
}
