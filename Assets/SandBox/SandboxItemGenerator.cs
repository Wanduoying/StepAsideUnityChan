using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxItemGenerator : MonoBehaviour {

    //コインのみの生成テスト

    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 0;
    //ゴール地点
    private int goalPos = 20;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    // Use this for initialization
    void Start () {
		//一定の距離ごとにアイテム生成
        for(int i = startPos; i < goalPos; i += 1)
        {

            int num = Random.Range(1, 11);
            if (num <=1) { 
                //コインをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject coin = Instantiate(conePrefab) as GameObject;
                    coin.transform.position = new Vector3(4 * j, coin.transform.position.y, i); //new=生成
                }
            }
            else
            {
                //レーン毎アイテム生成
                for(int j = -1; j <= 1; j++)
                {
                    //アイテム種類決定
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットランダム
                    //60%コイン　30%車 10%なし
                    if (1 <= item && item <= 6)
                    {
                        //コイン生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i);
                    }else if (7 <= item && item <= 9)
                    {
                        //車生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
