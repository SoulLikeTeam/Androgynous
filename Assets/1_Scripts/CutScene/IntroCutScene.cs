using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutScene : MonoBehaviour
{
    [SerializeField]
    private RectTransform _questPanel;
    [SerializeField]
    private RectTransform _questMessage;
    [SerializeField]
    private GameObject _nextCut;

    private void Start() {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(1f);
        float scale = 0;
        while(scale<=1)
        {   
            scale += 0.1f;
            _questMessage.localScale = new Vector3(1,scale,1);
            yield return new WaitForSeconds(0.01f);
        }
        Vector2 pos = _questMessage.anchoredPosition;
        _questMessage.anchoredPosition = pos + new Vector2(10,5);
        yield return new WaitForSeconds(0.2f);
        _questMessage.anchoredPosition = pos + new Vector2(-10,-5);
        yield return new WaitForSeconds(0.2f);
        _questMessage.anchoredPosition = pos;
        yield return new WaitForSeconds(5);
        scale = 1;
        while(scale>0)
        {   
            scale -= 0.1f;
            _questMessage.localScale = new Vector3(1,scale,1);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        _questMessage.gameObject.SetActive(false);

        while(scale<=1)
        {   
            scale += 0.1f;
            _questPanel.localScale = new Vector3(1,scale,1);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(3);
        _nextCut.SetActive(true);
        gameObject.SetActive(false);
    }
}
