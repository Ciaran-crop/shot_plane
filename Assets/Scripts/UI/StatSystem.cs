using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSystem : MonoBehaviour
{
    protected float targetFillAmount;
    protected float curFillAmount;
    [SerializeField] protected Image frontStatImage;
    [SerializeField] protected Image backStatImage;

    [SerializeField] protected bool fillDelay = true;
    [SerializeField] protected float delayFillTime = 0.2f;
    [SerializeField] protected float fillSpeed = 0.1f;
    protected float t;
    protected WaitForSecondsRealtime waitForDelayFill;
    float curFillAmountDefault;

    Coroutine bufferedStatCoroutine;


    protected virtual void Awake()
    {
        waitForDelayFill = new WaitForSecondsRealtime(delayFillTime);
        if (TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual void Initialize(float curStat, float maxStat)
    {
        targetFillAmount = curStat / maxStat;
        curFillAmount = targetFillAmount;
        backStatImage.fillAmount = targetFillAmount;
        frontStatImage.fillAmount = targetFillAmount;
    }

    public virtual void UpdateStat(float targetStat, float maxStat)
    {
        if (bufferedStatCoroutine != null)
        {
            StopCoroutine(bufferedStatCoroutine);
        }

        targetFillAmount = targetStat / maxStat;

        if (targetFillAmount < curFillAmount)
        {
            if (frontStatImage != null)
            {
                frontStatImage.fillAmount = targetFillAmount;
                if(gameObject.activeSelf) StartCoroutine(BufferedStatCoroutine(backStatImage));
            }
        }
        else if (targetFillAmount > curFillAmount)
        {
            if (backStatImage != null)
            {
                backStatImage.fillAmount = targetFillAmount;
                if(gameObject.activeSelf) StartCoroutine(BufferedStatCoroutine(frontStatImage));
            }
        }
    }

    protected virtual IEnumerator BufferedStatCoroutine(Image image)
    {
        if (fillDelay)
        {
            yield return waitForDelayFill;
        }
        t = 0f;
        curFillAmountDefault = curFillAmount;
        while (t <= 1f)
        {
            t += Time.deltaTime * fillSpeed;
            curFillAmount = Mathf.Lerp(curFillAmountDefault, targetFillAmount, t);
            image.fillAmount = curFillAmount;
            yield return null;
        }
    }

}
