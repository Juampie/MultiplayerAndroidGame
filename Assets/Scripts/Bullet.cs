using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{

    private float destroyTime = 5f;


    void Start()
    {
        
        Invoke("DestroyBullet", destroyTime);
    }

    public void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
        
    }


}
