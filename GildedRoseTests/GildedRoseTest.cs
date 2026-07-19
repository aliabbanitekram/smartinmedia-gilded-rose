using System.Collections.Generic;
using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void RegularItem_DecreasesSellInAndQuality()
    {
        Item item = UpdateSingleItem("+5 Dexterity Vest", sellIn: 10, quality: 20);

        Assert.Equal(9, item.SellIn);
        Assert.Equal(19, item.Quality);
    }

    [Fact]
    public void RegularItem_DegradesTwiceAsFastAfterSellByDate()
    {
        Item item = UpdateSingleItem("+5 Dexterity Vest", sellIn: 0, quality: 20);

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(18, item.Quality);
    }

    [Fact]
    public void RegularItem_QualityNeverBecomesNegative()
    {
        Item item = UpdateSingleItem("+5 Dexterity Vest", sellIn: 0, quality: 0);

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreasesQualityAsItGetsOlder()
    {
        Item item = UpdateSingleItem("Aged Brie", sellIn: 2, quality: 0);

        Assert.Equal(1, item.SellIn);
        Assert.Equal(1, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreasesQualityTwiceAsFastAfterSellByDate()
    {
        Item item = UpdateSingleItem("Aged Brie", sellIn: 0, quality: 10);

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(12, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverIncreasesAboveFifty()
    {
        Item item = UpdateSingleItem("Aged Brie", sellIn: 2, quality: 50);

        Assert.Equal(1, item.SellIn);
        Assert.Equal(50, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges()
    {
        Item item = UpdateSingleItem("Sulfuras, Hand of Ragnaros", sellIn: 0, quality: 80);

        Assert.Equal(0, item.SellIn);
        Assert.Equal(80, item.Quality);
    }

    [Fact]
    public void BackstagePass_IncreasesQualityByOneWhenMoreThanTenDaysRemain()
    {
        Item item = UpdateSingleItem("Backstage passes to a TAFKAL80ETC concert", sellIn: 11, quality: 20);

        Assert.Equal(10, item.SellIn);
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void BackstagePass_IncreasesQualityByTwoWhenTenDaysOrLessRemain()
    {
        Item item = UpdateSingleItem("Backstage passes to a TAFKAL80ETC concert", sellIn: 10, quality: 20);

        Assert.Equal(9, item.SellIn);
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePass_IncreasesQualityByThreeWhenFiveDaysOrLessRemain()
    {
        Item item = UpdateSingleItem("Backstage passes to a TAFKAL80ETC concert", sellIn: 5, quality: 20);

        Assert.Equal(4, item.SellIn);
        Assert.Equal(23, item.Quality);
    }

    [Fact]
    public void BackstagePass_QualityDropsToZeroAfterConcert()
    {
        Item item = UpdateSingleItem("Backstage passes to a TAFKAL80ETC concert", sellIn: 0, quality: 20);

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void BackstagePass_QualityNeverIncreasesAboveFifty()
    {
        Item item = UpdateSingleItem("Backstage passes to a TAFKAL80ETC concert", sellIn: 10, quality: 49);

        Assert.Equal(9, item.SellIn);
        Assert.Equal(50, item.Quality);
    }

    [Fact]
    public void ConjuredItem_DegradesTwiceAsFastAsNormalItem()
    {
        Item item = UpdateSingleItem("Conjured Mana Cake", sellIn: 3, quality: 6);

        Assert.Equal(2, item.SellIn);
        Assert.Equal(4, item.Quality);
    }

    [Fact]
    public void ConjuredItem_DegradesTwiceAsFastAfterSellByDate()
    {
        Item item = UpdateSingleItem("Conjured Mana Cake", sellIn: 0, quality: 6);

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(2, item.Quality);
    }

    [Fact]
    public void ConjuredItem_QualityNeverBecomesNegative()
    {
        Item item = UpdateSingleItem("Conjured Mana Cake", sellIn: 0, quality: 3);

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(0, item.Quality);
    }

    private static Item UpdateSingleItem(string name, int sellIn, int quality)
    {
        IList<Item> items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
        GildedRose app = new GildedRose(items);

        app.UpdateQuality();

        return items[0];
    }
}
