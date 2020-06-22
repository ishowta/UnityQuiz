using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SeikaiButton : QuizButton
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnClick(RaycastHit hit)
    {
        if (gameObject.name == "Seikai")
        {
            FindObjectOfType<Controller>().Seikai();
        }
        else
        {
            FindObjectOfType<Controller>().Huseikai();
        }
    }
}
