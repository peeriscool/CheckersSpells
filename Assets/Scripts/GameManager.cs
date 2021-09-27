using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Checker checkerTest;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
        checkerTest = new Checker(new GridPos(5, 7));
        Debug.Log(GridSystem.checkGridPosition(new GridPos(5, 7)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
