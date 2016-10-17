using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour {

    public static int armorPoint;
    int armorPointMax = 5000;

    int damage = 100;

    public Text armorText;

    int displayArmorPoint;
    
	// Use this for initialization
	void Start () {
        armorPoint = armorPointMax;
        displayArmorPoint = armorPoint;
	
	}
	
	// Update is called once per frame
	void Update () {


        //現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
        if(displayArmorPoint != armorPoint) {
            displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorPoint, 0.1f);
        }

        //体力をUI Textに表示する
        armorText.text = string.Format("{0:0000}", displayArmorPoint, armorPointMax);

        //残り体力の割合により文字の色を変える
        float percentageArmorPoint = (float)displayArmorPoint / armorPointMax;

        if(percentageArmorPoint > 0.5f) {
            armorText.color = Color.white;
        } else if(percentageArmorPoint > 0.3f) {
            armorText.color = Color.yellow;
        } else {
            armorText.color = Color.red;
        }	
	}

    private void OnCollisionEnter(Collision collider) {

        //敵の攻撃が接触時ダメージ
        if(collider.gameObject.tag == "testEnemy") {
            armorPoint -= damage;
            armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax);
        }
    }

}
