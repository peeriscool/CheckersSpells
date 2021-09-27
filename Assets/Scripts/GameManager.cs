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
        foreach (GameObject item in grid.GenerateVisual())
        {
            GameObject.Instantiate(item) as typeof();
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
