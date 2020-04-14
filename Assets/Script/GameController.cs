using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // 剣(○)Prefab
    public GameObject BastardSwordPrefab;
    // 盾(○)Prefab
    public GameObject KnightlyShieldPrefab;

    public GameManager[,] boardStatus = new GameManager[2, 2];
    // Use this for initialization
    void Start() {

        // 盤面3×3の各マスにGameManagerを生成
        for (int i = 0; i < boardStatus.GetLength(0); i++) {
            for (int j = 0; j < boardStatus.GetLength(1); j++) {

                boardStatus[i, j].init(i, j);


                GameObject obj = Instantiate(BastardSwordPrefab) as GameObject;

            }

        }
    }

    // Update is called once per frame
    void Update() {

    }

}


public class GameManager : MonoBehaviour {
    private int posX;       //x座標
    private int posY;       //y座標
    private string status;  //○、×、カラの状態を保持
    private GameObject obj; //ゲームオブジェクト

    /**
     * 初期化処理
     */
    public void init(int x, int y) {
        this.posX = x;
        this.posY = y;
        this.status = "";
        this.obj = null;
    }

    /**
     * 盤面上にオブジェクトを生成
     */
    public void generateObjectOnBoard(GameObject argObj) {
        this.obj = argObj;
        this.obj.transform.position = new Vector3(this.posX * 20 - 20, 10, this.posY * 20 - 20);


    }


}
