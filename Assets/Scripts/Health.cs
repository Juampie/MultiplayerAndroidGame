using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviourPunCallbacks
{
    private Slider _slider;
    private PhotonView _view;
    private Text gameOverText;


    void Start()
    {
        _slider = GameObject.Find("HP bar").GetComponent<Slider>();
        gameOverText = GameObject.Find("gameOverText").GetComponent<Text>();
        _view = GetComponent<PhotonView>();
        Hashtable props = new Hashtable();
        props.Add("Health", _slider.value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(Random.Range(0.1f, 0.2f));
            photonView.RPC("TakeDamage", RpcTarget.AllBuffered, Random.Range(0.1f, 0.2f));
            var bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.DestroyBullet();
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        if (photonView.IsMine)
        {
            _slider.value -= damage;

            Hashtable props = new Hashtable();
            props.Add("Health", _slider.value);

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

            if (_slider.value <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        photonView.RPC("TimeStop", RpcTarget.All);

        if (PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {
            Player winner = PhotonNetwork.PlayerList.OrderByDescending(p => (float)p.CustomProperties["Health"]).FirstOrDefault();

            if (winner != null)
            {
                string winnerName = winner.NickName;
                int coinCount = (int)winner.CustomProperties["CoinCount"];
                photonView.RPC("ShowGameOverText", RpcTarget.All, winnerName, coinCount);
            }
            
        }
    }

    [PunRPC]
    void ShowGameOverText(string winnerName, int coins)
    {
        gameOverText.text = $"Game Over! Player {winnerName} won with {coins} coins!";
    }

    [PunRPC]
    void TimeStop()
    {
        Time.timeScale = 0;
    }
}