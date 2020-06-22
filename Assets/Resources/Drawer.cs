using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class Drawer : MonoBehaviourPunCallbacks, IPointerCallbacks
{
    [SerializeField]
    private int m_width = 4;

    [SerializeField]
    private int m_height = 4;

    private Vector2 m_prevMousePos;

    private Texture2D m_texture = null;

    private bool painting = false;

    private void Start()
    {
        var paper = GetComponent<MeshRenderer>();
        var viewer = transform.GetChild(1).GetComponent<MeshRenderer>();
        m_texture = new Texture2D(1000, 1000, TextureFormat.RGBA32, false);
        Color white = Color.white;
        m_texture.SetPixels(Enumerable.Repeat(white, 1000 * 1000).ToArray());
        m_texture.Apply();
        paper.material.SetTexture("_MainTex", m_texture);
        viewer.material.SetTexture("_MainTex", m_texture);
    }

    private void OnDestroy()
    {
        if (m_texture != null)
        {
            Destroy(m_texture);
            m_texture = null;
        }
    }

    public void OnPointerDown()
    {
    }

    public void OnPointerUp()
    {
        painting = false;
    }

    public void OnPointerMove()
    {

    }

    void IPointerCallbacks.OnPointerDownOnIt(RaycastHit hit)
    {
    }

    void IPointerCallbacks.OnPointerMoveOnIt(RaycastHit hit)
    {
        var hitPos = hit.textureCoord * new Vector2(m_texture.width, m_texture.height);

        if (painting == false)
        {
            m_prevMousePos = hitPos;
            m_prevSendMousePos = hitPos;
            painting = true;
            return;
        }
        photonView.RPC("Paint", RpcTarget.All, m_prevMousePos, hitPos);
        m_prevMousePos = hitPos;
    }

    void IPointerCallbacks.OnPointerUpOnIt(RaycastHit hit)
    {
    }

    private void Update()
    {
    }

    [PunRPC]
    private void Paint(Vector2 prevPos, Vector2 hitPos)
    {
        int width = m_width;
        int height = m_height;

        var dir = prevPos - hitPos;
        var dist = (int)dir.magnitude;

        dir = dir.normalized;
        for (int d = 0; d < dist; ++d)
        {
            var pos = hitPos + dir * d;
            pos.x -= width / 2.0f;
            pos.y -= height / 2.0f;
            for (int h = 0; h < height; ++h)
            {
                int y = (int)(pos.y + h);
                if (y < 0 || y > m_texture.height)
                {
                    continue;
                }

                for (int w = 0; w < width; ++w)
                {
                    int x = (int)(pos.x + w);
                    if (x >= 0 && x <= m_texture.width)
                    {
                        m_texture.SetPixel(x, y, Color.black);
                    }
                }
            }
        }
        m_texture.Apply();
    }

    private Vector2 m_prevSendMousePos;
}
