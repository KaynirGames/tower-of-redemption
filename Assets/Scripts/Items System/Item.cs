public class Item
{
    public ItemSO ItemSO { get; private set; }
    public int Amount { get; private set; }

    public Item(ItemSO itemSO)
    {
        ItemSO = itemSO;
        Amount = 1;
    }

    public void AddAmount()
    {
        Amount += 1;
    }

    public void Use(PlayerCharacter player)
    {
        ItemSO.Use(player);
        Amount--;

        if (Amount == 0)
        {
            player.Inventory.RemoveItem(this);
        }
    }
}
