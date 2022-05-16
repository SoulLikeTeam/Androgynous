using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance;

    private HpBar _hpbar = null;
    private OnDeadUI _deadUI = null;

    public UIManager()
    {
        _hpbar = GameObject.Find("HPbar").GetComponent<HpBar>();
        _deadUI = GameObject.Find("OnDeadUI").GetComponent<OnDeadUI>();
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
