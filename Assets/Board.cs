using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public static List<Piece> pieces = new List<Piece>();
    public static List<Vector3> locations = new List<Vector3>();
    public static List<Field>[] fieldList = { new List<Field>(), new List<Field>(), new List<Field>(), new List<Field>() };
    public string hello { get; set; }
}
