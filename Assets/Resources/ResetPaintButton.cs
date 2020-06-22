using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ResetPaintButton : MonoBehaviourPunCallbacks, IPointerCallbacks
{
    private float buttonDownY = 0.01f;

    private bool onMouse = false;

    // Start is called before the first frame update
    void Start()
    {
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
            transform.Translate(0, buttonDownY, 0);
        }
    }

    public void OnPointerMove()
    {

    }

    void IPointerCallbacks.OnPointerDownOnIt(RaycastHit hit)
    {
        onMouse = true;
        transform.Translate(0, -buttonDownY, 0);
        photonView.RPC("Reset", RpcTarget.All);
    }

    [PunRPC]
    void Reset()
    {
        Texture2D tex = (Texture2D)transform.parent.gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex");
        Color white = Color.white;
        tex.SetPixels(Enumerable.Repeat(white, 1000 * 1000).ToArray());
        tex.Apply();
    }

    void IPointerCallbacks.OnPointerMoveOnIt(RaycastHit hit)
    {
    }

    void IPointerCallbacks.OnPointerUpOnIt(RaycastHit hit)
    {
    }
}
