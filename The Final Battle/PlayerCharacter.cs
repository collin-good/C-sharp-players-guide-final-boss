internal class PlayerCharacter : CharacterController
{
    public override void GetAndPerformAction(CharacterParty enemies)
    {
        int playerActionChoice = -1;
        Character? target = null;
        while (playerActionChoice < 0 || playerActionChoice >= characterData.AvalibleActions.Count)
        {
            for (int i = 0; i < characterData.AvalibleActions.Count; i++)
            {
                Console.WriteLine($"{i} - {characterData.AvalibleActions[i]}");
            }
            Console.WriteLine();
            Console.Write("What action would you like to do? ");

            int.TryParse(Console.ReadLine(), out playerActionChoice);
        }

        CharacterAction chosenAction = characterData.AvalibleActions[playerActionChoice];

        if (!chosenAction.GetType().IsInstanceOfType(new DoNothing()) &&
            !chosenAction.GetType().IsInstanceOfType(new UseHealingPotion()))
        {
            target = GetTarget(enemies);
        }

        chosenAction.Perform(character, target, GetHealingItem());
    }

    protected override Character? GetTarget(CharacterParty enemies)
    {
        int playerCharacterChoice = -1;
        while (playerCharacterChoice < 0 || playerCharacterChoice >= enemies.PartyMembers.Count)
        {
            for (int i = 0; i < enemies.PartyMembers.Count; i++)
            {
                Console.WriteLine($"{i} - {enemies.PartyMembers[i].Name}");
            }
            Console.WriteLine();
            Console.Write("Who would you like to target? ");
            int.TryParse(Console.ReadLine(), out playerCharacterChoice);
        }

        return enemies.PartyMembers[playerCharacterChoice] ?? null;
    }
}
