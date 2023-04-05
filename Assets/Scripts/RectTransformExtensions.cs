using UnityEngine;

public static class RectTransformExtensions
{

    public static void SetWidth(this RectTransform t, float width)
    {
        t.sizeDelta = new Vector3(width, t.rect.height);
    }

    public static void SetHeight(this RectTransform t, float height)
    {
        t.sizeDelta = new Vector3(t.rect.width, height);
    }

}

