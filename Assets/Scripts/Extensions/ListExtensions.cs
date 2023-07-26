using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static void SafeRemovå<T>(this List<T> list, T item)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
        }
    }
    
    public static void SafeAdd<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }

    public static T Pop<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            T item = list[^1];
            list.Remove(item);

            return item;
        }

        return default;
    }
}
