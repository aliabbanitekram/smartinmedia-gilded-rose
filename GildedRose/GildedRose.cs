using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (Item item in Items)
        {
            if (IsSulfuras(item))
            {
                continue;
            }

            if (IsAgedBrie(item))
            {
                UpdateAgedBrie(item);
                continue;
            }

            if (IsBackstagePass(item))
            {
                UpdateBackstagePass(item);
                continue;
            }

            UpdateNormalItem(item);
        }
    }

    private static void UpdateNormalItem(Item item)
    {
        DecreaseQuality(item);
        DecreaseSellIn(item);

        if (IsPastSellByDate(item))
        {
            DecreaseQuality(item);
        }
    }

    private static void UpdateAgedBrie(Item item)
    {
        IncreaseQuality(item);
        DecreaseSellIn(item);

        if (IsPastSellByDate(item))
        {
            IncreaseQuality(item);
        }
    }

    private static void UpdateBackstagePass(Item item)
    {
        IncreaseQuality(item);

        if (item.SellIn < 11)
        {
            IncreaseQuality(item);
        }

        if (item.SellIn < 6)
        {
            IncreaseQuality(item);
        }

        DecreaseSellIn(item);

        if (IsPastSellByDate(item))
        {
            item.Quality = 0;
        }
    }

    private static void IncreaseQuality(Item item)
    {
        if (item.Quality < 50)
        {
            item.Quality++;
        }
    }

    private static void DecreaseQuality(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality--;
        }
    }

    private static void DecreaseSellIn(Item item)
    {
        item.SellIn--;
    }

    private static bool IsPastSellByDate(Item item)
    {
        return item.SellIn < 0;
    }

    private static bool IsAgedBrie(Item item)
    {
        return item.Name == "Aged Brie";
    }

    private static bool IsBackstagePass(Item item)
    {
        return item.Name == "Backstage passes to a TAFKAL80ETC concert";
    }

    private static bool IsSulfuras(Item item)
    {
        return item.Name == "Sulfuras, Hand of Ragnaros";
    }
}
