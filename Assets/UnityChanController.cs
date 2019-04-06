using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;


    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前進するための力
    private float forwardForce = 800.0f;
    //左右に移動するための力
    private float turnForce = 500.0f;
    //ジャンプするための力
    private float upForce = 500.0f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;
    //動きを減速させる係数
    private float cosfficient = 0.95f;

    //ゲーム終了判定
    private bool isEnd = false;
    //ゲーム終了時に表示するテキスト
    private GameObject stateText;
    //スコア表示のテキスト
    private GameObject scoreText;
    //得点
    private int score = 0;

    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;
         


    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }



    void Update()
    {
        //ゲーム終了ならunityちやんの動きを減衰する
        if (this.isEnd)
        {
            this.forwardForce *= this.cosfficient;
            this.turnForce *= this.cosfficient;
            this.upForce *= this.cosfficient;
            this.myAnimator.speed *= this.cosfficient;
        }

        //Unityちゃんに前方向の力を加える
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左に移動
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.movableRange > this.transform.position.x)
        {
            //右に移動
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        //Jumpステートの場合はJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしていない時にスペースが押されたらジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
     }

    public void OnTriggerEnter(Collider other)
    {
        //障碍物に衝突した時
        if(other.gameObject.tag=="CarTag" ||other.gameObject.tag== "TrafficConeTag")
        {
            this.isEnd = true;
            //stateTextにゲームオーバー表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴールに到達した時
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //stateTextにゲームクリア表示
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //コインに衝突した時
        if (other.gameObject.tag == "CoinTag")
        {
            //スコア加算
            this.score += 10;
            //スコア加算を反映
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            //パーティクル再生
            GetComponent<ParticleSystem>().Play();
            //コインオブジェクト削除
            Destroy(other.gameObject);
        }
    }

    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    //左ボタン押し続ける
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタン離す
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    //右ボタン押し続ける
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタン離す
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}