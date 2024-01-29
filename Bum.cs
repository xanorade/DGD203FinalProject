using System;

public class Bum : Enemy
{
    private const int bumHealth = 25;

    private const int bumMinDamage = 7;
    private const int bumMaxDamage = 14;

    public Bum()
    {
        Health = bumHealth;
        Damage = CalculateRandomDamage();
    }

    private int CalculateRandomDamage()
    {
        Random rand = new Random();
        return rand.Next(bumMinDamage, bumMaxDamage + 1);
    }
}
