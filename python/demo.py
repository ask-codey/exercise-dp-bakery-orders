"""A demo: tries your code on a day at the bakery. You don't need to change it."""
from bakery import Checkout, Menu, Settings, price_board, take_order


class Pie:
    """Next month's product, written outside the bakery's code."""

    name = "Pie"
    price = 15


menu = Menu()
menu.add("pie", Pie)
order = take_order(menu, ["cake", "cupcakes", "cookies"])
print("Order: " + ", ".join(product.name for product in order))
print("Price board: " + ", ".join(price_board(menu, ["cake", "cupcakes", "cookies", "pie"])))
print(f"First shop's total: ${Checkout(Settings(8)).total(order)}")
print(f"Second shop's total: ${Checkout(Settings(10)).total(order)}")
