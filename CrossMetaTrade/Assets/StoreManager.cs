using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StoreManager : MonoBehaviour
{
    PhotonView view;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            view = collision.collider.GetComponent<PhotonView>();
            if (view.IsMine)
            {
                collision.collider.GetComponent<PlayerControl>().setSubscriptionCanvas(true, true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            view = collision.collider.GetComponent<PhotonView>();
            if (view.IsMine)
            {
                bool cursor_vis = collision.collider.GetComponent<PlayerControl>().isBackpackOpen;
                collision.collider.GetComponent<PlayerControl>().setSubscriptionCanvas(false, cursor_vis);
            }
        }
    }
}
