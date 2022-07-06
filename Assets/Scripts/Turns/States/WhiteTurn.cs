using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTurn : Gamestate
{
    //it's white's turn
    private IPlaceable selectedPlaceable;
    bool turnFinished = false;

    public WhiteTurn()
    {
        Transitions = new List<StateTransition>();
        Transitions.Add(new StateTransition(typeof(BlackTurn), () => turnFinished == true));
    }

    public override void Enter()
    {
        base.Enter();
        turnFinished = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MovePieces();
        Debug.Log(turnFinished);
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
                if (clickedTile != null && clickedTile.placeableType == 1)
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
