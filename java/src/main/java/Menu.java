import java.util.function.Supplier;

// What the bakery sells.
class Menu {
    // The new product code calls this: kind is the product's word ("pie"), maker a function that makes one.
    // It does nothing yet.
    void add(String kind, Supplier<Product> maker) {
    }
}
