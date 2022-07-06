using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackTurn : Gamestate
{
    //it's black's turn
    private IPlaceable selectedPlaceable;
    bool turnFinished = false;

    public BlackTurn()
    {
        Transitions = new List<StateTransition>();
        Transitions.Add(new StateTransition(typeof(WhiteTurn), () => turnFinished == true));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MovePieces();
    }

    public override void Enter()
    {
        base.Enter();
        turnFinished = false;
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
                if (clickedTile != null)
                    turnFinished = GridSystem.AttackChecker(selectedPlaceable.myPos, clickedPos);

                else if (clickedTile == null)
                    turnFinished = GridSystem.MoveChecker(selectedPlaceable.myPos, clickedPos);

                selectedPlaceable = null;
            }

            else if (selectedPlaceable == null)
            {
                if (clickedTile != null && clickedTile.placeableType == 0)
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
