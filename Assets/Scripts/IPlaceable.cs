using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    //location of the IPlaceable
    GridPos myPos { get; }

    //int to determine to which player the IPlaceable belongs. It's an int so more player could be added and there can be options for IPlaceables that belong to noone
    int blackOrWhite { get;}

    //function to Update the position of an IPlaceable using GridPos
    void UpdatePos(GridPos _pos);

    void UpdateVisual(GridPos _Offset);

    //function to fetch the body of an IPlaceable
    GameObject Get_Body(Checker _prefab);

    //function to destroy the Iplaceable
    void Kill();
}
