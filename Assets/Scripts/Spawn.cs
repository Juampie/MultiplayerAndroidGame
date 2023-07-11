using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _minX, _maxX, _minY, _maxY;
    

    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(_minX, _maxX),Random.Range(_minY, _maxY));
        PhotonNetwork.Instantiate(_player.name, randomPosition, Quaternion.identity);
    }
}
