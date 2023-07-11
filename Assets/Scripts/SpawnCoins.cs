using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 4f;
    [SerializeField] private float _minX, _maxX, _minY, _maxY;


    private void Start()
    {
        
        StartCoroutine(SpawnCoinCoroutine());
    }

    private System.Collections.IEnumerator SpawnCoinCoroutine()
    {
        while (true)
        {
            SpawnCoin();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCoin()
    {
        
        float randomX = Random.Range(_minX, _maxX);
        float randomY = Random.Range(_minY, _maxY);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);


        PhotonNetwork.Instantiate(coinPrefab.name, spawnPosition, Quaternion.identity);
    }
}

