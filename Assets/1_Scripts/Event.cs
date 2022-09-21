using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Event : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject door2;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform SpawnTrm;
    private void Start()
    {
        //OpenDoor();
    }
    public void OpenDoor()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(door.transform.DOMoveY(-14, 1));
        GameManager.Instance.IsEvent = true;
        seq.Append(player.transform.DOMove(new Vector3(149, -10), 0));
        seq.Append(player.transform.DOMove(new Vector3(151, -10), 2));
        TilemapRenderer ren =  door.GetComponent<TilemapRenderer>();
        ren.sortingOrder = 10;
        seq.Append(door.transform.DOMoveY(-9, 1));
        seq.AppendCallback(() =>
        {
            GameManager.Instance.FadeIn();
        });
        seq.AppendInterval(1.5f);
        seq.AppendCallback(() =>
        {
            GameManager.Instance.FadeOut();
            player.transform.position = SpawnTrm.position;
            GameManager.Instance.IsEvent = false;
        });
    }
}
