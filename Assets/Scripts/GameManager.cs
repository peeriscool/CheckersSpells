using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GridSystem grid;
    void Start()
    {
        grid = new GridSystem(10,10);
        foreach (Sprite item in grid.GenerateVisual())
        {
            GameObject.Instantiate(item);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
