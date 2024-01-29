using DGD203_2;
using System;

public class Combat
{
    #region REFERENCES

    private Game _theGame;
    public Player Player { get; private set; }

    public List<Enemy> Enemies { get; private set; }

    private Location _location;

    #endregion

    #region VARIABLES

    private const int maxNumberOfEnemies = 3;

    private bool _isOngoing;


    private string _playerInput;

    #endregion

    #region CONSTRUCTOR

    public Combat(Game game, Location location)
{
    _theGame = game;
    Player = game.Player;

    _isOngoing = false;

    _location = location;

    Random rand = new Random();
    int numberOfEnemies = rand.Next(1, maxNumberOfEnemies + 1);

    Enemies = new List<Enemy>();

        for (int i = 0; i < numberOfEnemies; i++)
    {
        Enemy nextEnemy;

        if (_location.Name == "Bum Combat")
        {
            nextEnemy = new Bum();
        }
        else if (_location.Name == "Hooligan Combat")
        {
            nextEnemy = new Hooligan();
        }
        else
        {
            nextEnemy = new Immigrant();
        }

        Enemies.Add(nextEnemy);
    }
}

    #endregion


    #region METHODS

    #region Initialization & Loop

    public void StartCombat()
    {
        _isOngoing = true;

        Console.WriteLine($"Prepare to fight {GetEnemyTypeName()}!");

        while (_isOngoing)
        {
            GetInput();
            ProcessInput();

            if (!_isOngoing) break;

            ProcessEnemyActions();
            CheckPlayerPulse();
        }
    }

    private string GetEnemyTypeName()
    {
        if (_location.Name == "Bum Combat")
        {
            return "Bums";
        }
        else if (_location.Name == "Hooligan Combat")
        {
            return "Hooligans";
        }
        else if (_location.Name == "Immigrant Combat")
        {
            return "Immigrants";
        }
        else
        {
            return "Unknown Enemy";
        }
    }

    private void GetInput()
    {
        string enemyTypeName = GetEnemyTypeName();

        Console.WriteLine($"There are {Enemies.Count} {enemyTypeName}(s) in front of you. What do you want to do?");
        for (int i = 0; i < Enemies.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]: Attack {enemyTypeName} {i + 1}");
        }
        Console.WriteLine($"[{Enemies.Count + 1}]: Try to flee (50% chance)");
        _playerInput = Console.ReadLine();
        Thread.Sleep(500);
        Console.Clear();
    }

    private void ProcessInput()
    {
        if (_playerInput == "" || _playerInput == null)
        {
            Console.WriteLine("You can't just stand still, they will attack you!");
            return;
        }

        ProcessChoice(_playerInput);
    }


    private void ProcessChoice(string choice)
    {
        if (Int32.TryParse(choice, out int value)) 
        {
            if (value > Enemies.Count + 1)
            {
                Console.WriteLine("That is not a valid choice");
            }
            else
            {
                if (value == Enemies.Count + 1)
                {
                    TryToFlee();
                }
                else
                {
                    HitEnemy(value);
                }
            }
        }
        else 
        {
            Console.WriteLine("You don't make any sense. Quit babbling, they are going to kill you!");
        }
    }

    private void CheckPlayerPulse()
    {
        if (Player.Health <= 0)
        {
            EndCombat();
        }
    }

    private void EndCombat()
    {
        _isOngoing = false;
        _location.CombatHappened();
    }

    #endregion

    #region Combat

    private void TryToFlee()
    {
        string enemyTypeName = GetEnemyTypeName();
        Random rand = new Random();
        double randomNumber = rand.NextDouble();

        if (randomNumber >= 0.5f)
        {
            Console.WriteLine("You flee! You have no enemies.");
            EndCombat();
        }
        else
        {
            Console.WriteLine($"You cannot flee because {enemyTypeName} is in your way.");
        }
    }

    private void HitEnemy(int index)
    {
        string enemyTypeName = GetEnemyTypeName();
        int enemyIndex = index - 1;
        int playerDamage = Player.Damage();

        Enemies[enemyIndex].TakeDamage(playerDamage);
        Console.WriteLine($"The {enemyTypeName} takes {playerDamage} damage!");

        if (Enemies[enemyIndex].Health <= 0)
        {
            Console.WriteLine($"You defeated these {enemyTypeName} ");
            Enemies.RemoveAt(enemyIndex);
        }
    }

    private void ProcessEnemyActions()
    {
        if (Enemies.Count == 0)
        {
            Console.WriteLine("You defeated all your enemies!");
            EndCombat();
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            int goblinDamage = Enemies[i].Damage;
            Player.TakeDamage(goblinDamage);
        }
    }



    #endregion

    #endregion

}
