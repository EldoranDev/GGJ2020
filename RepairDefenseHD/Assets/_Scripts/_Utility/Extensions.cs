using System.Collections.Generic;
using UnityEngine;


static class Extensions
{
    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            return default(T);
        }

        var random = Random.Range(0, list.Count);

        return list[random];
    }
}
