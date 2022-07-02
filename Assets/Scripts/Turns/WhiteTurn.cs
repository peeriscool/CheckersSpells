using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTurn : Gamestate
{
    //it's white's turn
    private IPlaceable selectedPlaceable;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MovePieces();
    }
    void MovePieces()
    {
        //replace this with input from the inputHandler?
        if (Input.GetMouseButtonDown(0))
        {
            IPlaceable clickedTile = GridSystem.checkGridPosition(GridSystem.ClickOnTiles());
            GridPos clickedPos = GridSystem.ClickOnTiles();

            if (selectedPlaceable != null)
            {
                bool turnFinished = false;

                if (clickedTile != null)
                    turnFinished = GridSystem.AttackChecker(selectedPlaceable.myPos, clickedPos);

                else if (clickedTile == null)
                    turnFinished = GridSystem.MoveChecker(selectedPlaceable.myPos, clickedPos);

                selectedPlaceable = null;

            }

            else if (selectedPlaceable == null)
            {
                if (clickedTile != null && clickedTile.blackOrWhite == 1)
                    selectedPlaceable = clickedTile;
            }
        }
    }

    public override void OnSwitch()
    {
        base.OnSwitch();
        Camera.main.transform.Rotate(0, 0, 180);
    }
}
