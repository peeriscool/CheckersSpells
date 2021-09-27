using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    //
    private int[] Xrow;
    private int[] Yrow;

    public GridSystem(int sizeX, int sizeY)
    {
        Xrow = new int[sizeX];
        Yrow = new int[sizeY];

        for (int i = 0; i < Xrow.Length; i++)
        {
            Xrow[i] = i;
        }
        for (int i = 0; i < Yrow.Length; i++)
        {
            Yrow[i] = i;
        }
        //Debug stuff
        //foreach (int item in Xrow)
        //{
        //    Debug.Log(item + " X");
        //}
        //foreach (int item in Yrow)
        //{
        //    Debug.Log(item + " Y");
        //}
    }

   public Sprite[] GenerateVisual()
    {
        Sprite[] data = new Sprite[Xrow.Length];
        for (int i = 0; i < Xrow.Length; i++)
        {

            // GameObject a = new GameObject(i.ToString(),typeof (cell));
            Sprite a = Sprite.Create(null,new Rect() ,Vector2.zero);          
            data[i] = a;
        }
        return data;
    }
}
public class cell
{
    cell() { }
}
