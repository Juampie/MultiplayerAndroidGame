using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _bullet;
    private FixedJoystick _joystick;
    private float _bulletSpeed = 10f;
    private float _shotSpeed = 0.7f;

    private PhotonView _view;
    private bool _isShooting = false;

    void Start()
    {
        _joystick = GameObject.Find("Fixed Joystick Attack").GetComponent<FixedJoystick>();
        _view = GetComponent<PhotonView>();

        // Enable sending and receiving position updates for the instantiated bullets
        PhotonNetwork.SendRate = 60; // Adjust this value as needed
        PhotonNetwork.SerializationRate = 30; // Adjust this value as needed
    }

    void Update()
    {
        if (_view.IsMine && !_isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        _isShooting = true;

        yield return new WaitForSeconds(_shotSpeed);

        while (Mathf.Abs(_joystick.Horizontal) > 0.7f || Mathf.Abs(_joystick.Vertical) > 0.7f)
        {
            Vector3 bulletPosition = transform.position + new Vector3(_joystick.Horizontal, _joystick.Vertical, 0);
            photonView.RPC("SpawnBullet", RpcTarget.AllViaServer, bulletPosition);
            yield return new WaitForSeconds(_shotSpeed);
        }

        _isShooting = false;
    }

    [PunRPC]
    private void SpawnBullet(Vector3 position)
    {
        GameObject bullet = Instantiate(_bullet, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Ignore collision between the bullet and its creator
        Collider2D creatorCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), creatorCollider);

        rb.velocity = (position - transform.position).normalized * _bulletSpeed;
    }
}
