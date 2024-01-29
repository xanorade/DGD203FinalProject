using System;

public class Immigrant : Enemy
{
    private const int immigrantHealth = 15
        ;

    private const int immigrantMinDamage = 9;
    private const int immigrantMaxDamage = 18;

    public Immigrant()
    {
        Health = immigrantHealth;
        Damage = CalculateRandomDamage();
    }

    private int CalculateRandomDamage()
    {
        Random rand = new Random();
        return rand.Next(immigrantMinDamage, immigrantMaxDamage + 1);
    }
}