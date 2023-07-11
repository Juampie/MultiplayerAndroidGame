using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviourPun  
{
    private TextMesh _nameText;

    void Start()
    {
        _nameText = GetComponent<TextMesh>();
        _nameText.text = photonView.Owner.NickName;
    }
}
