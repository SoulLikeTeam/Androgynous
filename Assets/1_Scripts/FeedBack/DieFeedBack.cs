using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieFeedBack : FeedBack
{
    [SerializeField]
    private float _waitforeTime;

    public UnityEvent DeathCallBack;
    public override void CompletePrevFeedBack()
    {
        StopAllCoroutines();
    }

    public override void CreateFeedBack()
    {
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {

        yield return new WaitForSeconds(_waitforeTime);
        DeathCallBack?.Invoke();
    }
}
