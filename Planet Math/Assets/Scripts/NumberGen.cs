using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumberGen {

    private int lowBound;
    private int upBound;
    private int[] numbers;
    private int[] exceptions;

    public int LowBound
    {
        get { return lowBound; }
    }

    public int UpBound
    {
        get { return upBound; }
    }

    public int[] Numbers
    {
        get { return numbers; }
    }

    public int[] Exceptions
    {
        get { return exceptions; }
    }

    public NumberGen(int low, int high, int[] nums, int[] excepts)
    {
        lowBound = low;
        upBound = high;
        numbers = nums;
        exceptions = excepts;
    }
}
