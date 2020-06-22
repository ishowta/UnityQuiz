using UnityEngine;

public interface IPointerCallbacks
{
    void OnPointerDown();
    void OnPointerMove();
    void OnPointerUp();
    void OnPointerDownOnIt(RaycastHit hit);
    void OnPointerMoveOnIt(RaycastHit hit);
    void OnPointerUpOnIt(RaycastHit hit);
}
