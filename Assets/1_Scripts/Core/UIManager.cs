using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance;

    private HpBar _hpbar = null;
    private OnDeadUI _deadUI = null;
    private GameObject _GameMenu = null;

    public UIManager()
    {
        _hpbar = GameObject.Find("HPbar").GetComponent<HpBar>();
        _deadUI = GameObject.Find("OnDeadUI").GetComponent<OnDeadUI>();
        //_GameMenu = GameObject.Find("EscPanel").GetComponent<GameObject>();
    }

    private void Update() {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(_GameMenu.activeSelf)
            {
                _GameMenu.SetActive(true);
            }
            else
            {
                _GameMenu.SetActive(false);
            }
        }
    }

    public void UpdateHpGauge(float value)
    {
        _hpbar.SetHp(value);
    }

    public void PlayDaedAction()
    {
        _deadUI.ShowDeadUI();
    }
}
