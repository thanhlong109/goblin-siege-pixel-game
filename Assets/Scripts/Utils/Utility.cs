
using UnityEngine;

public static class Utility
{
    // Function checking a gameobject layer belong to a layermask
    public static bool IsInLayerMask(GameObject gameObject, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << gameObject.layer)) != 0);
    }
}
