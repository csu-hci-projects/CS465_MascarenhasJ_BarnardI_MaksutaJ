using System;
using System.Collections.Generic;
using UnityEngine;

public interface IShapeComparer
{
    Boolean Compare(List<Vector2> shape1, List<Vector2> shape2);
}
