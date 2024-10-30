public class Character
{
    public int currentHealth { get; private set; }
    CharacterController controller;
    CharacterData characterData;
    public event Action<Character>? onDie;
    public string Name { get => characterData.Name; }

    Character(CharacterController controller, CharacterData characterData)
    {
        this.controller = controller;
        this.characterData = characterData;
    }

    public void GetAndPerformAction(CharacterParty enemies) => controller.GetAndPerformAction(enemies);

    /// <summary>
    /// creates a character and sets up the character's health
    /// </summary>
    /// <param name="controller">The concrete implementataion of the CharacterController class this character uses</param>
    /// <param name="characterData">The character data this character uses</param>
    /// <returns>The created character</returns>
    public static Character CreateAndSetUpCharacter(CharacterController controller, CharacterData characterData, CharacterParty party)
    {
        Character newCharacter = new Character(controller, characterData);
        controller.SetCharacter(newCharacter, characterData, party);
        newCharacter.currentHealth = characterData.maxHealth;
        return newCharacter;
    }

    /// <summary>
    /// heals this character by the amount specified and prevents the cahracter from being healed past their max health
    /// </summary>
    /// <param name="amount">the amount to heal the character</param>
    public void Heal(int amount)
    {
        currentHealth = Math.Min(currentHealth + amount, characterData.maxHealth);
    }

    public void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{characterData.Name} has been defeated");
        onDie?.Invoke(this);
    }
}

