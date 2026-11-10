# Exercise: The Bakery Order Counter

Exercise code for the lesson **Exercise: The Bakery Order Counter** in the course *Design Patterns, Demystified* on [ask-codey.com](https://www.ask-codey.com).

## The ticket

> **From:** the owner, Sunrise Bakery
>
> Two things for next month.
>
> 1. **Pies launch Monday, at $15.** The new product code is written by the menu team, outside the order code. They'll call `menu.add("pie", ...)` with a function that makes a pie, and from then on a pie has to be orderable on the counter tablet and show up on the price board. After pies, we add something new every month, so this should never need the order code edited again.
> 2. **We're opening a second shop**, across town, where the sales tax is 10% instead of 8%. Each shop's checkout is made with its own settings, `new Checkout(settings)`, and the two shops must never charge each other's tax.
>
> Everything that works today has to keep working.

The code that's there works for one shop and three products. `menu.add` and the `Checkout` constructor are already in place, because the other teams' code calls them, but they don't do anything yet.

## Pick your language

The same exercise is here in five languages. Work in the folder for the language you set up; you only need one.

| Folder | The bakery's code | The tests |
|---|---|---|
| `csharp/` | `BakeryOrders/Bakery.cs` | `BakeryOrders.Tests/BakeryTests.cs` |
| `java/` | `src/main/java/` (`Menu.java`, `Orders.java`, `Settings.java`, `Checkout.java` and the products) | `src/test/java/BakeryTest.java` |
| `python/` | `bakery.py` | `test_bakery.py` |
| `javascript/` | `bakery.js` | `bakery.test.js` |
| `typescript/` | `bakery.ts` | `bakery.test.ts` |

Every folder also has a small demo program that runs a day at the bakery, with a pie written outside the bakery's code, the way the menu team will write it. You don't need to change the tests or the demo.

## Run it and check your work

Open your language's folder in VS Code (**File > Open Folder**), then open its terminal (**Terminal > New Terminal**) and run these commands there.

**C#**

```bash
dotnet run --project BakeryOrders
dotnet test
```

**Java** (the Maven Wrapper downloads Maven the first time, so the first run takes a minute)

```bash
java src/main/java/Demo.java
./mvnw test
```

On Windows in PowerShell, write `.\mvnw test` instead of `./mvnw test`.

**Python** (once: make a virtual environment and install pytest into it)

```bash
python -m venv .venv
```

Then turn it on: `.venv\Scripts\Activate.ps1` in PowerShell on Windows, or `source .venv/bin/activate` on macOS (where the command is `python3`, not `python`). Then:

```bash
pip install -r requirements.txt
python demo.py
python -m pytest
```

**JavaScript**

```bash
node demo.js
npm test
```

**TypeScript** (once: `npm install`, which installs TypeScript)

```bash
node demo.ts
npm test
```

At first, 4 tests pass and 4 fail. The 4 that pass are what the bakery does today: keep them green the whole time, and run the tests after every small change. The 4 that fail are the ticket. A failing test's message starts with **Not yet:** (or **Keep this working:** if you broke something that worked) and says what it expected.

## When you're done

All 8 tests are green, and the demo prints:

```text
Order: Cake, Cupcakes, Cookie box
Price board: Cake: $30, Cupcakes: $12, Cookie box: $8, Pie: $15
First shop's total: $54
Second shop's total: $55
```

<details>
<summary>Stuck?</summary>

- **For the pies:** the order code and the price board each decide, with their own `if` chain, which product goes with a word. Give that decision one home instead: a **simple factory**, and since products are added from outside, one written as **a dictionary from each word to a function that makes the product**, kept inside `Menu`. Then `takeOrder` and the price board both ask the menu. (*Simple Factory: One Place That Says new* and *Factories the Modern Way*.)
- **For the second shop:** checkout reads the one shared `Settings.instance()`, a **Singleton**, so both shops get the same tax. Keep the settings checkout is handed, and use those. (*Why Singleton Is Usually a Trap*.)

</details>

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
