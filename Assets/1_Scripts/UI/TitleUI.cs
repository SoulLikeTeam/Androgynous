using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    private string _message = null;
    private Text _text;
    private void Awake() {
        _text = GetComponentInChildren<Text>();
    }
    private void Start() {
        StartCoroutine(PlayUI());
    }

    private IEnumerator PlayUI()
    {
        _message = _text.text;
        while(true)
        {
            _text.text = _message;
            yield return new WaitForSeconds(0.5f);
            _text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
