using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MaskToggleButton : QuizButton
{
    private bool masked = true;
    private GameObject[] maskObjects;
    void Start()
    {
        maskObjects = GameObject.FindGameObjectsWithTag("Mask");
    }

    void Update()
    {

    }

    public override void OnClick(RaycastHit hit)
    {
        photonView.RPC("Toggle", RpcTarget.All);
    }

    [PunRPC]
    void Toggle()
    {
        Debug.Log(masked);
        foreach (var mask in maskObjects)
        {
            mask.SetActive(!masked);
        }
        masked = !masked;
    }
}
