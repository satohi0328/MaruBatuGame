using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    // 剣(○)Prefab
    public GameObject bastardSwordPrefab;
    // 盾(×)Prefab
    public GameObject knightlyShieldPrefab;
    // BlockPrefab
    public GameObject blockPrefab;

    //BlockMaterial
    public Material blockMaterial;

    //GameSetText
    public GameObject gameSetText;

    // まるばつの設置回数を保持
    private int setCounter = 0;

    // 前回タップされたマス番号を保持
    private int beforTapTileNumber = 0;

    // ゲームセットの状態を保持
    private bool gameSetFlg = false;

    public BlockManager[,] boardStatus = new BlockManager[3, 3];
    // Use this for initialization
    void Start() {

        // 盤面3×3の各マスにBlockManagerを生成
        for (int i = 0; i < boardStatus.GetLength(0); i++) {
            for (int j = 0; j < boardStatus.GetLength(1); j++) {
                // 盤面配列に格納するオブジェクト生成
                GameObject wkObj = Instantiate(blockPrefab) as GameObject;
                // 座標設定
                wkObj.transform.position = new Vector3(j * 20 - 20, 2, i * 20 - 20);

                // BlockManagerを取得
                BlockManager gameManager = wkObj.GetComponent<BlockManager>();
                boardStatus[i, j] = gameManager;
                // マスごとに初期化
                boardStatus[i, j].Init(i, j);

            }
        }

        // BoardColorを保持
        blockMaterial = Resources.Load("Materials/BoardColor") as Material;

        // GameSetTextを取得
        this.gameSetText = GameObject.Find("GameSetText");
    }

    // Update is called once per frame
    void Update() {
        // ゲームセットフラグが立ってた場合は、処理させない
        if (gameSetFlg == true) {
            return;
        }

        // 左クリックされた場合
        if (Input.GetMouseButtonDown(0)) {
            touchOnBoardAction();
        }
    }

    /**
     * タップ時の挙動
     */
    private void touchOnBoardAction() {
        //カメラからの光線を設定
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // 光線がオブジェクトに衝突した場合
        if (Physics.Raycast(ray, out hit, 100.0f) && hit.collider.gameObject.name == "BlockPrefab(Clone)") {
            // BlockManagerオブジェクトを取得
            BlockManager gm = hit.collider.GetComponent<BlockManager>();

            //オブジェクトが既に表示されている場合は処理しない。
            if (gm.JudgeShowObject() == false) {
                return;
            }

            // 初回はタップされた箇所を赤反転
            if (beforTapTileNumber != gm.GetBlockNumber()) {

                // 盤面の色を一旦初期化
                for (int i = 0; i < boardStatus.GetLength(0); i++) {
                    for (int j = 0; j < boardStatus.GetLength(1); j++) {
                        boardStatus[i, j].GetComponent<MeshRenderer>().material = blockMaterial;
                    }
                }
                gm.GetComponent<MeshRenderer>().material.color = Color.red;
                // タップされたマス番号を保持
                beforTapTileNumber = gm.GetBlockNumber();

                // 上記以外の場合(２回連続で同じマスがタップ)
            } else {
                // 色を戻す
                gm.GetComponent<MeshRenderer>().material = blockMaterial;
                // 偶数回目の場合は剣(○)
                if (setCounter % 2 == 0) {
                    gm.ShowObject(true);
                } else {
                    gm.ShowObject(false);
                }
                setCounter++;
                //盤面の状態を判定
                if (JudgeBoardStatus()) {
                    //ゲーム終了処理
                    AfterGameSetAction();
                }
            }


        }
    }

    /**
    * 盤面上のまるばつを判定
    * 盤面の番号は以下
    * ------------
    *  7 | 8 | 9 |
    * ------------
    *  4 | 5 | 6 |
    * ------------
    *  1 | 2 | 3 |
    * ------------
    * ○:1 , ×:-1 ,null:0
    */
    private bool JudgeBoardStatus() {
        // 各マスのステータスを取得
        int board_1 = boardStatus[0, 0].GetStatus();
        int board_2 = boardStatus[0, 1].GetStatus();
        int board_3 = boardStatus[0, 2].GetStatus();
        int board_4 = boardStatus[1, 0].GetStatus();
        int board_5 = boardStatus[1, 1].GetStatus();
        int board_6 = boardStatus[1, 2].GetStatus();
        int board_7 = boardStatus[2, 0].GetStatus();
        int board_8 = boardStatus[2, 1].GetStatus();
        int board_9 = boardStatus[2, 2].GetStatus();

        // statusを加算したときに絶対値が3の場合、揃ったと判定
        // 1,2,3が揃ってる場合
        if (Mathf.Abs(board_1 + board_2 + board_3) == 3) {
            MatchTileProcess(1, 2, 3);
            return true;
        }
        // 4,5,6が揃ってる場合
        if (Mathf.Abs(board_4 + board_5 + board_6) == 3) {
            MatchTileProcess(4, 5, 6);
            return true;
        }
        // 7,8,9が揃ってる場合
        if (Mathf.Abs(board_7 + board_8 + board_9) == 3) {
            MatchTileProcess(7, 8, 9);
            return true;
        }
        // 1,4,7が揃ってる場合
        if (Mathf.Abs(board_1 + board_4 + board_7) == 3) {
            MatchTileProcess(1, 4, 7);
            return true;
        }
        // 2,5,8が揃ってる場合
        if (Mathf.Abs(board_2 + board_5 + board_8) == 3) {
            MatchTileProcess(2, 5, 8);
            return true;
        }
        // 3,6,9が揃ってる場合
        if (Mathf.Abs(board_3 + board_6 + board_9) == 3) {
            MatchTileProcess(3, 6, 9);
            return true;
        }
        // 1,5,9が揃ってる場合
        if (Mathf.Abs(board_1 + board_5 + board_9) == 3) {
            MatchTileProcess(1, 5, 9);
            return true;
        }
        // 3,5,7が揃ってる場合
        if (Mathf.Abs(board_3 + board_5 + board_7) == 3) {
            MatchTileProcess(3, 5, 7);
            return true;
        }
        // 全部埋まっている場合(引き分け)
        if (Mathf.Abs(board_1) + Mathf.Abs(board_2) + Mathf.Abs(board_3) +
            Mathf.Abs(board_4) + Mathf.Abs(board_5) + Mathf.Abs(board_6) +
            Mathf.Abs(board_7) + Mathf.Abs(board_8) + Mathf.Abs(board_9) == 9) {
            return true;
        }
        return false;
    }

    /**
     *揃ったタイルに対する処理
     */
    private void MatchTileProcess(int tile1, int tile2, int tile3) {

        BlockManager wkBoard1 = boardStatus[0, 0];
        BlockManager wkBoard2 = boardStatus[0, 0];
        BlockManager wkBoard3 = boardStatus[0, 0];

        // 一致したマスオブジェクトを取得
        for (int i = 0; i < boardStatus.GetLength(0); i++) {
            for (int j = 0; j < boardStatus.GetLength(1); j++) {
                if (boardStatus[i, j].GetBlockNumber() == tile1) {
                    wkBoard1 = boardStatus[i, j];
                } else if (boardStatus[i, j].GetBlockNumber() == tile2) {
                    wkBoard2 = boardStatus[i, j];
                } else if (boardStatus[i, j].GetBlockNumber() == tile3) {
                    wkBoard3 = boardStatus[i, j];
                }
            }
        }

        // マスの装飾処理を呼び出し
        decorationTile(wkBoard1);
        decorationTile(wkBoard2);
        decorationTile(wkBoard3);

    }

    /**
     * マスの装飾処理
     */
    private void decorationTile(BlockManager bm) {

        // マスを緑反転
        bm.GetComponent<MeshRenderer>().material.color = Color.green;

        // オブジェクトのBoundを保持
        Bounds b = bm.gameObject.GetComponent<Renderer>().bounds;

        // オブジェクトを正面空見た時の位置を保持
        Vector3 boundPoint1 = b.min;    //前左下
        Vector3 boundPoint2 = b.max;    //奥右上
        //y座標を気持ち上昇
        boundPoint1.y += 3f;
        boundPoint2.y += 3f;
        Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);//奥左下
        Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);//前左上
        Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);//前右下
        Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);//奥左上
        Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);//奥右下
        Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);//前右上

        // 角に配置するためのオブジェクト生成
        GameObject wkobj1 = null;
        GameObject wkobj2 = null;
        GameObject wkobj3 = null;
        GameObject wkobj4 = null;

        // スケールを保持
        Vector3 wkScale;

        // マスに表示されているオブジェクトを角隅に配置する
        // 剣が表示されている場合
        if (bm.GetObjSoard().activeSelf) {
            wkobj1 = Instantiate(bastardSwordPrefab) as GameObject;
            wkobj2 = Instantiate(bastardSwordPrefab) as GameObject;
            wkobj3 = Instantiate(bastardSwordPrefab) as GameObject;
            wkobj4 = Instantiate(bastardSwordPrefab) as GameObject;
            wkScale = new Vector3(2f, 2f, 2f);
        } else {
            wkobj1 = Instantiate(knightlyShieldPrefab) as GameObject;
            wkobj2 = Instantiate(knightlyShieldPrefab) as GameObject;
            wkobj3 = Instantiate(knightlyShieldPrefab) as GameObject;
            wkobj4 = Instantiate(knightlyShieldPrefab) as GameObject;
            wkScale = new Vector3(5f, 5f, 5f);
        }
        //角隅に配置
        wkobj1.transform.position = boundPoint2;
        wkobj2.transform.position = boundPoint4;
        wkobj3.transform.position = boundPoint6;
        wkobj4.transform.position = boundPoint8;

        // スケールを変更
        wkobj1.transform.localScale = wkScale;
        wkobj2.transform.localScale = wkScale;
        wkobj3.transform.localScale = wkScale;
        wkobj4.transform.localScale = wkScale;
    }

    /**
     *ゲームセット後のアクション
     */
    private void AfterGameSetAction() {
        // GameSetText表示
        this.gameSetText.GetComponent<Text>().enabled = true;

        // ゲームセットフラグを立てる
        gameSetFlg = true;

        Invoke("loadStartScene", 5f);
    }

    private void loadStartScene() {
        SceneManager.LoadScene("StartScene");
    }


}
