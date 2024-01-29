﻿using System;

public class Inventory
{
	public List<Item> Items {  get; private set; }

	public Inventory()
	{
		Items = new List<Item>();
	}

	public void AddItem(Item item)
	{
		if (Items.Contains(item))
		{
			return;
		}

        Items.Add(item);
	}

	public void RemoveItem(Item item)
	{
		if (!Items.Contains(item))
		{
			return;
        }

		Items.Remove(item);
    }

    public bool Contains(Item item)
    {
        return Items.Contains(item);
    }

}

public enum Item
{
	
	Delight,
	IstanbulCard,
}
