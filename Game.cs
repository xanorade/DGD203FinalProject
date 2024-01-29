using System.IO;
using System.Numerics;
using System.Threading;

namespace DGD203_2
{
    public class Game
    {
        #region VARIABLES

        #region Game Constants

        private const int _defaultMapWidth = 11;
        private const int _defaultMapHeight = 11;

        #endregion

        #region Game Variables

        #region Player Variables

        public Player Player { get; private set; }

        private string _playerName;
        private List<Item> _loadedItems;

        #endregion

        #region World Variables

        private Location[] _locations;

        #endregion

        private bool _gameRunning;
        private Map _gameMap;
        private string _playerInput;

        #endregion

        #endregion

        #region METHODS

        #region Initialization

        public void StartGame(Game gameInstanceReference)
        {
            LoadGame();

            CreatePlayer();

            Console.WriteLine("Anyway, you've pulled yourself together.");
            Thread.Sleep(2000);
            Console.WriteLine("You remembered why you were here.");
            Thread.Sleep(2000);
            Console.WriteLine("After all the nonsense you saw at school,");
            Thread.Sleep(2000);
            Console.WriteLine("you just wanted to take a walk to clear your mind.");
            Thread.Sleep(2000);
            Console.WriteLine("When you look around, you realize you're in Mecidiyekoy.");
            Thread.Sleep(2000);
            Console.WriteLine("You've looked all of your pockets but...");
            Thread.Sleep(2000);
            Console.WriteLine("you couldn't find your Istanbul card.");
            Thread.Sleep(2000);
            Console.WriteLine("You think you might have dropped it when...");
            Thread.Sleep(2000);
            Console.WriteLine("getting out of the vehicle, so you start looking around.");
            Thread.Sleep(2000);
            Console.Clear();

            CreateNewMap();

            InitializeGameConditions();

            _gameRunning = true;
            StartGameLoop();
            
        }

        private void CreateNewMap()
        {
            _gameMap = new Map(this, _defaultMapWidth, _defaultMapHeight);
        }

        private void CreatePlayer()
        {
            if (_playerName == null)
            {
                GetPlayerName();
            }

            if (_loadedItems == null)
            {
                _loadedItems = new List<Item>();
            }

            Player = new Player(_playerName, _loadedItems);
        }

        private void GetPlayerName()
        {
            Console.WriteLine("WAKE UP");
            Thread.Sleep(2500);
            Console.Clear();
            Thread.Sleep(1000);
            Console.WriteLine(".");
            Thread.Sleep(1000);
            Console.WriteLine(".");
            Thread.Sleep(1000);
            Console.WriteLine(".");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("                 'THE VEHICLE WILL GO IN THE OPPOSITE DIRECTION!'");
            Thread.Sleep(2000);
            Console.WriteLine("You opened your eyes.");
            Thread.Sleep(2000);
            Console.WriteLine("                 'THE DOORS ARE CLOSING!'");
            Thread.Sleep(2000);
            Console.WriteLine("                 Beep.");
            Thread.Sleep(1000);
            Console.WriteLine("                 Beep.");
            Thread.Sleep(1000);
            Console.WriteLine("                 Beep.");
            Thread.Sleep(2000);
            Console.WriteLine("You left the vehicle immediately.");
            Thread.Sleep(2500);
            Console.Clear();
            Console.WriteLine("You was so tired after all that lessons and topics. ");
            Thread.Sleep(2000);
            Console.WriteLine("You fell asleep at the subway");
            Thread.Sleep(2500);
            Console.WriteLine("Sleep was so good you feel like you lost your memories.");
            Thread.Sleep(2000);
            Console.WriteLine("Do you remember what is your name?");
            _playerName = Console.ReadLine();

            
            if (_playerName == "")
            {
                _playerName = "ㅤㅤㅤㅤㅤㅤ";
                Thread.Sleep(1000);
                Console.WriteLine("You really need to wake up.");
                Thread.Sleep(1000);
                Console.WriteLine("How can't you even remember your name");
                Thread.Sleep(2500);
                Console.Clear();

            }
            else
            {
                Console.WriteLine("Oh.");
                Thread.Sleep(1000);
                Console.WriteLine("OK.");
                Thread.Sleep(1000);
                Console.WriteLine($"At least you know you are {_playerName}");
                Thread.Sleep(2500);
                Console.Clear();
            }
        }

        private void InitializeGameConditions()
        {
            _gameMap.CheckForLocation(_gameMap.GetCoordinates());
        }


        #endregion

        #region Game Loop

        private void StartGameLoop()
        {
            while (_gameRunning)
            {
                GetInput();
                ProcessInput();
                CheckPlayerPulse();
            }
        }

        private void GetInput()
        {
            _playerInput = Console.ReadLine();
        }

        private void ProcessInput()
        {
            if (_playerInput == "" || _playerInput == null)
            {
                Console.WriteLine("I need commands!");
                return;
            }

            switch (_playerInput)
            {
                case "N":
                    _gameMap.MovePlayer(0, 1);
                    break;
                case "S":
                    _gameMap.MovePlayer(0, -1);
                    break;
                case "E":
                    _gameMap.MovePlayer(1, 0);
                    break;
                case "W":
                    _gameMap.MovePlayer(-1, 0);
                    break;
                case "exit":
                    EndGame();
                    break;
                case "save":
                    SaveGame();
                    Console.WriteLine("Game saved");
                    break;
                case "load":
                    LoadGame();
                    Console.WriteLine("Game loaded");
                    break;
                case "help":
                    Console.WriteLine(HelpMessage());
                    break;
                case "where":
                    _gameMap.CheckForLocation(_gameMap.GetCoordinates());
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "who":
                    Console.WriteLine($"You are {Player.Name}. Don't you know who you are?");
                    break;
                case "take":
                    _gameMap.TakeItem(Player, _gameMap.GetCoordinates());
                    break;
                case "inventory":
                    Player.CheckInventory();
                    break;

                default:
                    Console.WriteLine("Command not recognized. Please type 'help' for a list of available commands");
                    break;
            }
        }

        private void CheckPlayerPulse()
        {
            if (Player.Health <= 0)
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            Console.WriteLine("I hope you enjoyed my game!");
            _gameRunning = false;
        }

        #endregion

        #region Save Management

        private void LoadGame()
        {
            string path = SaveFilePath();

            if (!File.Exists(path)) return;
            
            string[] saveContent = File.ReadAllLines(path);

            _playerName = saveContent[0];

            List<int> coords = saveContent[1].Split(',').Select(int.Parse).ToList();
            Vector2 coordArray = new Vector2(coords[0], coords[1]);

            _loadedItems = new List<Item>();

            List<string> itemStrings = saveContent[2].Split(',').ToList();

            for (int i = 0; i < itemStrings.Count; i++)
            {
                if (Enum.TryParse(itemStrings[i], out Item result))
                {
                    Item item = result;
                    _loadedItems.Add(item);
                    _gameMap.RemoveItemFromLocation(item);
                }
            }

            _gameMap.SetCoordinates(coordArray);

        }

        private void SaveGame()
        {
            string xCoord = _gameMap.GetCoordinates()[0].ToString();
            string yCoord = _gameMap.GetCoordinates()[1].ToString();
            string playerCoords = $"{xCoord},{yCoord}";

            List<Item> items = Player.Inventory.Items;
            string playerItems = "";
            for (int i = 0; i < items.Count; i++)
            {
                playerItems += items[i].ToString();
                
                if(i != items.Count -1)
                {
                    playerItems += ",";
                }
            }

            string saveContent = $"{_playerName}{Environment.NewLine}{playerCoords}{Environment.NewLine}{playerItems}";

            string path = SaveFilePath();

            File.WriteAllText(path, saveContent);
        }

        private string SaveFilePath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string path = projectDirectory + @"\save.txt";

            return path;
        }

        #endregion

        #region Miscellaneous

        private string HelpMessage()
        {
            return @"Here are the current commands:
N = NORTH = UP (means 0,1)
S = SOUTH = DOWN (means 0,-1)
W = WEST = LEFT (means -1,0)
E = EAST = RIGHT (means 1,0)
load: Load saved game
save: save current game
exit: exit the game
inventory: view your inventory
take: take the item present on the location
who: view the player information
where: view current location
clear: clear the screen";

        }

        #endregion

        #endregion
    }
}