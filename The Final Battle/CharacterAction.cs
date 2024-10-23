public abstract class CharacterAction
{
    protected CharacterAction(string name, string description, AttackData? damage = null)
    {
        Name = name;
        Description = description;
        this.Damage = damage;
    }

    protected string Name { get; }
    protected string Description { get; }
    protected AttackData? Damage { get; }

    /// <summary>
    /// performs the chosen action
    /// </summary>
    /// <param name="actor">the character performing the action</param>
    /// <param name="target">the character to target (if applicable)</param>
    /// <param name="item">the item to use (if applicable)</param>
    public virtual void Perform(Character actor, Character? target = null, IUseItem? item = null)
    {
        if (target != null && Damage != null)
        {
            int attackDamage = Damage.GetDamage();
            Console.WriteLine($"{actor.Name} {Description} {target.Name} {(attackDamage == 0 ? "But it misses" : $"(does {attackDamage} damage)")}");
            target?.TakeDamage(attackDamage);
        }
    }

    public override string ToString() => Name;
}

class DoNothing : CharacterAction
{
    public DoNothing() : base("Do nothing", "does nothing") { }

    public override void Perform(Character actor, Character? _, IUseItem? __)
    {
        Console.WriteLine($"{actor.Name} {Description}");
    }
}

class Punch : CharacterAction { public Punch() : base("Punch", "punches", new(2, 1)) { } }

class BoneCrunch : CharacterAction { public BoneCrunch() : base("Bone Crunch", "crunches", new(1, 0.5f)) { } }

class Unraveling : CharacterAction { public Unraveling() : base("unraveling", "unravels", new(2, 1, true)) { } }
class QuickShot : CharacterAction { public QuickShot() : base("Quick Shot", "quick shots", new AttackData(3, 0.5f)) { } }

class UseHealingPotion : CharacterAction
{
    public UseHealingPotion() : base("Use Healing Potion", "a healing potion") { }

    public override void Perform(Character actor, Character? _, IUseItem? item)
    {
        if (item != null)
        {
            item.UseItem(actor);
            Console.WriteLine($"{actor.Name} uses {Description}");
        }
        else
        {
            Console.WriteLine($"{actor.Name} attempts to use {Description}, but there are none left! ");
        }
    }
}