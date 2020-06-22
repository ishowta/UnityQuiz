using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(AudioSource))]
public class AnswerButton : MonoBehaviourPunCallbacks, IPointerCallbacks
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
        audioSource.PlayOneShot(pinpon);
        transform.Translate(0, -buttonDownY, 0);
        GetComponent<MeshRenderer>().materials = new Material[] { redBeam };
    }

    void IPointerCallbacks.OnPointerMoveOnIt(RaycastHit hit)
    {
    }

    void IPointerCallbacks.OnPointerUpOnIt(RaycastHit hit)
    {
    }
}
