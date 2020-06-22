using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMe : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Camera.main) transform.LookAt(Camera.main.transform);
    }
}
