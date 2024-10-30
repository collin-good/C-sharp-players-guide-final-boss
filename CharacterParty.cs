using System.IO;

public record CharacterParty
{
    public List<Character> PartyMembers { get; private set; }
    ControllerType _controller;
    public List<IUseItem> items { get; private set; }

    public bool IsDefeated => PartyMembers.Count == 0;

    CharacterParty(ControllerType controller, List<IUseItem> items)
    {
        PartyMembers = new List<Character>();
        this._controller = controller;
        this.items = items;
    }

    /// <summary>
    /// Creates a character party with the specified number of healing potions
    /// </summary>
    /// <param name="type">the type of characterConstroller this character should use</param>
    /// <param name="numHealingItems">the number of healing potions this party has</param>
    /// <returns>the created CharacterParty</returns>
    public static CharacterParty CreateAndSetUpNewCharacterParty(ControllerType type, int numHealingItems)
    {
        List<IUseItem> healingItems = new List<IUseItem>();

        CharacterParty party = new CharacterParty(type, healingItems);
        for (int i = 0; i < numHealingItems; i++)
        {
            party.AddHealingPotion();
        }

        return party;
    }

    /// <summary>
    /// adds a healing potion to the parties "inventory"
    /// </summary>
    private void AddHealingPotion()
    {
        IUseItem healItem = new HealItem(5);
        healItem.OnUseItem += RemoveHealingPotion;
        items.Add(healItem);
    }

    /// <summary>
    /// removes the specified healing potion from the parties "inventory"
    /// </summary>
    /// <param name="item">The healng pottion to remove</param>
    private void RemoveHealingPotion(IUseItem item)
    {
        items.Remove(item);
        item.OnUseItem -= RemoveHealingPotion;
    }

    /// <summary>
    /// heals each member of the party
    /// </summary>
    /// <param name="amount">the amount of healing each member of the party is given</param>
    public void healParty(int amount)
    {
        foreach (Character character in PartyMembers)
        {
            character.Heal(amount);
        }
    }

    /// <summary>
    /// adds a character to the party with a new instance if the parties controller type
    /// </summary>
    /// <param name="character">the characterData the character uses</param>
    public void AddToParty(CharacterData character)
    {
        Character newCharacter = Character.CreateAndSetUpCharacter(_controller == ControllerType.Player ? new PlayerCharacter() : new AICharacter(), character, this);
        PartyMembers.Add(newCharacter);
        newCharacter.onDie += RemoveFromParty;
    }

    /// <summary>
    /// removes a character from the party and unsubscibes this from it's onDie event
    /// </summary>
    /// <param name="character">the character to remove from the party</param>
    private void RemoveFromParty(Character character)
    {
        PartyMembers.Remove(character);
        character.onDie -= RemoveFromParty;
    }

    /// <summary>
    /// returns a random character from the party
    /// </summary>
    /// <returns>the random party member chosen</returns>
    public Character GetRandomCharacter()
    {
        int i = new Random().Next(PartyMembers.Count);
        return PartyMembers[i];
    }

    public static CharacterParty SetUpHeroParty(ControllerType type)
    {
        CharacterParty party = CreateAndSetUpNewCharacterParty(type, 3);
        party.AddToParty(CharacterData.TheTrueProgrammer);
        party.AddToParty(CharacterData.VinFletcher);
        return party;
    }

    public static List<CharacterParty> SetUpMonsterParties(ControllerType type)
    {
        List<CharacterParty> enemyParties = new List<CharacterParty>();
        enemyParties.Add(SetUpRoundOneMonsters(type));
        enemyParties.Add(SetUpRoundTwoMonsters(type));
        enemyParties.Add(SetUpRoundThreeMonsters(type));
        return enemyParties;
    }
    private static CharacterParty SetUpRoundOneMonsters(ControllerType type)
    {
        CharacterParty party = CreateAndSetUpNewCharacterParty(type, 3);
        party.AddToParty(CharacterData.Skeleton);
        return party;
    }
    private static CharacterParty SetUpRoundTwoMonsters(ControllerType type)
    {
        CharacterParty party = CreateAndSetUpNewCharacterParty(type, 3);
        party.AddToParty(CharacterData.Skeleton);
        party.AddToParty(CharacterData.Skeleton);
        return party;
    }
    private static CharacterParty SetUpRoundThreeMonsters(ControllerType type)
    {
        CharacterParty party = CreateAndSetUpNewCharacterParty(type, 2);
        party.AddToParty(CharacterData.Skeleton);
        party.AddToParty(CharacterData.Skeleton);
        party.AddToParty(CharacterData.UncodedOne);
        return party;
    }
}

public enum ControllerType
{
    AI,
    Player
}

