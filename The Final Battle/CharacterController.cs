public class CharacterController
{
    protected Character character;
    protected CharacterData characterData;
    protected CharacterParty party;
    
    public void SetCharacter(Character character, CharacterData characterData, CharacterParty party)
    {
        this.character = character;
        this.characterData = characterData;
        this.party = party;
    }

    /// <summary>
    /// Gets a move from the character's avalible actions, chooses a target, and performs the action on the target
    /// </summary>
    /// <param name="enemies">the party of characters to target</param>
    public virtual void GetAndPerformAction(CharacterParty enemies)
    {
        int index = new Random().Next(characterData.AvalibleActions.Count);
        CharacterAction randAction = characterData.AvalibleActions[index];

        randAction?.Perform(character, GetTarget(enemies), GetHealingItem());
    }

    /// <summary>
    /// Chooses a character from the enemy party to target
    /// </summary>
    /// <param name="enemies">the party of characters to target</param>
    /// <returns>The chosen target</returns>
    protected virtual Character? GetTarget(CharacterParty enemies) => enemies.PartyMembers[0];

    protected virtual IUseItem? GetHealingItem()
    {
        if (party.items.Count > 0)
        {
            IUseItem item = party.items[0];
            return item;
        }
        else
            return null;
    }
}