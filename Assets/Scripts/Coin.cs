using UnityEngine;
using Photon.Pun;

public class Coin : MonoBehaviourPun
{
    [PunRPC]
    void DestroyCoin()
    {
        // Уничтожить монетку на локальном клиенте
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Вызвать удаленную процедуру для уничтожения монетки на всех клиентах
            photonView.RPC("DestroyCoin", RpcTarget.All);
        }
    }
}
