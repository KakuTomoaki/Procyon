using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWeapon : Photon.MonoBehaviour {

    GameObject shot;
    GameObject shot2;
    GameObject shot3;
    GameObject muzzle;
    GameObject muzzleFlash;
    Image weapon1gauge;
    Text weapon1text;
    Image weapon2gauge;
    Text weapon2text;
    Image weapon3gauge;
    Text weapon3text;

    //使用武器の番号
    int useWeapon = 1;
    //リロードフラグ
    bool reloadFlag;

    //リロードのCD
    float reloadInterval = 0;
    float reloadIntervalMax = 2f;
    //弾の発射速度
    float shotInterval = 0;
    float shotIntervalMax = 1f;
    //武器変更時のCD
    float changeInterval = 0;
    float changeIntervalMax = 0.2f;
    //武器1の発射可能数+リロード時のCD
    int weapon1shotCount;
    int weapon1shotCountMax = 30;
    float weapon1reloadtime = 2;
    //武器2の発射可能数+リロード時のCD
    int weapon2shotCount;
    int weapon2shotCountMax = 5;
    float weapon2reloadtime = 3;
    //武器3の発射可能数+リロード時のCD
    int weapon3shotCount;
    int weapon3shotCountMax = 3;
    float weapon3reloadtime = 5;

    //音関連
    private AudioSource SE_weapon1;
    private AudioSource SE_weapon2;
    private AudioSource SE_weapon3;
    private AudioSource SE_reload;


    // Use this for initialization
    void Start() {
        shot = (GameObject)Resources.Load("Prefabs/shot");
        shot2 = (GameObject)Resources.Load("Prefabs/shot2");
        shot3 = (GameObject)Resources.Load("Prefabs/shot3");
        muzzle = GameObject.Find("azalea/wall01_azalea:waist/wall01_azalea:belly/wall01_azalea:body/wall01_azalea:Rshoulder/wall01_azalea:Ruparm/25cn:base/muzzle");
        muzzleFlash = (GameObject)Resources.Load("Prefabs/MuzzleFlash");
        weapon1gauge = GameObject.Find("CanvasBattle/Weapon1/weapon1gauge").GetComponent<Image>();
        weapon1text = GameObject.Find("CanvasBattle/Weapon1/weapon1text").GetComponent<Text>();
        weapon2gauge = GameObject.Find("CanvasBattle/Weapon2/weapon2gauge").GetComponent<Image>();
        weapon2text = GameObject.Find("CanvasBattle/Weapon2/weapon2text").GetComponent<Text>();
        weapon3gauge = GameObject.Find("CanvasBattle/Weapon3/weapon3gauge").GetComponent<Image>();
        weapon3text = GameObject.Find("CanvasBattle/Weapon3/weapon3text").GetComponent<Text>();

        weapon1shotCount = weapon1shotCountMax;
        weapon2shotCount = weapon2shotCountMax;
        weapon3shotCount = weapon3shotCountMax;
        
        //SEをキャッシュ
        AudioSource[] audioSources = GetComponents<AudioSource>();
        SE_weapon1 = audioSources[0];
        SE_weapon2 = audioSources[1];
        SE_weapon3 = audioSources[2];
        SE_reload = audioSources[3];

    }

    // Update is called once per frame
    void Update() {
        //発射/リロード/チェンジ間隔を設定する
        shotInterval += Time.deltaTime;
        reloadInterval += Time.deltaTime;
        changeInterval += Time.deltaTime;
        //マウスのスクロール判定
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");


        /*
        ------------------------武器リロード関連-----------------------------
        */

        if (Input.GetButton("Reload")) {
            if (reloadInterval > reloadIntervalMax) {
                reloadFlag = true;
                SE_reload.PlayOneShot(SE_reload.clip);
                if (useWeapon == 1) {
                    Debug.Log("wepon1リロード中 + ReloadフラグOn");
                    Invoke("Reload", weapon1reloadtime);
                } else if (useWeapon == 2) {
                    Debug.Log("wepon2リロード中 + ReloadフラグOn");
                    Invoke("Reload", weapon2reloadtime);
                } else if (useWeapon == 3) {
                    Debug.Log("wepon3リロード中 + ReloadフラグOn");
                    Invoke("Reload", weapon3reloadtime);
                }
                reloadInterval = 0;
            }
        }

        /*
        ------------------------武器リロード関連-----------------------------
        */



        /*
        ------------------------武器変更関連-----------------------------
        */

        if (mouseScroll > 0) {
            if (changeInterval > changeIntervalMax && reloadFlag == false) {
                Debug.Log("change_up");
                if (useWeapon == 1) {
                    useWeapon = 2;
                    Debug.Log("wepon2に変更");
                } else if (useWeapon == 2) {
                    useWeapon = 3;
                    Debug.Log("wepon3に変更");
                } else if (useWeapon == 3) {
                    useWeapon = 1;
                    Debug.Log("wepon1に変更");
                }
                changeInterval = 0;
            }
        }
        if (mouseScroll < 0) {
            if (changeInterval > changeIntervalMax && reloadFlag == false) {
                Debug.Log("change_down");
                if (useWeapon == 1) {
                    useWeapon = 3;
                    Debug.Log("wepon3に変更");
                } else if (useWeapon == 2) {
                    useWeapon = 1;
                    Debug.Log("wepon1に変更");
                } else if (useWeapon == 3) {
                    useWeapon = 2;
                    Debug.Log("wepon2に変更");
                }
                changeInterval = 0;
            }
        }


        /*
        ------------------------武器変更関連-----------------------------
        */



        /*
        ------------------------攻撃関連-----------------------------
        */

        if (useWeapon == 1) {
            //弾の発射速度変更
            shotIntervalMax = 0.2f;
            if (Input.GetButton("Fire1") && weapon1shotCount > 0 && reloadFlag == false) {
                //CDチェック
                if (shotInterval > shotIntervalMax) {
                    //弾を発射する
                    Debug.Log("useWeapon = 1");
                    SE_weapon1.PlayOneShot(SE_weapon1.clip);
                    Instantiate(shot, muzzle.transform.position, Camera.main.transform.rotation);
                    weapon1shotCount -= 1;
                    shotInterval = 0;

                    //マズルフラッシュを表示する
                    Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
                }
            }
        }

        if (useWeapon == 2) {
            shotIntervalMax = 1.5f;
            //弾の発射速度変更
            if (Input.GetButton("Fire1") && weapon2shotCount > 0 && reloadFlag == false) {
                //CDチェック
                if (shotInterval > shotIntervalMax) {
                    //弾を発射する
                    Debug.Log("useWeapon = 2");
                    SE_weapon2.PlayOneShot(SE_weapon2.clip);
                    Instantiate(shot2, muzzle.transform.position, Camera.main.transform.rotation);
                    weapon2shotCount -= 1;
                    shotInterval = 0;

                    //マズルフラッシュを表示する
                    Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
                }
            }
        }
        if (useWeapon == 3) {
            shotIntervalMax = 3f;
            //弾の発射速度変更
            if (Input.GetButton("Fire1") && weapon3shotCount > 0 && reloadFlag == false) {
                //CDチェック
                if (shotInterval > shotIntervalMax) {
                    //弾を発射する
                    Debug.Log("useWeapon = 3");
                    SE_weapon3.PlayOneShot(SE_weapon3.clip);
                    Instantiate(shot3, muzzle.transform.position, Camera.main.transform.rotation);
                    weapon3shotCount -= 1;
                    shotInterval = 0;

                    //マズルフラッシュを表示する
                    Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
                }
            }
        }

        /*
        ------------------------攻撃関連-----------------------------
        */



        /*
        ------------------------攻撃のUI関連-----------------------------
        */

        //Wepon1ゲージの伸縮
        weapon1gauge.transform.localScale = new Vector3((float)weapon1shotCount / weapon1shotCountMax, 1, 1);
        //Wepon1の残弾をTextに表示する
        weapon1text.text = string.Format("{0:00} / {1:00}", weapon1shotCount, weapon1shotCountMax);

        //Wepon2ゲージの伸縮
        weapon2gauge.transform.localScale = new Vector3((float)weapon2shotCount / weapon2shotCountMax, 1, 1);
        //Wepon2の残弾をTextに表示する
        weapon2text.text = string.Format("{0:0} / {1:0}", weapon2shotCount, weapon2shotCountMax);

        //Wepon3ゲージの伸縮
        weapon3gauge.transform.localScale = new Vector3((float)weapon3shotCount / weapon3shotCountMax, 1, 1);
        //Wepon3の残弾をTextに表示する
        weapon3text.text = string.Format("{0:0} / {1:0}", weapon3shotCount, weapon3shotCountMax);

        /*
        ------------------------攻撃のUI関連-----------------------------
        */

    }

    /*
    ---------------------リロード関連---------------------------
    */
    void Reload() {
        if (useWeapon == 1) {
            weapon1shotCount = weapon1shotCountMax;
        }
        if (useWeapon == 2) {
            weapon2shotCount = weapon2shotCountMax;
        }
        if (useWeapon == 3) {
            weapon3shotCount = weapon3shotCountMax;
        }
        Debug.Log("ReloadフラグOFF");
        reloadFlag = false;
    }
}
