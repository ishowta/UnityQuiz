using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class QuizButton : MonoBehaviourPunCallbacks, IPointerCallbacks
{
    private Vector3 buttonDownDif;

    private bool onMouse = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonDownDif = new Vector3(0, GetComponent<MeshRenderer>().bounds.size.z / 2, 0);
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
            OnUp();
        }
    }

    public void OnPointerMove()
    {

    }

    public void OnPointerDownOnIt(RaycastHit hit)
    {
        onMouse = true;
        transform.Translate(-buttonDownDif);
        OnClick(hit);
    }

    public void OnPointerMoveOnIt(RaycastHit hit)
    {
        OnMove(hit);
    }

    public void OnPointerUpOnIt(RaycastHit hit)
    {
    }

    public virtual void OnClick(RaycastHit hit) { }
    public virtual void OnMove(RaycastHit hit) { }
    public virtual void OnUp() { }
}
