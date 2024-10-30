public class AttackData
{
    int damage;
    float hitPercent;
    bool doesRandomDamage;

    /// <summary>
    /// a container for the data of an attack
    /// </summary>
    /// <param name="damage">the maximum amount of damage the attack can do</param>
    /// <param name="hitPercent">how often this attack will hit, with 0 meaning the attack will always miss and 1 means the attack will always hit</param>
    /// <param name="doesRandomDamage">true if the attack doesn't always hit for the same amount of damage</param>
    public AttackData(int damage, float hitPercent, bool doesRandomDamage = false)
    {
        this.damage = damage;
        this.doesRandomDamage = doesRandomDamage;
        this.hitPercent = hitPercent;
    }

    /// <summary>
    /// uses the data of the attack to calculate it's damage
    /// </summary>
    /// <returns>the damage the attack does</returns>
    public int GetDamage()
    {
        if (new Random().NextDouble() < hitPercent)
            return doesRandomDamage ? new Random().Next(damage) + 1 : damage;
        else
            return 0;
    }
}
