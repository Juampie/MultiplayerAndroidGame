using UnityEngine;
using Photon.Pun;

public class Coin : MonoBehaviourPun
{
    [PunRPC]
    void DestroyCoin()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("DestroyCoin", RpcTarget.All);
        }
    }
}
