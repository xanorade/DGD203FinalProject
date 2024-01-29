using DGD203_2;
using System;
using System.Numerics;

public class GrandmaNPC
{
    private Vector2 _coordinates;
    private Item _requiredItem;

    public GrandmaNPC(Vector2 coordinates, Item requiredItem)
    {
        _coordinates = coordinates;
        _requiredItem = requiredItem;
    }

    public bool IsAtLocation(Vector2 playerCoordinates)
    {
        return _coordinates == playerCoordinates;
    }

    public Item GetRequiredItem()
    {
        return _requiredItem;
    }
}
