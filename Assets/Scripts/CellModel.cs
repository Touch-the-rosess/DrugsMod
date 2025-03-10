using System;

public class CellModel
{
    public static void Init()
    {
        CellModel.isEmpty = new bool[127];
        for (int i = 0; i < 127; i++)
        {
            CellModel.isEmpty[i] = false;
        }
        for (int j = 0; j < CellModel.empty.Length; j++)
        {
            CellModel.isEmpty[CellModel.empty[j]] = true;
        }
    }

    public static int[] empty = new int[]
    {
        30,
        31,
        32,
        33,
        34,
        35,
        36,
        37,
        39,
        83
    };

    public static bool[] isEmpty;
}

