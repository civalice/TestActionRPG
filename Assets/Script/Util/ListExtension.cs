using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.Util
{
    public static class ListExtension
    {
        public static T RandomItem<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}