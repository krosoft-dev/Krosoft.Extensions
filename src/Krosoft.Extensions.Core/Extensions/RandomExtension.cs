﻿namespace Krosoft.Extensions.Core.Extensions;

public static class RandomExtension
{
    /// <summary>
    /// Return a random item from a list.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="rnd">The Random instance.</param>
    /// <param name="list">The list to choose from.</param>
    /// <returns>A randomly selected item from the list.</returns>
    public static T Next<T>(this Random rnd, IList<T> list) => list[rnd.Next(list.Count)];
}