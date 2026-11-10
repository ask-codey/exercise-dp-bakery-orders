import java.util.List;

class Checkout {
    // The second shop's code passes its own settings in here. They aren't used yet.
    Checkout(Settings settings) {
    }

    // The order's total with the shop's sales tax, in whole dollars.
    int total(List<Product> products) {
        int subtotal = 0;
        for (Product product : products) { subtotal += product.price(); }
        return subtotal + subtotal * Settings.instance().taxPercent / 100;
    }
}
