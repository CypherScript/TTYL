using System;
using Sirenix.Utilities;
using UnityEngine;

public static class Utilities
{
    /// <summary>
    /// Measures the distance between two strings
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns>The minimum number of single character edits needed to change one string to the other</returns>
    //https://en.wikipedia.org/wiki/Levenshtein_distance
    public static int LevenshteinDistance(string source, string target)
    {
        if (source == target)
        {
            return 0;
        }

        if (source.Length == 0)
        {
            return target.Length;
        }

        if (target.Length == 0)
        {
            return source.Length;
        }

        // create two work vectors of integer distances
        int[] v0 = new int[target.Length + 1];
        int[] v1 = new int[target.Length + 1];

        // initialize v0 (the previous row of distances)
        // this row is A[0][i]: edit distance for an empty s
        // the distance is just the number of characters to delete from t
        for (int i = 0; i < v0.Length; i++)
            v0[i] = i;

        for (int i = 0; i < source.Length; i++)
        {
            // calculate v1 (current row distances) from the previous row v0

            // first element of v1 is A[i+1][0]
            //   edit distance is delete (i+1) chars from s to match empty t
            v1[0] = i + 1;

            // use formula to fill in the rest of the row
            for (int j = 0; j < target.Length; j++)
            {
                var cost = (source[i] == target[j]) ? 0 : 1;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
            }

            // copy v1 (current row) to v0 (previous row) for next iteration
            for (int j = 0; j < v0.Length; j++)
                v0[j] = v1[j];
        }

        return v1[target.Length];
    }

    /// <summary>
    /// Calculates the percentage similarity between two strings
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns>The percentage similarity from 0 to 1</returns>
    public static float CalculatePercentSimilarity(string source, string target)
    {
        if (source.IsNullOrWhitespace() || target.IsNullOrWhitespace())
        {
            return 0f;
        }
        if (source == target)
        {
            return 1f;
        }

        int numEditsDifferent = LevenshteinDistance(source, target);
        return 1 - (float) numEditsDifferent / Math.Max(source.Length, target.Length);
    }
}
