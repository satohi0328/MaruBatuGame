using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPos : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Bounds b = this.gameObject.GetComponent<Renderer>().bounds;

        // 各角のポジション取得
        Vector3 boundPoint1 = b.min;
        Vector3 boundPoint2 = b.max;
        Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
        Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
        Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
        Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

        // 各角のpositionログ取得
        Debug.Log(boundPoint1);
        Debug.Log(boundPoint2);
        Debug.Log(boundPoint3);
        Debug.Log(boundPoint4);
        Debug.Log(boundPoint5);
        Debug.Log(boundPoint6);
        Debug.Log(boundPoint7);
        Debug.Log(boundPoint8);

        // 隅に配置するための丸オブジェクト生成
        GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere2  = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere5 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere6 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere7 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject sphere8 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // 生成した丸オブジェクトを移動
        sphere1.transform.position = boundPoint1;
        sphere2.transform.position = boundPoint2;
        sphere3.transform.position = boundPoint3;
        sphere4.transform.position = boundPoint4;
        sphere5.transform.position = boundPoint5;
        sphere6.transform.position = boundPoint6;
        sphere7.transform.position = boundPoint7;
        sphere8.transform.position = boundPoint8;

        Vector3 wkScale = new Vector3(3f, 3f, 3f);
        // 丸オブジェクトのスケール変更
        sphere1.transform.localScale = wkScale;
        sphere2.transform.localScale = wkScale;
        sphere3.transform.localScale = wkScale;
        sphere4.transform.localScale = wkScale;
        sphere5.transform.localScale = wkScale;
        sphere6.transform.localScale = wkScale;
        sphere7.transform.localScale = wkScale;
        sphere8.transform.localScale = wkScale;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
