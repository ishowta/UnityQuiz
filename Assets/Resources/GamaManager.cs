using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Characters.FirstPerson;

public class GamaManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity, 0);
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<Camera>().enabled = true;
        //Instantiate(target, new Vector3(0, 1, 0), Quaternion.identity);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCallPointerCallbacks();
    }

    private void CheckCallPointerCallbacks()
    {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        Action<Action<IPointerCallbacks>> CallAllComponents = (Action<IPointerCallbacks> fn) =>
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.activeInHierarchy)
                {
                    var comp = obj.GetComponent<IPointerCallbacks>();
                    if (comp != null)
                    {
                        fn(comp);
                    }
                }
            }
        };

        Action<Action<IPointerCallbacks, RaycastHit>> CallPointedComponents = (Action<IPointerCallbacks, RaycastHit> fn) =>
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var comp = hit.collider.gameObject.GetComponent<IPointerCallbacks>();
                if (comp != null)
                {
                    fn(comp, hit);
                }
            }
        };

        if (Input.GetMouseButtonDown(0))
        {
            // PointerDown
            CallAllComponents(c => c.OnPointerDown());
            CallPointedComponents((c, hit) => c.OnPointerDownOnIt(hit));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // PointerUp
            CallAllComponents(c => c.OnPointerUp());
            CallPointedComponents((c, hit) => c.OnPointerUpOnIt(hit));
        }
        else if (Input.GetMouseButton(0))
        {
            // PointerMove
            CallAllComponents(c => c.OnPointerMove());
            CallPointedComponents((c, hit) => c.OnPointerMoveOnIt(hit));
        }
    }
}
