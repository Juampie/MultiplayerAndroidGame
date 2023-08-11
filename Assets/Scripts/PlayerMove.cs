using Photon.Pun;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{
    [SerializeField] private float _speed = 5f;
    private FixedJoystick _joystick;
    private Rigidbody2D _rb;
    private PhotonView _view;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _joystick = GameObject.Find("Fixed Joystick Move").GetComponent<FixedJoystick>();
        _view = GetComponent<PhotonView>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_view.IsMine)
        {
            Move();
            FlipSprites();
        }
    }

    private void Move()

    {
        Vector2 direction = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        _rb.velocity = direction.normalized * _speed;

        _animator.SetFloat("Horizontal", Mathf.Abs(_joystick.Horizontal));
        _animator.SetFloat("Vertical", Mathf.Abs(_joystick.Vertical));
    }

    private void FlipSprites()
    {
        if (_joystick.Horizontal < -0.1f && !_spriteRenderer.flipX)
        {
            photonView.RPC("FlipSpritesRPC", RpcTarget.All, true);
        }
        else if (_joystick.Horizontal > 0.1f && _spriteRenderer.flipX)
        {
            photonView.RPC("FlipSpritesRPC", RpcTarget.All, false);
        }
    }

    [PunRPC]
    private void FlipSpritesRPC(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }
}