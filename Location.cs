﻿using System;
using System.Numerics;

public class Location
{
    #region VARIABLES


    public string Name { get; private set; }
    public Vector2 Coordinates { get; private set; }
    public LocationType Type { get; private set; }

    public bool CombatAlreadyHappened { get; private set; }

    public List<Item> ItemsOnLocation { get; private set; }

    #endregion

    #region CONSTRUCTOR

    public Location(string locationName, LocationType type, Vector2 coordinates, List<Item> itemsOnLocation)
    {
        Name = locationName;
        Type = type;
        Coordinates = coordinates;
        ItemsOnLocation = itemsOnLocation; 
    }

    public Location(string locationName, LocationType type, Vector2 coordinates)
    {
        Name = locationName;
        Type = type;
        Coordinates = coordinates;
        ItemsOnLocation = new List<Item>(); 
    }

    #endregion

    #region METHODS

    public void RemoveItem(Item item)
    {
        ItemsOnLocation.Remove(item);
    }

    public void CombatHappened()
    {
        if (Type != LocationType.Combat) return;
        CombatAlreadyHappened = true;
    }

    #endregion
}

public enum LocationType
{
    Neighbourhood,
    Combat,
    Station,
    Bazaar,
    House,
    Beach,
    High_School,
}