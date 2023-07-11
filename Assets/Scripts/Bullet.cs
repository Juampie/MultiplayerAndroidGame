using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    void Start()
    {
        Invoke("DestroyBullet", 5f);
        
    }

    void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    



}
