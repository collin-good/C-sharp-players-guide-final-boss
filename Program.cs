int _turnNumber = 0, _roundNumber = 0, _numPlayers = 0;
CharacterParty _actingCharacters, _targets;

CharacterParty _heros;
List<CharacterParty> _monsters;
ConsoleColor[] _colors =
{
    ConsoleColor.Blue,
    ConsoleColor.Red
};

//setting up the parties based on the number of players
Console.Write("How many players? 0, 1, or 2? ");
int.TryParse(Console.ReadLine(), out _numPlayers);
switch (_numPlayers)
{
    case 1:
    {
        _heros = CharacterParty.SetUpHeroParty(ControllerType.Player);
        _monsters = CharacterParty.SetUpMonsterParties(ControllerType.AI);
        break;
    }
    case 2:
    {
        _heros = CharacterParty.SetUpHeroParty(ControllerType.Player);
        _monsters = CharacterParty.SetUpMonsterParties(ControllerType.Player);
        break;
    }
    default:
    {
        _heros = CharacterParty.SetUpHeroParty(ControllerType.AI);
        _monsters = CharacterParty.SetUpMonsterParties(ControllerType.AI);
        break;
    }
}
Console.WriteLine();

//loops while there are still rounds avalible and there are still heros left
while (_roundNumber < _monsters.Count && !_heros.IsDefeated)
{
    _actingCharacters = _turnNumber % 2 == 0 ? _heros : _monsters[_roundNumber];
    _targets = _turnNumber % 2 == 0 ? _monsters[_roundNumber] : _heros;

    foreach (Character character in _actingCharacters.PartyMembers)
    {
        //has do be done here or if a previous member of the party defeats an enemy the remaining party member's will have yellow test
        Console.ForegroundColor = _colors[_turnNumber % 2];
        //don't attack if the opposing party is defeated
        if (_targets.IsDefeated)
            break;

        Console.WriteLine($"The {(_turnNumber % 2 == 0 ? "heros" : "monsters")} have {_actingCharacters.items.Count} healing potions left");
        Console.WriteLine($"It is {character.Name}'s turn");
        Console.WriteLine($"{character.Name} has {character.currentHealth} health");

        character.GetAndPerformAction(_targets);
        Console.WriteLine();
    }

    //all the monsters in this round have been defeated
    if (_monsters[_roundNumber].IsDefeated)
    {
        //player should always move first
        _turnNumber = -1;
        //no hero will have more health than this so they are all healed to full
        _heros.healParty(50);
        _roundNumber++;
        if (_roundNumber < _monsters.Count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The heros are elated at their victory and regain their health");
        }
    }

    _turnNumber++;
    Console.ReadLine();
}
Console.WriteLine();

// game is over, display results
Console.ForegroundColor = ConsoleColor.Cyan;
if (_heros.IsDefeated)
{
    Console.WriteLine("The heros were defeated by the overwhelming forces of the Uncoded One, Game Over");
}
else
{
    Console.WriteLine("The heros have successfully defeated the Uncoded Ones forces and have saved the land!");
}