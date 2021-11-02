using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
    /// <summary>
    /// dkahwihwihae
    /// </summary>
    /// <param name="x">wdadwad</param>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns></returns>
    /// 
    public static bool IsBetween(this int x, int lower, int upper)
    {
        bool n = false;

        if (x >= lower && x <= upper)
            n = true;

        

        return n;
    }
    // Start is called before t
}
