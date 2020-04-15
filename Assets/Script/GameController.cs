using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // 剣(○)Prefab
    public GameObject BastardSwordPrefab;
    // 盾(×)Prefab
    public GameObject KnightlyShieldPrefab;
    // BlockPrefab
    public GameObject BlockPrefab;


    // まるばつの設置回数を保持
    private int setCounter = 0;


    public BlockManager[,] boardStatus = new BlockManager[3, 3];
    // Use this for initialization
    void Start() {

        // 盤面3×3の各マスにBlockManagerを生成
        for (int i = 0; i < boardStatus.GetLength(0); i++) {
            for (int j = 0; j < boardStatus.GetLength(1); j++) {
                // 盤面配列に格納するオブジェクト生成
                GameObject wkObj = Instantiate(BlockPrefab) as GameObject;
                // 座標設定
                wkObj.transform.position = new Vector3(j * 20 - 20, 2, i * 20 - 20);

                // BlockManagerを取得
                BlockManager gameManager = wkObj.GetComponent<BlockManager>();
                boardStatus[i, j] = gameManager;
                // マスごとに初期化
                boardStatus[i, j].init(i, j);
            }
        }



    }

    // Update is called once per frame
    void Update() {

        // 左クリックされた場合
        if (Input.GetMouseButtonDown(0)) {
            //カメラからの光線を設定
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // 光線がオブジェクトに衝突した場合
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                BlockManager gm = hit.collider.GetComponent<BlockManager>();

                //オブジェクトの表示判定
                if (gm.judgeShowObject()) {
                    // 偶数回目の場合は剣(○)
                    if (setCounter % 2 == 0) {
                        gm.showObject(true);
                    } else {
                        gm.showObject(false);
                    }
                    setCounter++;
                }
                Debug.Log(hit.collider.gameObject.transform.position);
            }
        }

    }

}
