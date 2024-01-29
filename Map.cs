using System;
using System.Numerics;
using System.ComponentModel;
using System.Diagnostics;
using DGD203_2;

public class Map
{
	private Game _theGame;
    private string _isMeditating;
	private Vector2 _coordinates;

	private int[] _widthBoundaries;
	private int[] _heightBoundaries;

	private Location[] _locations;

    private GrandmaNPC _grandmaNPC;


    public Map(Game game, int width, int height)
	{
		_theGame = game;

		
		int widthBoundary = (width - 1) / 2;

        _widthBoundaries = new int[2];
        _widthBoundaries[0] = -widthBoundary;
		_widthBoundaries[1] = widthBoundary;

		
        int heightBoundary = (height - 1) / 2;

        _heightBoundaries = new int[2];
		_heightBoundaries[0] = -heightBoundary;
		_heightBoundaries[1] = heightBoundary;

		
        _coordinates = new Vector2(0,0);

		GenerateLocations();

       
        Console.WriteLine($"Created map with size {width}x{height}");
        Thread.Sleep( 2000);
        Console.WriteLine("You can move with E-W-S-N commands on the map.(use 'help' command if you forget commands.)");
        Thread.Sleep(2000);
        Console.WriteLine("W = WEST = LEFT (means -1,0)");
        Thread.Sleep(2000);
        Console.WriteLine("E = EAST = RIGHT (means 1,0)");
        Thread.Sleep(2000);
        Console.WriteLine("S = SOUTH = DOWN (means 0,-1)");
        Thread.Sleep(2000);
        Console.WriteLine("N = NORTH = UP (means 0,1)");
        Thread.Sleep(2000);
        Console.WriteLine("");
        Console.WriteLine("                 press any key when you're done.");
        Console.ReadKey();
        Console.Clear();
    }

    #region Coordinates

    public Vector2 GetCoordinates()
	{
		return _coordinates;
	}

	public void SetCoordinates(Vector2 newCoordinates)
	{
		_coordinates = newCoordinates;
	}

	#endregion

	#region Movement

	public void MovePlayer(int x, int y)
	{
        if (_theGame.Player.Inventory.Contains(Item.IstanbulCard))
        {
            int newXCoordinate = (int)_coordinates[0] + x;
            int newYCoordinate = (int)_coordinates[1] + y;

            if (!CanMoveTo(newXCoordinate, newYCoordinate))
            {
                Console.WriteLine("You can't go that way");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");
                return;
            }

            _coordinates[0] = newXCoordinate;
            _coordinates[1] = newYCoordinate;

            CheckForLocation(_coordinates);
        }
        else
        {
            int newXCoordinate = (int)_coordinates[0];
            int newYCoordinate = (int)_coordinates[1];
            Console.WriteLine("Have you lost your mind?");
            Thread.Sleep(1000);
            Console.WriteLine("You can't go anywhere without IstanbulCard.");
            Thread.Sleep(1000);
            Console.WriteLine("You can't go everywhere by walking.");
            Thread.Sleep(1000);
            Console.WriteLine("Take the IstanbulCard");
            Thread.Sleep(2000);
            Console.Clear();
            CheckForLocation(_coordinates);

        }
    }

	private bool CanMoveTo(int x, int y)
	{
		return !(x < _widthBoundaries[0] || x > _widthBoundaries[1] || y < _heightBoundaries[0] || y > _heightBoundaries[1]);
	}

	#endregion

	#region Locations

	private void GenerateLocations()
	{
        _locations = new Location[16];

        Vector2 mecidiyekoyMetroLocation = new Vector2(0, 0);
		List<Item> mecidiyekoyMetroItems = new List<Item>();
        mecidiyekoyMetroItems.Add(Item.IstanbulCard);
        Location mecidiyekoy = new Location("Mecidiyeköy Metro", LocationType.Station, mecidiyekoyMetroLocation, mecidiyekoyMetroItems);
        _locations[0] = mecidiyekoy;

        Vector2 kagithaneLocation = new Vector2(-4, 0);
        Location kagithane = new Location("Kağıthane Metro", LocationType.Station, kagithaneLocation);
        _locations[1] = kagithane;

        Vector2 besiktasLocation = new Vector2(3, 0);
        Location besiktas = new Location("Beşiktaş", LocationType.Neighbourhood, besiktasLocation);
        _locations[2] = besiktas;

        Vector2 ayazagaLocation = new Vector2(1, 3);
        Location ayazaga = new Location("Ayazağa", LocationType.Neighbourhood, ayazagaLocation);
        _locations[3] = ayazaga;

        Vector2 alibeykoyLocation = new Vector2(-3, 3);
        Location alibeykoy = new Location("Alibeyköy Metro", LocationType.Station, alibeykoyLocation);
        _locations[4] = alibeykoy;

        Vector2 seyrantepeLocation = new Vector2(4, 4);
        Location seyrantepe = new Location("Seyrantepe Metro", LocationType.Station, seyrantepeLocation);
        _locations[5] = seyrantepe;

        Vector2 aksarayLocation = new Vector2(-1, -3);
        Location aksaray = new Location("Aksaray Metro", LocationType.Station, aksarayLocation);
        _locations[6] = aksaray;

        Vector2 capaLocation = new Vector2(-5, -3);
        Location capa = new Location("Çapa", LocationType.Neighbourhood, capaLocation);
        _locations[7] = capa;

        Vector2 eminonuLocation = new Vector2(3, -4);
        Location eminonu = new Location("Emiönü", LocationType.Neighbourhood, eminonuLocation);
        _locations[8] = eminonu;

        Vector2 firstCombatLocation = new Vector2(2, 4);
        Location firstCombat = new Location("Bum Combat", LocationType.Combat, firstCombatLocation);
        _locations[9] = firstCombat;

        Vector2 secondCombatLocation = new Vector2(5, 3);
        Location secondCombat = new Location("Hooligan Combat", LocationType.Combat, secondCombatLocation);
        _locations[10] = secondCombat;

        Vector2 thirdCombatLocation = new Vector2(1, -4);
        Location thirdCombat = new Location("Immigrant Combat", LocationType.Combat, thirdCombatLocation);
        _locations[13] = thirdCombat;

        Vector2 grandBazaarLocation = new Vector2(4, -4);
        List<Item> grandBazaarItems = new List<Item>();
        grandBazaarItems.Add(Item.Delight);
        Location grandBazaar = new Location("Grand", LocationType.Bazaar, grandBazaarLocation, grandBazaarItems);
		_locations[11] = grandBazaar;

        _grandmaNPC = new GrandmaNPC(new Vector2(-4, 4), Item.Delight);
        Vector2 grandmaLocation = new Vector2(-4, 4);
        Location grandma = new Location("Grandma's", LocationType.House, grandmaLocation);
        _locations[12] = grandma;

        Vector2 sehreminiLocation = new Vector2(-3, -4);
        Location sehremini = new Location("Şehremini", LocationType.High_School, sehreminiLocation);
        _locations[14] = sehremini;

        Vector2 bebekLocation = new Vector2(5, 0);
        Location bebek = new Location("Bebek", LocationType.Beach, bebekLocation);
        _locations[15] = bebek;

    }

    public void CheckForLocation(Vector2 coordinates)
    {
        Thread.Sleep(250);
        Console.Clear();
        Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");

        if (IsOnLocation(_coordinates, out Location location))
        {
            if (location.Type == LocationType.Combat)
            {
                if (location.CombatAlreadyHappened) return;

                Combat combat = new Combat(_theGame, location);

                combat.StartCombat();

            }
            else if (location.Type == LocationType.House)
            {
                Console.WriteLine($"You are in {location.Name} {location.Type}");
                CheckGrandmaInteraction(coordinates);
            }

            else if (location.Type == LocationType.Beach)
            {
                Console.WriteLine($"You are in {location.Name} {location.Type}");
                Thread.Sleep(2000);
                Console.WriteLine("Bebek Beach, bathed in the warm glow of the sun, offered a picturesque view along the Bosphorus.");
                Thread.Sleep(2000);
                Console.WriteLine("The sunlit waves gently lapped against the sandy shores, creating a soothing melody of nature.");
                Thread.Sleep(2000);
                Console.WriteLine("Families and friends gathered, enjoying the sun's embrace as it painted the sky in hues of orange and pink.");
                Thread.Sleep(2000);
                Console.WriteLine("Palm trees swayed in rhythm with the gentle breeze, casting playful shadows on the sun-kissed beach.");
                Thread.Sleep(2000);
                Console.WriteLine("Laughter and relaxation filled the air, making Bebek Beach a delightful escape on that sunny day.");
                Thread.Sleep(2000);
                Console.WriteLine("Do you want to meditate a little? (yes or no)");
                _isMeditating = Console.ReadLine();
                if (_isMeditating == "yes")
                {
                    Meditation();
                }

            }
            else if (location.Type == LocationType.High_School && _theGame.Player.QuestCompleted) 
            {
                Console.Clear();
                Console.WriteLine($"You are in {location.Name} {location.Type}");
                Thread.Sleep(2000);
                Console.WriteLine("You are at the school again");
                Thread.Sleep(2000);
                Console.WriteLine("You arrived just in time");
                Thread.Sleep(2000);
                Console.WriteLine("The school bell rings and class starts");
                Thread.Sleep(2000);
                Console.WriteLine("and you start to daydream like usual");
                Thread.Sleep(2000);
                Console.WriteLine("You fell into a deep sleep");
                Thread.Sleep(2000);
                _theGame.EndGame();
            }

            else
            {
                Console.WriteLine($"You are in {location.Name} {location.Type}");

                if (HasItem(location))
                {
                    Console.WriteLine($"There is a {location.ItemsOnLocation[0]} here");

                }
            }
        }
        Console.WriteLine("Which way you want to go? ");
    }

    private void CheckGrandmaInteraction(Vector2 playerCoordinates)
    {
        if (_grandmaNPC.IsAtLocation(playerCoordinates))
        {
            if (!_theGame.Player.QuestCompleted)
            {
                Console.WriteLine("Grandma says: ");
                Console.WriteLine("Hello dear!");

                if (_theGame.Player.HasAcceptedQuest)
                {                  
                    if (_theGame.Player.Inventory.Contains(Item.Delight))
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine("Oh, thank you for bringing me the Delight! Let's drink some Turkish Coffee .");
                        Thread.Sleep(3000);
                        Console.Clear();
                        Thread.Sleep(1000);
                        Console.WriteLine("After 10-20 mins something sparks in your head.");
                        Thread.Sleep(1000);
                        Console.WriteLine("You were on the lunch break of school.");
                        Thread.Sleep(1000);
                        Console.WriteLine("So you need to find your way to school again befor its too late. ");
                        Thread.Sleep(3000);
                        Console.Clear();
                        Thread.Sleep(1000);
                        _theGame.Player.QuestCompleted = true;

                                   _theGame.Player.Inventory.RemoveItem(Item.Delight);
                        Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine($"I'm still waiting for you to bring {_grandmaNPC.GetRequiredItem()}.");
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Would you like to help me? (Type 'yes' or 'no')");

                    string userResponse = Console.ReadLine();
                    Thread.Sleep(1000);
                    Console.Clear();

                    if (userResponse.ToLower() == "yes")
                    {
                        _theGame.Player.HasAcceptedQuest = true;
                        Console.WriteLine("Grandma says: ");
                        Thread.Sleep(1000);
                        Console.WriteLine("Oh, thank you dear! Can you get Turkish Delight from Grand Bazaar ?");
                        Thread.Sleep(1000);
                        Console.WriteLine("So we can have some time together. I miss you.");
                        Thread.Sleep(1000);
                        Console.WriteLine("I will prepare coffees until you are here again with the delights.");
                        Thread.Sleep(1000);
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");
                    }
                    else
                    {
                        Console.WriteLine("Grandma says:");
                        Thread.Sleep(1000);
                        Console.WriteLine("Oh, that's okay dear.");
                        Thread.Sleep(1000);
                        Console.WriteLine("Come again when you have free time.");
                        Thread.Sleep(1000);
                        Console.WriteLine("I miss you so much.");
                        Thread.Sleep(1000);
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");
                    }
                }
            }
        }
    }

    public void Meditation()
    {
        Console.Clear();
        Console.WriteLine("Meditation has started. Press any key to finish.");

        bool meditationCompleted = false;

        while (!meditationCompleted)
        {
            Console.WriteLine("Breathe in");
            Thread.Sleep(5000);

            if (Console.KeyAvailable)
            {
                Console.ReadKey(); 
                meditationCompleted = true;
            }
            else
            {
                Console.WriteLine("Breathe out");
                Thread.Sleep(5000);
            }
        }

        Console.WriteLine("Meditation has ended.");
        Thread.Sleep(2000);
        Console.Clear() ;
        Console.WriteLine("You feel refreshed. You are ready to get caught up in the flow of life.");
        Thread.Sleep(2000);
        Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");
    }

    private bool IsOnLocation(Vector2 coords, out Location foundLocation)
	{
		for (int i = 0; i < _locations.Length; i++)
		{
			if (_locations[i].Coordinates == coords)
			{
				foundLocation = _locations[i];
				return true;
			}
		}

		foundLocation = null;
		return false;
	}

	private bool HasItem(Location location)
	{
		return location.ItemsOnLocation.Count != 0;

	}

	public void TakeItem(Location location)
	{

	}

	public void TakeItem(Player player, Vector2 coordinates)
	{
		if (IsOnLocation(coordinates, out Location location))
		{
			if (HasItem(location))
			{
				Item itemOnLocation = location.ItemsOnLocation[0];

				player.TakeItem(itemOnLocation);
				location.RemoveItem(itemOnLocation);

				Console.WriteLine($"You took the {itemOnLocation}");
                Thread.Sleep(2000);
                Console.Clear();
                CheckForLocation(_coordinates);
                return;
			}
		}

		Console.WriteLine("There is nothing to take here!");
        Thread.Sleep(2000);
        Console.Clear();
        CheckForLocation(_coordinates);
    }

	public void RemoveItemFromLocation(Item item)
	{
		for (int i = 0; i < _locations.Length; i++)
		{
			if (_locations[i].ItemsOnLocation.Contains(item))
			{
				_locations[i].RemoveItem(item);
			}
		}
	}

	#endregion
}