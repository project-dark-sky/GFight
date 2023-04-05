using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private float maxValue = 100f;

    private float value;

    [SerializeField]
    private RectTransform topBar;

    [SerializeField]
    private RectTransform bottomBar;

    [SerializeField]
    private float animationSpeed = 10f;

    private float _fullHeight;
    private float TargetHeight => value * _fullHeight / maxValue;
    private Coroutine adjustBarHeightCoroutine;

    private void Start()
    {
        _fullHeight = topBar.rect.height;
        value = maxValue;
    }

    public void setBar(float amount)
    {
        float current = this.value;
        value = Mathf.Clamp(amount, 0, maxValue);
        if (adjustBarHeightCoroutine != null)
        {
            StopCoroutine(adjustBarHeightCoroutine);
        }
        adjustBarHeightCoroutine = StartCoroutine(AdjustBarHeight(amount, current));
    }


    private IEnumerator AdjustBarHeight(float amount, float currentAmount)
    {
        Debug.Log(amount + "  " + currentAmount);
        var suddenChangeBar = amount >= currentAmount ? bottomBar : topBar;
        var slowChangeBar = amount >= currentAmount ? topBar : bottomBar;

        suddenChangeBar.SetHeight(TargetHeight);

        while (Mathf.Abs(slowChangeBar.rect.height - suddenChangeBar.rect.height) > .5f)
        {
            slowChangeBar.SetHeight(Mathf.Lerp(slowChangeBar.rect.height, TargetHeight, Time.deltaTime * animationSpeed));
            yield return null;
        }

        slowChangeBar.SetHeight(TargetHeight);

    }

}
