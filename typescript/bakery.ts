// Sunrise Bakery's order code. Read the ticket in README.md first.

export interface Product {
  name: string;
  price: number;
}

export class Cake implements Product { name = "Cake"; price = 30; }
export class Cupcakes implements Product { name = "Cupcakes"; price = 12; }
export class CookieBox implements Product { name = "Cookie box"; price = 8; }

// The shop's settings. The whole app reaches them through Settings.instance().
export class Settings {
  static #instance: Settings | null = null;
  taxPercent: number;

  constructor(taxPercent: number) {
    this.taxPercent = taxPercent;
  }

  static instance(): Settings {
    if (Settings.#instance === null) {
      Settings.#instance = new Settings(8);
    }
    return Settings.#instance;
  }
}

// What the bakery sells.
export class Menu {
  // The new product code calls this: kind is the product's word ("pie"), maker a function that makes one.
  // It does nothing yet.
  add(kind: string, maker: () => Product): void {}
}

// The products for the words the customer tapped, in order. Words the bakery doesn't sell are skipped.
export function takeOrder(menu: Menu, kinds: string[]): Product[] {
  const products: Product[] = [];
  for (const kind of kinds) {
    if (kind === "cake") { products.push(new Cake()); }
    else if (kind === "cupcakes") { products.push(new Cupcakes()); }
    else if (kind === "cookies") { products.push(new CookieBox()); }
  }
  return products;
}

// One line for each product the bakery sells, such as "Cake: $30", for the price board.
export function priceBoard(menu: Menu, kinds: string[]): string[] {
  const lines: string[] = [];
  for (const kind of kinds) {
    let product: Product | null = null;
    if (kind === "cake") { product = new Cake(); }
    else if (kind === "cupcakes") { product = new Cupcakes(); }
    else if (kind === "cookies") { product = new CookieBox(); }
    if (product !== null) { lines.push(`${product.name}: $${product.price}`); }
  }
  return lines;
}

export class Checkout {
  // The second shop's code passes its own settings in here. They aren't used yet.
  constructor(settings: Settings) {}

  // The order's total with the shop's sales tax, in whole dollars.
  total(products: Product[]): number {
    let subtotal = 0;
    for (const product of products) { subtotal += product.price; }
    return subtotal + Math.floor(subtotal * Settings.instance().taxPercent / 100);
  }
}
