public record CharacterData
{
    public string Name { get; init; }
    public int maxHealth { get; init; }
    public List<CharacterAction> AvalibleActions { get; private set; }

    CharacterData(string name, int health, List<CharacterAction> avalibleActions)
    {
        Name = name;
        maxHealth = health;
        AvalibleActions = avalibleActions;
    }

    /// <summary>
    /// creates a character and gives it a DoNothing action and a Use Healing Item Action by default
    /// </summary>
    /// <param name="name">The name of the character</param>
    /// <param name="health">the maximum amount of health this character can have</param>
    /// <param name="avalibleActions">The actions the character has avalible to it, excluding Do Nothing and Use Healing Item</param>
    /// <returns>teh created CharacterData</returns>
    public static CharacterData CreateAndSetUpCharacter(string name, int health, params CharacterAction[]? avalibleActions)
    {
        List<CharacterAction> actions = avalibleActions?.ToList() ?? new List<CharacterAction>();
        actions.Insert(0, new DoNothing());
        actions.Add(new UseHealingPotion());
        CharacterData newCharacter = new CharacterData(name, health, actions);

        return newCharacter;
    }

    /// <summary>
    /// Adds an action for this character to use
    /// </summary>
    /// <param name="action">the action to add</param>
    public void AddAction(CharacterAction action)
    {
        AvalibleActions.Add(action);
    }

    /// <summary>
    /// Removes an action from the avalible actions
    /// </summary>
    /// <param name="action">the action to remove</param>
    public void RemoveAction(CharacterAction action)
    {
        AvalibleActions.Remove(action);
    }

    public static CharacterData TheTrueProgrammer => CharacterData.CreateAndSetUpCharacter("The True Programmer", 10, new Punch());
    public static CharacterData VinFletcher => CharacterData.CreateAndSetUpCharacter("Vin Fletcher", 10, new QuickShot());
    public static CharacterData Skeleton => CharacterData.CreateAndSetUpCharacter("Skeleton", 10, new BoneCrunch());
    public static CharacterData UncodedOne => CharacterData.CreateAndSetUpCharacter("The Uncoded One", 15, new Unraveling());
}

