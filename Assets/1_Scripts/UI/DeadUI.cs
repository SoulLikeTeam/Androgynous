using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadUI : MonoBehaviour
{
    [SerializeField]
    private float _fadespeed = 5f;
    [SerializeField]
    private float _reloadTime = 0.5f;
    [SerializeField]
    private GameObject _deadPanel;
    [SerializeField]
    private Text _text;

    private Image _deadImage;
    private bool _isReload = false;
    private bool _isRestart = false;

    private void Awake() {
        _deadImage = _deadPanel.GetComponent<Image>();
    }
    private void OnEnable() {
        Init();
    }
    private void Init()
    {   
        if(_deadImage == null)
        {
            Debug.LogError("No have DeadImage Object");
        }
        _deadImage.color = new Color(0,0,0,0);
        _text.color = Color.red;
        _text.text = "System Down";
        _isReload = false;
        _isRestart = false;
    }
    private void Update() {
        PlayDeadUI();
        PlayRestartUI();
    }
    private void PlayRestartUI()
    {
        if(!_isRestart) return;
        if(_deadImage.color.a <= 0) gameObject.SetActive(false);
        _deadImage.color -= new Color(0,0,0,0.1f) * _fadespeed * Time.deltaTime;

    }
    public void PlayDeadUI()
    {
        if(_isReload)return;
        if(_deadImage.color.a <= 1)
        {
             _deadImage.color += new Color(0,0,0,0.1f) * _fadespeed * Time.deltaTime;
        }
        else
        {
            _isReload = true;
            StartCoroutine(ReloadAnimaion());
        }
    }

    private IEnumerator ReloadAnimaion()
    {

        yield return new WaitForSeconds(1);
        _text.text = "";
        yield return new WaitForSeconds(1f);
        _text.color = Color.green;


        int i = 0;
        while(i<3)
        {
            _text.text = "Reload";
            yield return new WaitForSeconds(_reloadTime);
            _text.text = "Reload.";
            yield return new WaitForSeconds(_reloadTime);
            _text.text = "Reload..";
            yield return new WaitForSeconds(_reloadTime);
            _text.text = "Reload...";
            yield return new WaitForSeconds(_reloadTime);
            i++;
        }
        _text.text = "";
        yield return new WaitForSeconds(1.8f);
        _isRestart = true;
    }

}
