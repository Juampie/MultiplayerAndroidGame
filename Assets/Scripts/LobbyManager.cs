using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _createInputField;
    [SerializeField] private InputField _joinInputField;
    [SerializeField] private InputField _name;
    [SerializeField] private Text _waitingText;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(_createInputField.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_joinInputField.text);
    }

    public override void OnJoinedRoom()
    {

        PhotonNetwork.NickName = _name.text == "" ? "Player" :
            _name.text;
        UpdateWaitingText();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        UpdateWaitingText();

       
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    void UpdateWaitingText()
    {
        
        _waitingText.text = PhotonNetwork.CurrentRoom.PlayerCount == 1
            ? "Ожидание второго игрока..."
            : "Второй игрок присоединился! Загрузка игры...";
    }

}
