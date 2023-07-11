
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.UIElements;

public class CollectingCoins : MonoBehaviourPunCallbacks
{
    private Text _coinsText;

    private void Start()
    {
        _coinsText = GameObject.Find("CoinsText").GetComponent<Text>();

        Hashtable props = new Hashtable();
        props.Add("CoinCount", int.Parse(_coinsText.text));
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin") && photonView.IsMine)
        {
            _coinsText.text = (int.Parse(_coinsText.text) + 1).ToString();

            Hashtable props = new Hashtable();
            props.Add("CoinCount", int.Parse(_coinsText.text));

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        }
       
    }

   
}
