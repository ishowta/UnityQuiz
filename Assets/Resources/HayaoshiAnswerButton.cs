using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(AudioSource))]
public class HayaoshiAnswerButton : MonoBehaviourPunCallbacks, IPointerCallbacks
{
    [SerializeField] private AudioClip pinpon;
    private AudioSource audioSource;
    private float buttonDownY = 0.03f;
    private Material red;
    private Material redBeam;
    private bool onMouse = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        red = Resources.Load("Red", typeof(Material)) as Material;
        redBeam = Resources.Load("Red Beam", typeof(Material)) as Material;
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
        photonView.RPC("Challenge", RpcTarget.MasterClient);
    }

    [PunRPC]
    void Challenge()
    {
        if (FindObjectOfType<Controller>().HayaoshiChallenge(this))
            photonView.RPC("PinPon", RpcTarget.All);
    }

    [PunRPC]
    void PinPon()
    {
        audioSource.PlayOneShot(pinpon);
        GetComponent<MeshRenderer>().materials = new Material[] { redBeam };
    }

    public void Seikai()
    {
        transform.parent.GetChild(1).GetChild(0).GetChild(1).GetComponent<PointUpDownButton>().Up();
        Release();
    }

    public void Huseikai()
    {
        transform.parent.GetChild(1).GetChild(1).GetChild(1).GetComponent<PointUpDownButton>().Up();
        Release();
    }

    private void Release()
    {
        GetComponent<MeshRenderer>().materials = new Material[] { red };
    }

    void IPointerCallbacks.OnPointerMoveOnIt(RaycastHit hit)
    {
    }

    void IPointerCallbacks.OnPointerUpOnIt(RaycastHit hit)
    {
    }
}
