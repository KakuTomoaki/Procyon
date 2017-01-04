﻿using UnityEngine;
using System.Collections;

public class Weapon2 : MonoBehaviour {

    GameObject explosion;

    // Use this for initialization
    void Start() {
        //出現後一定時間で自動的に消滅させる
        Destroy(gameObject, 3.0f);

    }

    // Update is called once per frame
    void Update() {
        explosion = (GameObject)Resources.Load("Prefabs/Explosion04b");
        //弾を前進させる
        transform.position += transform.forward * Time.deltaTime * 500;

    }

    private void OnCollisionEnter(Collision collider) {
        if (collider.gameObject.name == "Terrain" || collider.gameObject.tag == "Enemy") {
            //地形とぶつかったら消滅させる
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
