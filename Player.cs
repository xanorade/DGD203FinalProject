﻿using System;

public class Player
{
	private const int playerMaxHealth = 100;

	private const int playerDefaultMinDamage = 8;
	private const int playerDefaultMaxDamage = 16;

	public string Name { get; private set; }
	public int Health { get; private set; }

	public Inventory Inventory { get; private set; }
	public bool HasAcceptedQuest { get; set; }
    public bool QuestCompleted { get; set; }

    public Player(string name, List<Item> inventoryItems)
	{

        HasAcceptedQuest = false;
        QuestCompleted = false;

        Name = name;
		Health = playerMaxHealth;
		Inventory = new Inventory();

		for (int i = 0; i < inventoryItems.Count; i++)
		{
			Inventory.AddItem(inventoryItems[i]);
		}
	}

	public void TakeItem(Item item)
	{
		Inventory.AddItem(item);
	}

	public void CheckInventory()
	{
		for (int i = 0; i < Inventory.Items.Count; i++)
		{
			Console.WriteLine($"You have a {Inventory.Items[i]}");
		}
	}

	public int Damage()
	{
		Random damageRandom = new Random();
		int damage = damageRandom.Next(playerDefaultMinDamage, playerDefaultMaxDamage + 1);
		return damage;
	}

	public void TakeDamage(int amount)
	{
		Health -= amount;
		if (Health < 0) Health = 0;

		Console.WriteLine($"You take {amount} damage. You have {Health} health left.");

		if (Health <= 0)
		{
			Console.WriteLine("YOU DIED");
		}
	}
}
