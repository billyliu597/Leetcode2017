using System;
using System.Generic;
using System.Collection;
using System.Collection.Generic;

public class Solution {
    bool Helper(IList<double> list, double target){
        if (list.Count == 1){
            return Math.Abs(list[0] - target) <= 1e-6;
        }
        
        for (int i = 0; i < list.Count; i ++){
            for (int j = 0; j < list.Count; j ++){
                if (i == j){
                    continue;
                }
                
                
                // A hard copy of the list.
                IList<double> copyList = list.ToList();
                
                copyList.RemoveAt(Math.Max(i, j));
                copyList.RemoveAt(Math.Min(i, j));
                
                copyList.Add(list[i] + list[j]);
                if (Helper(copyList, target)){
                    return true;
                }
                
                copyList[copyList.Count - 1] = list[i] - list[j];
                if (Helper(copyList, target)){
                    return true;
                }
                
                copyList[copyList.Count - 1] = list[i] * list[j];
                if (Helper(copyList, target)){
                    return true;
                }
                
                copyList[copyList.Count - 1] = list[i] / list[j];
                if (Helper(copyList, target)){
                    return true;
                }
            }
        }
        
        return false;
    }
    
    public bool JudgePointN(int[] nums, int K) {
        IList<double> list = new List<double>();
        foreach (var num in nums){
            list.Add(num);
        }
        return Helper(list, K);
    }
}
