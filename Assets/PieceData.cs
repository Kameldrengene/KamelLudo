using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PieceData
{
    private PieceColor pieceColor;
    private int pieceID;
    private int fieldID;
    private bool isInPlay;
    private bool isDone;

    public PieceData(PieceColor pc, int pID, int fID, bool inPlay, bool Done)
    {
        this.pieceColor = pc;
        this.pieceID = pID;
        this.fieldID = fID;
        this.isInPlay = inPlay;
        this.isDone = Done;
    }

}

