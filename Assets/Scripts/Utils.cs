using System.Collections;
using System.Collections.Generic;

public static class Utils
{
    public static List<T> Shuffle<T>(List<T> arr) {
        int p = arr.Count;
        for (int n = p-1; n > 0 ; n--) {
            int r = SceneMgr.rng.Next(1, n);
            var t = arr[r];
            arr[r] = arr[n];
            arr[n] = t;
        }

        return arr;
    }
}
