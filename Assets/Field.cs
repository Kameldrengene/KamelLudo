using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Field {
    public GameObject field { get; set; }
    public Field nextField { get; set; }
    protected List<Piece> pieces = new List<Piece>();
    protected PieceColor quadrant;  // Must be same as pieces color system
    public Field(GameObject gameObject)
    {
        field = gameObject;
    }
    public List<Piece> getPieces() { return this.pieces; }
    public PieceColor getQuadrant() { return this.quadrant; }
    public abstract void OnLand(Piece piece);
    public abstract Field NextField(Piece piece);


}


public class StarField : Field
{
    public StarField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece)
    {
        if (this.nextField != null)
            return this.nextField;
        else
            throw new NotImplementedException();
    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on Starfield!");

        //Finding the next star/entrance field

        Field nextStarField = this.nextField;
        while (!(nextStarField is StarField || nextStarField is EntranceField))
        {
            nextStarField = nextStarField.NextField(piece);
        }

        if (this.pieces.Count == 0) //no pieces yet
        {
            // handle the jump on the next field
            this.handleJump(piece, nextStarField);
        }

        else if (this.pieces.Count == 1) //one piece on field
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
            {
                this.pieces.RemoveAt(0); // Remove piece and send it to start
                // TODO: add the removed piece to the correct start
            }
            // handle the jump on the next field
            this.handleJump(piece, nextStarField);

        }

        else //2 or more pieces
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
            {
                // The moving piece is sent home
                // TODO: add the current piece to the correct start
            }
            else // The pieces are all the same color
            {
                // handle the jump on the next field
                this.handleJump(piece, nextStarField);
            }
        }
    }

    private void handleJump(Piece piece, Field nextStarField)
    {
        if (nextStarField.getPieces().Count == 0) //no pieces yet
        {
            nextStarField.getPieces().Add(piece); // simply adds the piece to the field
        }

        else if (nextStarField.getPieces().Count == 1) //one piece on field
        {
            if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
            {
                nextStarField.getPieces().RemoveAt(0); // Remove piece and send it to start
                // TODO: add the removed piece to the correct start
            }
            nextStarField.getPieces().Add(piece); //adds the piece to field

        }

        else //2 or more pieces
        {
            if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
            {
                // The moving piece is sent home
                // TODO: add the current piece to the correct start
            }
            else // The pieces are all the same color
            {
                nextStarField.getPieces().Add(piece); // adds the piece to field
            }
        }
    }
}

public class NormalField : Field
{
    public NormalField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece)
    {
        if (this.nextField != null)
            return this.nextField;
        else
            return null;
     
    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on NormalField!");

        if (this.pieces.Count == 0) //no pieces yet
        {
            this.pieces.Add(piece); // simply adds the piece to the field
        }

        else if (this.pieces.Count == 1) //one piece on field
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
            {
                this.pieces.RemoveAt(0); // Remove piece and send it to start
                // TODO: add the removed piece to the correct start
            }
            this.pieces.Add(piece); //adds the piece to field
            
        }

        else //2 or more pieces
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
            {
                // The moving piece is sent home
                // TODO: add the current piece to the correct start
            }
            else // The pieces are all the same color
            {
                this.pieces.Add(piece); // adds the piece to field
            }
        }


    }
}

public class GlobusField : Field
{

    public GlobusField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece)
    {
        if (this.nextField != null)
            return this.nextField;
        else
            return null;

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on GlobusField!");


        if (this.pieces.Count == 0) //no pieces yet
        {
            this.pieces.Add(piece); // simply adds the piece to the field
        }

        else //1 or more pieces
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece(s) are not the same color
            {
                // The moving piece is sent home
                // TODO: add the current piece to the correct start
            }
            else // The pieces are all the same color
            {
                this.pieces.Add(piece); // adds the piece to field
            }
        }
    }
}

public class SafeHomeField : Field
{
    public SafeHomeField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece)
    {
        if (this.nextField != null)
            return this.nextField;
        else
            return null;

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on SafeHomeField!");

        if (this.pieces.Count == 0) //no pieces yet
        {
            this.pieces.Add(piece); // simply adds the piece to the field
        }

        else //1 or more pieces
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece(s) are not the same color
            {
                // Checking if the moving piece is of the starting color. if so, it will send any amount of pieces in the field home.
                // This is because the moving piece is just exiting home. pieces will never land on their SafeHomeField on any other occation than exiting home.
                if (piece.getPieceColor() == this.quadrant)
                {
                    // TODO: The stading piece(s) are sent home
                    this.pieces = new List<Piece>(); //set new list to clear out any pieces on the field
                    this.pieces.Add(piece);
                }
                else
                {
                    // The moving piece is sent home
                    // TODO: add the current piece to the correct start
                }
            }
            else // The pieces are all the same color
            {
                this.pieces.Add(piece); // adds the piece to field
            }
        }
    }
}

public class EntranceField : Field //Is also a StarField. pieces of same color will not jump away from this field.
{
    public EntranceField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece) //Should have piece passed? how will it know to send the correct pieces to finish spaces?
    {
        if (this.nextField != null)
            return this.nextField;
        else
            return null;

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on Entrancefield!");

        //Finding the next star/entrance field

        Field nextStarField = this.nextField;
        while (!(nextStarField is StarField || nextStarField is EntranceField))
        {
            nextStarField = nextStarField.NextField(piece);
        }

        if (this.pieces.Count == 0) //no pieces yet
        {
            // handle the jump on the next field
            this.handleJump(piece, nextStarField);
        }

        else if (this.pieces.Count == 1) //one piece on field
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
            {
                this.pieces.RemoveAt(0); // Remove piece and send it to start
                // TODO: add the removed piece to the correct start
            }
            // handle the jump on the next field
            this.handleJump(piece, nextStarField);

        }

        else //2 or more pieces
        {
            if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
            {
                // The moving piece is sent home
                // TODO: add the current piece to the correct start
            }
            else // The pieces are all the same color
            {
                // handle the jump on the next field
                this.handleJump(piece, nextStarField);
            }
        }
    }

    private void handleJump(Piece piece, Field nextStarField)
    {
        if (piece.getPieceColor() == this.quadrant) //Should not jump
        {
            this.pieces.Add(piece); // Adds the piece to the current field instead
        }

        else if (nextStarField.getPieces().Count == 0) //no pieces yet
        {
            nextStarField.getPieces().Add(piece); // simply adds the piece to the field
        }

        else if (nextStarField.getPieces().Count == 1) //one piece on field
        {
            if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
            {
                nextStarField.getPieces().RemoveAt(0); // Remove piece and send it to start
                // TODO: add the removed piece to the correct start
            }
            nextStarField.getPieces().Add(piece); //adds the piece to field

        }

        else //2 or more pieces
        {
            if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
            {
                // The moving piece is sent home
                // TODO: add the current piece to the correct start
            }
            else // The pieces are all the same color
            {
                nextStarField.getPieces().Add(piece); // adds the piece to field
            }
        }
    }
}

public class HomeField : Field //Not a real field
{
    public HomeField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece)
    {
        if (this.nextField != null)
            return this.nextField;
        else
            return null;

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on HomeField!");
    }
}

public class FinishedField : Field // Not a real field?
{
    public FinishedField(GameObject gameObject) : base(gameObject)
    {

    }
    public override Field NextField(Piece piece)
    {
        if (this.nextField != null)
            return this.nextField;
        else
            return null;

    }

    public override void OnLand(Piece piece)
    {
        Console.WriteLine("Landed on FinishedField!");
    }
}