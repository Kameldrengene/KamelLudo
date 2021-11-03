using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Field {
    public GameObject field = null;
    protected Field nextField = null;
    protected List<Piece> pieces = new List<Piece>();
    public Field(GameObject gameObject)
    {
        field = gameObject;
    }
    public abstract void OnLand(Piece piece);
    public abstract Field NextField();


}


public class StarField : Field
{
    public StarField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField()
    {
        if (this.nextField != null)
            return this.nextField;
        else
            throw new NotImplementedException();
    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on Starfield!");
    }
}

public class NormalField : Field
{
    public NormalField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField()
    {
        if (this.nextField != null)
            return this.nextField;
        else   
            throw new NotImplementedException();
     
    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on NormalField!");
    }
}

public class GlobusField : Field
{

    public GlobusField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField()
    {
        if (this.nextField != null)
            return this.nextField;
        else
            throw new NotImplementedException();

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on GlobusField!");
    }
}

public class SafeHomeField : Field
{
    public SafeHomeField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField()
    {
        if (this.nextField != null)
            return this.nextField;
        else
            throw new NotImplementedException();

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on SafeHomeField!");
    }
}

public class HomeField : Field
{
    public HomeField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField()
    {
        if (this.nextField != null)
            return this.nextField;
        else
            throw new NotImplementedException();

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on HomeField!");
    }
}

public class FinishedField : Field
{
    public FinishedField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField()
    {
        if (this.nextField != null)
            return this.nextField;
        else
            throw new NotImplementedException();

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on FinishedField!");
    }
}