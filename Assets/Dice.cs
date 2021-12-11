using System;
using System.Collections;
using System.Collections.Generic;

public class Dice
{
    private static Dice instance;
    public static Dice Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Dice();
            }
            return instance;
        }
    }
    public int roll()
    {
        Random random = new Random();
        return random.Next(1,7);
    }
}

public class CheatDice: Dice
{
    private static Dice instance;
    public static Dice Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CheatDice();
            }
            return instance;
        }
    }
    public int roll()
    {
        Random random = new Random();
        int above = 3;
        if (random.Next(1, 7) > above) return 6; //50% chance to roll 6
        return random.Next(1, 7); //50% chance to roll again as normally
    }
}
