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
