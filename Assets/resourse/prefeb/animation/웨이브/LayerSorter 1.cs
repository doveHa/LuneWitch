using UnityEngine;

public class LayerSorter1 : MonoBehaviour
{
    public void SetOrderByLine(int line)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = line * 1;
        }
    }
}
