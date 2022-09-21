using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAreaController : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    public bool isDoor;

    private void Awake()
    {
        GameObject panel = transform.Find("Panel").gameObject; //패널 게임 오브젝트 찾기

        panel.SetActive(true); // 패널 표시
        if (Input.GetKeyDown(KeyCode.E)) // 패널이 표시된 후 E키를 누르면 IsDoor 함수 활성화(문열림)
        {
            isDoor = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // 충돌 감지 대상이 Player 라면?
        {
            GameObject canvas = GameObject.FindWithTag("Canvas");

            if (canvas == null)
            {
                return;
            }
        }
    }
}