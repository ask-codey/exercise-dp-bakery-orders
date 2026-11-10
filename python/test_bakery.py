"""The tests for your code. You don't need to change this file.
Each message says what was expected, so a failing test tells you what to look at."""
from bakery import Checkout, Menu, Settings, price_board, take_order


class TestPie:
    name = "Pie"
    price = 15


def names(products):
    return [product.name for product in products]


def three_things():
    return take_order(Menu(), ["cake", "cupcakes", "cookies"])


# What the bakery does today. These pass before you change anything: keep them passing.

def test_an_order_holds_the_products_in_the_order_tapped():
    assert names(three_things()) == ["Cake", "Cupcakes", "Cookie box"], \
        "Keep this working: an order for cake, cupcakes and cookies holds Cake, Cupcakes and Cookie box, in that order."


def test_words_the_bakery_does_not_sell_are_skipped():
    assert names(take_order(Menu(), ["bagel", "cake"])) == ["Cake"], \
        "Keep this working: the bakery doesn't sell bagels, so an order for a bagel and a cake holds only the Cake."


def test_the_price_board_shows_each_product_and_its_price():
    assert price_board(Menu(), ["cake", "cookies"]) == ["Cake: $30", "Cookie box: $8"], \
        'Keep this working: the price board for cake and cookies says "Cake: $30" and "Cookie box: $8".'


def test_checkout_adds_the_shops_tax():
    assert Checkout(Settings(8)).total(three_things()) == 54, \
        "Keep this working: $50 of cake, cupcakes and cookies with 8% tax is $54."


# The ticket's new requirements. These fail until the work is done.

def test_a_product_added_to_the_menu_can_be_ordered():
    menu = Menu()
    menu.add("pie", TestPie)
    assert names(take_order(menu, ["pie", "cake"])) == ["Pie", "Cake"], \
        'Not yet: after menu.add("pie", ...), an order for pie and cake holds Pie and Cake.'


def test_a_product_added_to_the_menu_is_on_the_price_board():
    menu = Menu()
    menu.add("pie", TestPie)
    assert price_board(menu, ["cake", "pie"]) == ["Cake: $30", "Pie: $15"], \
        'Not yet: after menu.add("pie", ...), the price board shows "Pie: $15" too, without anyone editing the price board.'


def test_two_shops_charge_their_own_tax():
    assert Checkout(Settings(8)).total(three_things()) == 54, "Not yet: the first shop's 8% tax makes $50 into $54."
    assert Checkout(Settings(10)).total(three_things()) == 55, "Not yet: the second shop's 10% tax makes $50 into $55."


def test_changing_one_shops_tax_leaves_the_other_alone():
    first = Settings(8)
    second = Settings(8)
    first_checkout = Checkout(first)
    second_checkout = Checkout(second)
    second.tax_percent = 10
    assert second_checkout.total(three_things()) == 55, \
        "Not yet: the second shop changed its tax to 10%, so its $50 order costs $55."
    assert first_checkout.total(three_things()) == 54, \
        "Not yet: the first shop's tax is still 8%, so its $50 order still costs $54."
