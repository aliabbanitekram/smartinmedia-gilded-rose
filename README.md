# Gilded Rose Refactoring Kata

This repository contains my C# xUnit solution for the Gilded Rose Refactoring Kata.

The solution keeps the original `Item` class unchanged and adds support for Conjured items while preserving the existing Gilded Rose rules.

## Prerequisites

- .NET SDK `10.0.302`

The SDK version is pinned in `global.json`.

## Build the project

```cmd
dotnet build GildedRose.sln -c Debug
```

## Run the command-line program

For e.g. 10 days:

```cmd
dotnet run --project GildedRose -- 10
```

## Run the tests

```cmd
dotnet test
```

## Approach

I started by replacing the placeholder failing test with focused characterization tests for the existing behavior:

- regular item degradation
- Aged Brie increasing quality
- Sulfuras never changing
- Backstage passes increasing and dropping to zero after the concert
- quality boundaries of `0` and `50`

After the existing behavior was covered, I refactored `GildedRose.UpdateQuality()` into small item-specific methods. This made the rules easier to read before adding the new Conjured behavior.

Finally, I added tests for Conjured items and implemented the feature:

- Conjured items degrade twice as fast as normal items
- after the sell-by date, Conjured items degrade twice as fast again
- quality never becomes negative

## Testing

The test suite contains two kinds of tests:

- focused xUnit tests in `GildedRoseTest.cs` for individual business rules
- an approval test in `ApprovalTest.cs` that verifies the 30-day command-line output against `ApprovalTest.ThirtyDays.verified.txt`

The approval file was updated after implementing Conjured items because the expected 30-day output changed for `Conjured Mana Cake`.

Current result:

```text
Passed: 16, Failed: 0
```
