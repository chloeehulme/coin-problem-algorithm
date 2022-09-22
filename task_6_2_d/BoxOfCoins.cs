using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace BoxOfCoins
{
    public class BoxOfCoins
    {
        public static int Solve(int[] boxes)
        {
            int n = boxes.Length;

            // Create a table to store solutions of subproblems
            int[,] table = new int[n, n];
            int gap, i, j, x, y, z;

            // fill table (memoization)
            for (gap = 0; gap < n; ++gap)
            {
                for (i = 0, j = gap; j < n; ++i, ++j)
                {

                    // here x = (i+2, j), y = (i+1, j-1) and z = (i, j-2)
                    x = ((i + 2) <= j) ? table[i + 2, j] : 0;
                    y = ((i + 1) <= (j - 1)) ? table[i + 1, j - 1] : 0;
                    z = (i <= (j - 2)) ? table[i, j - 2] : 0;

                    // Player 1 takes  box i or box j + the option Player 2 didn't pick (therefore the smaller value)
                    // * This is the recursive formula *
                    table[i, j] = Math.Max(boxes[i] + Math.Min(x, y),
                                           boxes[j] + Math.Min(y, z));
                }
            }

            // maximum number of coins Player 1 is able to collect
            int MaxCoins = table[0, n - 1];

            // effectively ∑ (Player 1's coins) - ∑ (Player 2's coins)
            return 2 * MaxCoins - Sum(boxes, boxes.Length);
        }

        // recursively finds the sum of all elements in the array
        public static int Sum(int[] data, int n)
        {
            if (n == 0) return 0;
            else return Sum(data, n - 1) + data[n - 1];
        }
    }
}