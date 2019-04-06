using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //【発展課題】
    //
    //Unityちやんから見て何ｍ先にアイテムを生成するかの基準距離
    private float itemGenerateRange = 50.0f;
    //アイテムが生成される間隔
    private int itemInterval = 15;
    //最後に生成されたアイテムの座標
    private int lastGeneratePos;
    //unityちゃん取得
    private GameObject Player;
    

    // Use this for initialization
    void Start()
    {
        this.Player = GameObject.Find("unitychan");
        lastGeneratePos = startPos;
    }



    // Update is called once per frame
    void Update () {
               
        if (lastGeneratePos - Player.transform.position.z < itemGenerateRange)
        {
            //Unityちゃんから一定の距離ごとにアイテムを生成　
            for (int i = lastGeneratePos; i < lastGeneratePos+ itemInterval; i += itemInterval)
            {
                //どのアイテムを出すのかをランダムに設定
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab) as GameObject;
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                    }
                }
                else
                {
                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン配置:30%車配置:10%何もなし
                        if (1 <= item && item <= 6)
                        {
                            //コインを生成
                            GameObject coin = Instantiate(coinPrefab) as GameObject;
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i /*+ offsetZ*/);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab) as GameObject;
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, i /*+ offsetZ*/);
                        }
                    }
                }
            }
            lastGeneratePos += itemInterval;
        }

        //ゴール附近およびゴールよりも奥の座標にアイテムが生成されるのを防ぐ処理
        if (Player.transform.position.z > goalPos - itemGenerateRange)
        {
            lastGeneratePos += 1000 ;
        }

    }
}