﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{
    public GameObject Join;
    public GameObject newJoin;
    public Camera MenuCamera;
    private GameObject CanvasMenu;
    private GameObject CanvasBattle;
    private int count;

    // Use this for initialization
    void Start()
    {
        //初期設定
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        PhotonNetwork.ConnectUsingSettings("v1.0");
        CanvasMenu = GameObject.Find("CanvasMenu");
        CanvasBattle = GameObject.Find("CanvasBattle");
        CanvasBattle.GetComponent<Canvas>().enabled = false;

        count = 1;
        
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
            //ここにルームがある場合、ルームの削除処理を入れないといけない。
            Destroy(newJoin);
        }
        else {

            //ルームが1件以上ある時ループでRoomInfo情報をログ出力
            for (int i = 0; i < rooms.Length; i++)
            {
                
                GameObject.Find("StatusText").GetComponent<Text>().text = "";
                Debug.Log("RoomName:" + rooms[i].name);
                Debug.Log("userName:" + rooms[i].customProperties["userName"]);
                Debug.Log("userId:" + rooms[i].customProperties["userId"]);
                
                //2つ以上ルームが出来た際にエラーが出るので↓のソースの改変が必要かと思われる。
                newJoin = (GameObject)Instantiate(Join, new Vector3(0, gameObject.transform.position.y - (120 * i) - 240, 0), Quaternion.identity);
                newJoin.transform.SetParent(CanvasMenu.transform, false);
                newJoin.name = rooms[i].name;
                GameObject.Find(rooms[i].name +"/Text").GetComponent<Text>().text = "Room" + count;
            }
        }
    }

    public void CreateRoom()
    {

        string userName = "ユーザ" + count;
        string userId = "user" + count;
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

        count++;

        //userIdが名前のルームがなければ作って入室、あれば普通に入室する。 ←よく意味がわからない
        PhotonNetwork.JoinOrCreateRoom(userId, roomOptions, null);

        
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(transform.name);

    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom()
    {
        Debug.Log("PhotonManager OnJoinedRoom");
        
        CanvasMenu.GetComponent<Canvas>().enabled = false;
        CanvasBattle.GetComponent<Canvas>().enabled = true;

        GameObject myPlayer = PhotonNetwork.Instantiate("azalea", new Vector3(0, 0, 0), Quaternion.identity, 0);
        myPlayer.name = "azalea";

        //MenuカメラをOFFにする
        MenuCamera.enabled = false;
    }
}