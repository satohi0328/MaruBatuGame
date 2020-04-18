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
    public GameObject bastardSwordPrefab;
    // 盾(×)Prefab
    public GameObject knightlyShieldPrefab;

    private int posX;       //x座標
    private int posY;       //y座標
    private int blockNumber;  //マス番号
    private int status;  //○:1、×:-1、カラ:0 の状態を保持
    private int tapCount;
    private GameObject objSoard; //ゲームオブジェクト maru
    private GameObject objShild; //ゲームオブジェクト batu

    /**
     * 初期化処理
     */
    public void Init(int x, int y) {
        this.posX = x;
        this.posY = y;
        this.status = 0;

        this.blockNumber = x * 3 + y + 1;
        Debug.Log(this.blockNumber);

        //オブジェクト生成し、非表示
        // 剣(○)オブジェクト生成
        this.objSoard = Instantiate(bastardSwordPrefab) as GameObject;
        //位置を移動
        Vector3 soardPos = this.transform.position;
        soardPos.y = 7f;
        this.objSoard.transform.position = soardPos;
        this.objSoard.SetActive(false);

        // 盾(×)オブジェクト生成
        this.objShild = Instantiate(knightlyShieldPrefab) as GameObject;
        //位置を移動
        Vector3 shildPos = this.transform.position;
        shildPos.y = 9f;
        this.objShild.transform.position = shildPos;
        this.objShild.SetActive(false);
    }

    /**
    * マス上にオブジェクトを表示できるか判定
    */
    public bool JudgeShowObject() {

        // 一度も表示されていない場合
        if (this.status == 0) {
            return true;
        }
        return false;
    }

    /**
    * マス上にオブジェクトを表示
    * 引数:true(○),false(×)
    */
    public void ShowObject(bool arg) {
        if (arg) {
            this.objSoard.SetActive(true);
            this.status = 1;
        } else {
            this.objShild.SetActive(true);
            this.status = -1;
        }
    }

    // メンバStatusのゲッター
    public int GetStatus() {
        return this.status;
    }
    // メンバblockNumberのゲッター
    public int GetBlockNumber() {
        return this.blockNumber;
    }
    // メンバobjSoardのゲッター
    public GameObject GetObjSoard() {
        return this.objSoard;
    }
    // メンバobjShildのゲッター
    public GameObject GetObjShild() {
        return this.objShild;
    }

}