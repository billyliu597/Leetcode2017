 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LeetCode
{
    public class Solution
    {
       int[] GetIndex(int[] cnt, int k, Dictionary<int, long> perCnt)
        {
            int[] ans = new int[2];
            long tillNow = 0;
            for (int i = cnt.Length - 1; i >= 0; i--)
            {
                if (cnt[i] != 0)
                {
                    cnt[i]--;
                    long remain = 1;
                    int total = 0;
                    for (int j = cnt.Length - 1; j >= 0; j--)
                    {
                        total += cnt[j];
                        remain *= perCnt[cnt[j]];
                    }

                    remain = perCnt[total]/remain;
                    tillNow += remain;

                    if (tillNow >= k)
                    {
                        ans[0] = i;
                        ans[1] = (int)(tillNow - remain);
                        break;
                    }
                    else
                    {
                        cnt[i]++;
                    }
                }
            }

            return ans;
        }

        public string KthPermutation(char[] chars, int K)
        {
            int[] cnt = new int[26];
            foreach (var c in chars)
            {
                cnt[(int) (c - 'A')]++;
            }
            // 20! > long.MaxValue. So chars length should be less than 20. 
            Dictionary<int, long> perCnt = new Dictionary<int, long>();
            perCnt.Add(0, 1);
            long per = 1;
            for (int i = 1; i <= chars.Length; i++)
            {
                per *= i;
                perCnt.Add(i, per);
            }

            StringBuilder sb = new StringBuilder();
            while (sb.Length < chars.Length)
            {
                var idx = GetIndex(cnt, K, perCnt);
                sb.Append((char) ('A' + idx[0]));
                K -= idx[1];
            }
            return sb.ToString();
        }
    }
}
