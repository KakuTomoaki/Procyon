using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
    
    public Transform target;
    public float distance = 12f;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;
    //追加
    public float zMin = 5; //マウスホイールで近づける最小距離
    public float zMax = 15; //マウスホイールで離れられる最大距離

    private float x = 0.0f;
    private float y = 0.0f;

    Vector3 angles;

    // Use this for initialization
    void Start() {
        angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update() {
        if (target) {

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;


            //以下追加
            //ズームの距離に制限を設ける
            distance -= Input.GetAxis("Mouse ScrollWheel");
            distance = Mathf.Clamp(distance, zMin, zMax);

            /*
            //カメラの後方にレイを飛ばし近くのオブジェクトに当たったら
            //当たった位置にカメラを移動する
            RaycastHit hit;
            if (Physics.Raycast(target.transform.position, -transform.forward, out hit, distance)) {
                transform.position = hit.point;
            }
            */

        }
    }

    float ClampAngle(float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
