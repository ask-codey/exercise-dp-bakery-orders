"""Sunrise Bakery's order code. Read the ticket in README.md first."""


class Cake:
    name = "Cake"
    price = 30


class Cupcakes:
    name = "Cupcakes"
    price = 12


class CookieBox:
    name = "Cookie box"
    price = 8


class Settings:
    """The shop's settings. The whole app reaches them through Settings.instance()."""

    _instance = None

    def __init__(self, tax_percent):
        self.tax_percent = tax_percent

    @classmethod
    def instance(cls):
        if cls._instance is None:
            cls._instance = Settings(8)
        return cls._instance


class Menu:
    """What the bakery sells."""

    def add(self, kind, maker):
        """The new product code calls this: kind is the product's word ("pie"), maker a function that makes one.
        It does nothing yet."""


def take_order(menu, kinds):
    """The products for the words the customer tapped, in order. Words the bakery doesn't sell are skipped."""
    products = []
    for kind in kinds:
        if kind == "cake":
            products.append(Cake())
        elif kind == "cupcakes":
            products.append(Cupcakes())
        elif kind == "cookies":
            products.append(CookieBox())
    return products


def price_board(menu, kinds):
    """One line for each product the bakery sells, such as "Cake: $30", for the price board."""
    lines = []
    for kind in kinds:
        product = None
        if kind == "cake":
            product = Cake()
        elif kind == "cupcakes":
            product = Cupcakes()
        elif kind == "cookies":
            product = CookieBox()
        if product is not None:
            lines.append(f"{product.name}: ${product.price}")
    return lines


class Checkout:
    def __init__(self, settings):
        """The second shop's code passes its own settings in here. They aren't used yet."""

    def total(self, products):
        """The order's total with the shop's sales tax, in whole dollars."""
        subtotal = sum(product.price for product in products)
        return subtotal + subtotal * Settings.instance().tax_percent // 100
