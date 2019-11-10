
namespace dummycode
{
    using System;
    using System.Collections.Generic;

    public class Minmove
    {
        public int MinMove(string s, char[][] keyboard)
        {
            Dictionary<char, int> row = new Dictionary<char, int>();
            Dictionary<char, int> col = new Dictionary<char, int>();

            for (int i = 0; i < keyboard.Length; i++)
            {
                for (int j = 0; j < keyboard[i].Length; j++)
                {
                    row.Add(keyboard[i][j], i);
                    col.Add(keyboard[i][j], j);
                }
            }

            Dictionary<char, Dictionary<char, int>> moves = new Dictionary<char, Dictionary<char, int>>();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                moves.Add(c, new Dictionary<char, int>());
                for (char t = 'A'; t <= 'Z'; t++)
                {
                    moves[c].Add(t, Math.Abs(row[t] - row[c]) + Math.Abs(col[t] - col[c]));
                }
            }

            // one point is on s[i], another point is on 'A' - 'Z'
            int[,] dp = new int[s.Length, 26];

            for (int i = 1; i < s.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    dp[i, j] = int.MaxValue;

                    for (int m = 0; m < 26; m++)
                    {
                        // move point1 from s[i-1] to s[i], and the other one from m + 'A' to (char)(j + 'A').
                        dp[i, j] = Math.Min(dp[i, j], dp[i - 1, m] + moves[s[i - 1]][s[i]] + moves[(char)(m + 'A')][(char)(j + 'A')]);

                        // move point1 from m + 'A' to s[i], and the other one from s[i-1] to (char)(j + 'A').
                        dp[i, j] = Math.Min(dp[i, j], dp[i - 1, m] + moves[(char)(m + 'A')][s[i]] + moves[s[i - 1]][(char)(j + 'A')]);
                    }
                }
            }
            int ans = int.MaxValue;
            for (int i = 0; i < 26; i++)
            {
                ans = Math.Min(ans, dp[s.Length - 1, i]);
            }
            return ans;
        }


        public void Test()
        {

            char[][] cars = new char[4][];
            cars[0] = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            cars[1] = new char[] { 'H', 'I', 'J', 'K', 'L', 'M', 'N' };
            cars[2] = new char[] { 'O', 'P', 'Q', 'R', 'S', 'T' };
            cars[3] = new char[] { 'U', 'V', 'W', 'X', 'Y', 'Z' };

            var ans = MinMove("TODA", cars);

            ans = MinMove("AB", cars);
        }
    }
}
