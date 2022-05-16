using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Image _fillGauge ;
    [SerializeField]
    private Text _hpGauge;
    private void Start() {
        SetHp(1);
    }

    public void SetHp(float value)
    {
        value = value > 1 ? 1 : value;
        value = value < 0 ? 0 : value;
        _fillGauge.transform.localScale = new Vector3(value,1,1);

        //텍스트 퍼센트 표시
        float percent = value * 100f;
        _hpGauge.text = string.Format(percent+"%");
    }

}
