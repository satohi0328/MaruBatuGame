using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

    // Use this for initialization
    void Start() {
    }
    // Update is called once per frame
    void Update() {

    }
    // 剣(○)Prefab
    public GameObject BastardSwordPrefab;
    // 盾(×)Prefab
    public GameObject KnightlyShieldPrefab;


    private int posX;       //x座標
    private int posY;       //y座標
    private string status;  //○、×、カラの状態を保持
    private GameObject objSoard; //ゲームオブジェクト maru
    private GameObject objShild; //ゲームオブジェクト batu

    /**
     * 初期化処理
     */
    public void init(int x, int y) {
        this.posX = x;
        this.posY = y;
        this.status = null;

        //オブジェクト生成し、非表示
        // 剣(○)オブジェクト生成
        this.objSoard = Instantiate(BastardSwordPrefab) as GameObject;
        //位置を移動
        Vector3 soardPos = this.transform.position;
        soardPos.y = 7f;
        this.objSoard.transform.position = soardPos;
        this.objSoard.SetActive(false);

        // 盾(×)オブジェクト生成
        this.objShild = Instantiate(KnightlyShieldPrefab) as GameObject;
        //位置を移動
        Vector3 shildPos = this.transform.position;
        shildPos.y = 9f;
        this.objShild.transform.position = shildPos;
        this.objShild.SetActive(false);
    }


    /**
    * マス上にオブジェクトを表示できるか判定
    */
    public bool judgeShowObject() {

        // 一度も表示されていない場合
        if (this.status == null) {
            return true;
        }
        return false;
    }


    /**
    * マス上にオブジェクトを表示
    * 引数:true(○),false(×)
    */
    public void showObject(bool arg) {
        if (arg) {
            this.objSoard.SetActive(true);
            this.status = "maru";
        } else {
            this.objShild.SetActive(true);
            this.status = "batu";
        }
    }

}