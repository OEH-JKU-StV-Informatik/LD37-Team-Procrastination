using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public int fieldHeight = 5;
    public int fieldWidth = 5;

    private FieldType[,] field;

    // Use this for initialization
    void Start()
    {
        field = new FieldType[fieldWidth, fieldHeight];

        instantiateField();

    }

    private void instantiateField()
    {
        for (int x = 0; x < field.GetLength(1); x++)
            for (int y = 0; y < field.GetLength(0); y++)
            {
                var ft = field[x, y];

                if (ft == FieldType.Empty)
                {
                    // Instantiate();
                }
            }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
