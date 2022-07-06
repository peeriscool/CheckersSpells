using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable : IPoolable
{
    //location of the IPlaceable
    GridPos myPos { get; }

    //int to determine to which player the IPlaceable belongs. It's an int so more player could be added and there can be options for IPlaceables that belong to no-one
    //0 is black, 1 is white
    //this could be an enum maybe
    int placeableType { get;}

    void InitializePlaceable(GridPos _initPos, int _placeableType);

    //function to Update the position of an IPlaceable using GridPos
    void UpdatePos(GridPos _pos);

    void UpdateVisual(GridPos _Offset);

    //function to fetch the body of an IPlaceable
    GameObject Get_Body();
}
