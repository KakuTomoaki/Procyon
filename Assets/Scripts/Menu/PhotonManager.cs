﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{
    public GameObject Prefab;
    public GameObject canvasObject;
    private GameObject CanvasMenu;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
        CanvasMenu = GameObject.Find("CanvasMenu");

        //初期設定
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    void OnJoinedLobby()
    {
        Debug.Log("PhotonManager OnJoinedLobby");
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(room.name);  //部屋名
            Debug.Log(room.playerCount);    //部屋の入場人数
            Debug.Log(room.maxPlayers);  //最大人数
        }
    }
    // Update is called once per frame


    void OnReceivedRoomListUpdate()
    {
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
        {
            GameObject.Find("StatusText").GetComponent<Text>().text = "Roomが1つもありません";
        }
        else {

            //ルームが1件以上ある時ループでRoomInfo情報をログ出力
            for (int i = 0; i < rooms.Length; i++)
            {
                GameObject.Find("StatusText").GetComponent<Text>().text = "";
                Debug.Log("RoomName:" + rooms[i].name);
                Debug.Log("userName:" + rooms[i].customProperties["userName"]);
                Debug.Log("userId:" + rooms[i].customProperties["userId"]);
                GameObject newJoin = (GameObject)Instantiate(Prefab, new Vector3(0, gameObject.transform.position.y - (120 * i) - 240, 0), Quaternion.identity);
                newJoin.transform.SetParent(canvasObject.transform, false);
                newJoin.name = rooms[i].name;
                //newJoin.GetComponent<Text>().text = "rooms [i].name";
                
            }
        }
    }

    public void CreateRoom()
    {
        string userName = "ユーザ1";
        string userId = "user1";
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        //カスタムプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName); //ユーザ名
        customProp.Add("userId", userId); //ユーザID
        PhotonNetwork.SetPlayerCustomProperties(customProp);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.customRoomProperties = customProp;
        //ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
        roomOptions.customRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.maxPlayers = 2; //部屋の最大人数
        roomOptions.isOpen = true; //入室許可する
        roomOptions.isVisible = true; //ロビーから見えるようにする
        //userIdが名前のルームがなければ作って入室、あれば普通に入室する。
        PhotonNetwork.JoinOrCreateRoom(userId, roomOptions, null);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("user1");

    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom()
    {
        Debug.Log("PhotonManager OnJoinedRoom");
        //Application.LoadLevel("Test");
        CanvasMenu.GetComponent<Canvas>().enabled = false;

        GameObject myPlayer = PhotonNetwork.Instantiate("azalea", new Vector3(0, 0, 0), Quaternion.identity, 0);
        //  自分が生成したPlayerを移動可能にする
        myPlayer.GetComponent<PlayerMove>().enabled = true;
        //myPlayer.GetComponent<Camera>().enabled = true;
    }
}