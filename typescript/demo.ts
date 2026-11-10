// A demo: tries your code on a day at the bakery. You don't need to change it.
import { Checkout, Menu, Settings, priceBoard, takeOrder, type Product } from "./bakery.ts";

// Next month's product, written outside the bakery's code.
class Pie implements Product { name = "Pie"; price = 15; }

const menu = new Menu();
menu.add("pie", () => new Pie());
const order = takeOrder(menu, ["cake", "cupcakes", "cookies"]);
console.log("Order: " + order.map((product) => product.name).join(", "));
console.log("Price board: " + priceBoard(menu, ["cake", "cupcakes", "cookies", "pie"]).join(", "));
console.log(`First shop's total: $${new Checkout(new Settings(8)).total(order)}`);
console.log(`Second shop's total: $${new Checkout(new Settings(10)).total(order)}`);
