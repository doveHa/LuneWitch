using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    public void SetSortingOrder(int order)
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = order;
        }
    }

    public void SetSortingLayer(string layerName)
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = layerName;
        }
    }
}
