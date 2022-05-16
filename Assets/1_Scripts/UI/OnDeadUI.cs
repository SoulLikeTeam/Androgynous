using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeadUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _deadUI;

    public void ShowDeadUI()
    {
        _deadUI.SetActive(true);
    }
}
