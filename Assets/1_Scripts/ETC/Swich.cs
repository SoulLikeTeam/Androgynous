using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAreaController : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    public bool isDoor;

    private void Awake()
    {
        GameObject panel = transform.Find("Panel").gameObject; //�г� ���� ������Ʈ ã��

        panel.SetActive(true); // �г� ǥ��
        if (Input.GetKeyDown(KeyCode.E)) // �г��� ǥ�õ� �� EŰ�� ������ IsDoor �Լ� Ȱ��ȭ(������)
        {
            isDoor = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // �浹 ���� ����� Player ���?
        {
            GameObject canvas = GameObject.FindWithTag("Canvas");

            if (canvas == null)
            {
                return;
            }
        }
    }
}