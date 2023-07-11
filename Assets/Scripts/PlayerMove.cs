using Photon.Pun;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{

    [SerializeField] private float _speed = 5f;


    private FixedJoystick _Joystick;
    private Rigidbody2D _rb;
    private PhotonView _view;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _Joystick = GameObject.Find("Fixed Joystick Move").GetComponent<FixedJoystick>();
        _view = GetComponent<PhotonView>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if (_view.IsMine)
        {


            _rb.velocity = new Vector2(_Joystick.Horizontal * _speed, _Joystick.Vertical * _speed);
            _animator.SetFloat("Horizontal", Mathf.Abs(_Joystick.Horizontal));
            _animator.SetFloat("Vertical", Mathf.Abs(_Joystick.Vertical));


            if (_Joystick.Horizontal < 0.1 && _spriteRenderer.flipX == false)
            {
                photonView.RPC("FlipSprites", RpcTarget.All, true);
            }
            else if (_Joystick.Horizontal > 0.1 && _spriteRenderer.flipX == true)
            {
                photonView.RPC("FlipSprites", RpcTarget.All, false);
            }
        }
    }

    [PunRPC]
    void FlipSprites(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }
}
