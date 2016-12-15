using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour {

    public GameObject target_C;
    public GameObject target_P;
    public float RotationSensitivity = 200f;// 感度

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        target_C = GameObject.Find("CameraParent");
        target_P = GameObject.Find("azalea");
        var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity;
        //キャラの回転
        transform.Rotate(Vector3.up, rotX * 1.5f, 0);
        //transform.rotation = Quaternion.Euler(0, target_C.transform.localEulerAngles.y, 0);
        //transform.rotation = Quaternion.Euler(0, target_C.transform.localEulerAngles.y - target_P.transform.localEulerAngles.y, 0);
        //this.transform.Rotate(0, target.transform.localEulerAngles.y, 0);

    }
}
