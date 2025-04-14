using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Common
{
    public static string GetOutputString(List<Vector2> listA)
    {
        return GetOutputString(listA, new List<Vector2>());
    }

    public static string GetOutputString(List<Vector2> listA, List<Vector2> listB)
    {
        return string.Format("Left:,\t Right:\n{0}", Common.PrintVector2List(listA, listB));
    }

    public static string GetOutputString(List<Vector2Int> listA)
    {
        return GetOutputString(listA, new List<Vector2Int>());
    }

    public static string GetOutputString(List<Vector2Int> listA, List<Vector2Int> listB)
    {
        return string.Format("Left:,\t Right:\n{0}", Common.PrintVector2List(listA, listB));
    }

    public static string PrintVector2List(List<Vector2> vector2List)
    {
        string result = string.Empty;
        foreach (Vector2 vector2 in vector2List)
        {
            result += vector2.ToString() + "\n";
        }
        return result;
    }

    public static string PrintVector2List(List<Vector2Int> vector2List)
    {
        string result = string.Empty;
        foreach (Vector2 vector2 in vector2List)
        {
            result += vector2.ToString() + "\n";
        }
        return result;
    }

    public static string PrintVector2List(List<Vector2> leftList, List<Vector2> rightList)
    {
        string result = string.Empty;
        int max = Math.Max(leftList.Count, rightList.Count);

        for (int n = 0; n < max; n++)
        {
            string outLeft = string.Empty;
            if (n < leftList.Count)
            {
                outLeft = leftList[n].ToString();
            }
            string outRight = string.Empty;
            if (n < rightList.Count)
            {
                outRight = rightList[n].ToString();
            }
            result += string.Format("{0},\t{1}\n", outLeft, outRight);
        }
        return result;
    }

    public static string PrintVector2List(List<Vector2Int> leftList, List<Vector2Int> rightList)
    {
        string result = string.Empty;
        int max = Math.Max(leftList.Count, rightList.Count);

        for (int n = 0; n < max; n++)
        {
            string outLeft = string.Empty;
            if (n < leftList.Count)
            {
                outLeft = leftList[n].ToString();
            }
            string outRight = string.Empty;
            if (n < rightList.Count)
            {
                outRight = rightList[n].ToString();
            }
            result += string.Format("{0},\t{1}\n", outLeft, outRight);
        }
        return result;
    }

}
