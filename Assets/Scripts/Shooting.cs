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

    private void Awake()
    {
        _joystick = GameObject.Find("Fixed Joystick Attack").GetComponent<FixedJoystick>();
        _view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (_view.IsMine)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {

        while (true)
        {
            Vector3 joystickDirection = new Vector3(_joystick.Horizontal, _joystick.Vertical, 0);
            if (joystickDirection.magnitude > 0.7f)
            {
                Vector3 bulletPosition = transform.position + joystickDirection.normalized;
                SpawnBullet(bulletPosition);
            }

            yield return new WaitForSeconds(_shotSpeed);
        }
    }

    private void SpawnBullet(Vector3 position)
    {
        GameObject bullet = PhotonNetwork.Instantiate(_bullet.name, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Ignore collision between the bullet and its creator
        Collider2D creatorCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), creatorCollider);

        rb.velocity = (position - transform.position).normalized * _bulletSpeed;
    }
}