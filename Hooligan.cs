using System;

public class Hooligan : Enemy
{
    private const int hooliganHealth = 40
        ;

    private const int hooliganMinDamage = 5;
    private const int hooliganMaxDamage = 10;

    public Hooligan()
    {
        Health = hooliganHealth;
        Damage = CalculateRandomDamage();
    }

    private int CalculateRandomDamage()
    {
        Random rand = new Random();
        return rand.Next(hooliganMinDamage, hooliganMaxDamage + 1);
    }
}