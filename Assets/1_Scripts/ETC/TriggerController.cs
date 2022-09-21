using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField]
    private TriggerType type = TriggerType.None;

    [field:SerializeField]
    public UnityEvent OnTrigger { get; set; }

    private bool _isActive = false;
    private bool _isTriggerActive = true;
    public bool isDoor;

    private enum TriggerType
    {
        None,
        Lever, //한번만 실행
        Button // 여러번 실행

    }

    private void Awake()
    {
        canvas.SetActive(false);
    }
    private void Update()
    {
        TriggerActive();
    }

    private void TriggerActive()
    {
        if(_isActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                switch(type)
                {
                    case TriggerType.Lever:
                        OnTrigger?.Invoke();
                        _isTriggerActive = false;
                        canvas.SetActive(false);
                        _isActive = false;
                        break;
                    case TriggerType.Button:
                        OnTrigger?.Invoke();
                        break;
                    default:
                        break;
                }
               
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&& _isTriggerActive) // 충돌 감지 대상이 Player 라면?
        {
            canvas.SetActive(true);

            if (canvas == null)
            {
                return;
            }

            _isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 충돌 감지 대상이 Player 라면?
        {
            canvas.SetActive(false);
            _isActive = false;
        }
    }
}
