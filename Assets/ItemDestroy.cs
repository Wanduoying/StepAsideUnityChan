using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour {

    private GameObject Player;

    //アイテムを消去する座標の基準
    private float itemDestroyRange = 10.0f;

    // Use this for initialization
    void Start () {
        this.Player = GameObject.Find("unitychan");
    }
	
	// Update is called once per frame
	void Update () {
        //通り過ぎたオブジェクトの消去
        if (this.transform.position.z - Player.transform.position.z < -itemDestroyRange)
        {
            Debug.Log("object destroy");
            Destroy(this.gameObject);
        }
    }
}
