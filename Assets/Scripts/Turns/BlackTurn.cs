using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackTurn : Gamestate
{
    //it's black's turn
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
                bool succeededTurn = false;
                if (clickedTile != null)
                    succeededTurn = GridSystem.AttackChecker(selectedPlaceable.myPos, clickedPos);

                else if (clickedTile == null)
                    succeededTurn = GridSystem.MoveChecker(selectedPlaceable.myPos, clickedPos);

                selectedPlaceable = null;

                if(succeededTurn == true)
                {
                    //Go to the next player's turn?
                }
            }

            else if (selectedPlaceable == null)
            {
                if (clickedTile != null && clickedTile.blackOrWhite == 0)
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
