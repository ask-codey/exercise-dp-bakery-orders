// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
import assert from "node:assert/strict";
import { test } from "node:test";
import { Checkout, Menu, Settings, priceBoard, takeOrder, type Product } from "./bakery.ts";

class TestPie implements Product { name = "Pie"; price = 15; }

function names(products: Product[]): string[] {
  return products.map((product) => product.name);
}

function threeThings(): Product[] {
  return takeOrder(new Menu(), ["cake", "cupcakes", "cookies"]);
}

// What the bakery does today. These pass before you change anything: keep them passing.

test("an order holds the products in the order tapped", () => {
  assert.deepEqual(names(threeThings()), ["Cake", "Cupcakes", "Cookie box"],
    "Keep this working: an order for cake, cupcakes and cookies holds Cake, Cupcakes and Cookie box, in that order.");
});

test("words the bakery doesn't sell are skipped", () => {
  assert.deepEqual(names(takeOrder(new Menu(), ["bagel", "cake"])), ["Cake"],
    "Keep this working: the bakery doesn't sell bagels, so an order for a bagel and a cake holds only the Cake.");
});

test("the price board shows each product and its price", () => {
  assert.deepEqual(priceBoard(new Menu(), ["cake", "cookies"]), ["Cake: $30", "Cookie box: $8"],
    'Keep this working: the price board for cake and cookies says "Cake: $30" and "Cookie box: $8".');
});

test("checkout adds the shop's tax", () => {
  assert.equal(new Checkout(new Settings(8)).total(threeThings()), 54,
    "Keep this working: $50 of cake, cupcakes and cookies with 8% tax is $54.");
});

// The ticket's new requirements. These fail until the work is done.

test("a product added to the menu can be ordered", () => {
  const menu = new Menu();
  menu.add("pie", () => new TestPie());
  assert.deepEqual(names(takeOrder(menu, ["pie", "cake"])), ["Pie", "Cake"],
    'Not yet: after menu.add("pie", ...), an order for pie and cake holds Pie and Cake.');
});

test("a product added to the menu is on the price board", () => {
  const menu = new Menu();
  menu.add("pie", () => new TestPie());
  assert.deepEqual(priceBoard(menu, ["cake", "pie"]), ["Cake: $30", "Pie: $15"],
    'Not yet: after menu.add("pie", ...), the price board shows "Pie: $15" too, without anyone editing the price board.');
});

test("two shops charge their own tax", () => {
  assert.equal(new Checkout(new Settings(8)).total(threeThings()), 54, "Not yet: the first shop's 8% tax makes $50 into $54.");
  assert.equal(new Checkout(new Settings(10)).total(threeThings()), 55, "Not yet: the second shop's 10% tax makes $50 into $55.");
});

test("changing one shop's tax leaves the other alone", () => {
  const first = new Settings(8);
  const second = new Settings(8);
  const firstCheckout = new Checkout(first);
  const secondCheckout = new Checkout(second);
  second.taxPercent = 10;
  assert.equal(secondCheckout.total(threeThings()), 55, "Not yet: the second shop changed its tax to 10%, so its $50 order costs $55.");
  assert.equal(firstCheckout.total(threeThings()), 54, "Not yet: the first shop's tax is still 8%, so its $50 order still costs $54.");
});
