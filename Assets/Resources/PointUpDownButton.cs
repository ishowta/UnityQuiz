using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PointUpDownButton : MonoBehaviourPunCallbacks, IPointerCallbacks
{
    [SerializeField]
    private int zeroPaddingSize = 3;

    [SerializeField]
    private int pointDiff = 10;

    private Vector3 buttonDownDif;

    private bool onMouse = false;

    private TextMesh pointText;

    // Start is called before the first frame update
    void Start()
    {
        buttonDownDif = new Vector3(0, GetComponent<MeshRenderer>().bounds.size.z / 2, 0);
        pointText = transform.parent.gameObject.GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown()
    {
    }

    public void OnPointerUp()
    {
        if (onMouse)
        {
            onMouse = false;
            transform.Translate(buttonDownDif);
        }
    }

    public void OnPointerMove()
    {

    }

    void IPointerCallbacks.OnPointerDownOnIt(RaycastHit hit)
    {
        onMouse = true;
        transform.Translate(-buttonDownDif);
        photonView.RPC("UpOrDown", RpcTarget.All);
    }

    [PunRPC]
    void UpOrDown()
    {
        switch (gameObject.name)
        {
            case "Up":
                Up();
                break;
            case "Down":
                Down();
                break;
        }
    }

    public void Up()
    {
        pointText.text = (Int32.Parse(pointText.text) + pointDiff).ToString(new String('0', zeroPaddingSize));
    }

    public void Down()
    {
        pointText.text = (Int32.Parse(pointText.text) - pointDiff).ToString(new String('0', zeroPaddingSize));
    }

    void IPointerCallbacks.OnPointerMoveOnIt(RaycastHit hit)
    {
    }

    void IPointerCallbacks.OnPointerUpOnIt(RaycastHit hit)
    {
    }
}
