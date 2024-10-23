public interface IUseItem
{
    public event Action<IUseItem> OnUseItem;
    public void UseItem(Character character)
    {
    }
}

public class HealItem : IUseItem
{
    public event Action<IUseItem>? OnUseItem;
    int healAmount;

    public HealItem(int healAmount)
    {
        this.healAmount = healAmount;
    }

    public void UseItem(Character character)
    {
        character.Heal(healAmount);
        OnUseItem?.Invoke(this);
    }
}
