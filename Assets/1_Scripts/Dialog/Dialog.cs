using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private DialogSO _logData;

    [SerializeField]
    private Text _sentenceText;


    private int currentPage = 0;
    private bool _isTouch = false;


    public void PrintSentence()
    {
        if (currentPage < _logData.sentenceList.Count)
        {
            SetDialog(_logData.sentenceList[currentPage]);
            currentPage++;
        }
    }
    public void InitDialog()
    {
        currentPage = 0;
    }
    private void SetDialog(string sentence)
    {
        StartCoroutine(StartDialog(sentence));
    }
    public void TouchPanel()
    {
        _isTouch = true;
    }
    private IEnumerator StartDialog(string sentence)
    {
        _isTouch = false;
        _sentenceText.text = "";
        foreach (char latter in sentence.ToCharArray())
        {
            _sentenceText.text += latter;
            if (!_isTouch)
            {
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return new WaitForSeconds(1.2f);
    }
}
