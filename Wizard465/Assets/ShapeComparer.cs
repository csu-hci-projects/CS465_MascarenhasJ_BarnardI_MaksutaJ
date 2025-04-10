using System.Collections.Generic;
using UnityEngine;

public abstract class ShapeComparer : MonoBehaviour, IShapeComparer
{
    public abstract bool Compare(List<Vector2> shape1, List<Vector2> shape2, double threshhold);

    public abstract SpellDefinition CompareAll(List<Vector2> testPoints, List<SpellDefinition> library);

}
