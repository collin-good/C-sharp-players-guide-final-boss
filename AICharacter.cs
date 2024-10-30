internal class AICharacter : CharacterController
{
    public override void GetAndPerformAction(CharacterParty enemies)
    {
        int actionIndex = 0;
        if (characterData.AvalibleActions.Count > 1)
        {
            actionIndex = new Random().Next(characterData.AvalibleActions!.Count) + new Random().Next(characterData.AvalibleActions!.Count - 1);
            //makes AI less likely to do nothing and unable to use a healing potion above 50% health
            if (character.currentHealth <= characterData.maxHealth / 2)
            {
                actionIndex = Math.Clamp(actionIndex, 0, characterData.AvalibleActions!.Count - 1);
            }
            else
            {
                actionIndex = Math.Clamp(actionIndex, 0, characterData.AvalibleActions!.Count - 2);
            }
        }

        characterData.AvalibleActions[actionIndex]?.Perform(character, GetTarget(enemies), GetHealingItem());
    }

    protected override Character? GetTarget(CharacterParty enemies) => enemies.GetRandomCharacter();

}
