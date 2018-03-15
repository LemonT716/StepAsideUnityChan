using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	private Animator myAnimator; //アニメーションするためのコンポーネントを入れる
	private Rigidbody myRigidbody;  //Unityちゃんを移動させるコポーネントを入れる
	private float forwardForce = 800.0f;  //前進するための力
	private float turnForce = 500.0f;  //左右に移動するための力
	private float upForce=500.0f;//ジャンプするための力
	private float movableRange=3.4f;//左右に移動できる範囲
	private float coefficient = 0.95f;//動きを減速させる係数
	private bool isEnd = false;//ゲーム終了の判定
	private GameObject stateText;//ゲーム終了時に表示するテキスト
	private GameObject scoreText;//スコアを表示するテキスト
	private int score=0;
	private bool isLButtonDown = false;
	private bool isRButtonDown = false;



	void Start () {
		this.myAnimator = GetComponent<Animator> ();  //Animatorコポーネントを取得
		this.myRigidbody=GetComponent<Rigidbody>();  //Rigidbodyコンポーネントを取得

		this.myAnimator.SetFloat ("Speed", 1);   //走るアニメーションを取得

		this.stateText = GameObject.Find ("GameResultText");//シーン中のstateTextオブジェクトを取得
		this.scoreText=GameObject.Find("ScoreText");//シーン中のscoreTextオブジェクトを取得
		
	}
	




	void Update () {
		//ゲーム終了ならUnityちゃんの動きを減速する
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);  //Unityちゃんに前方向の力を加える
		if ((Input.GetKey (KeyCode.LeftArrow)||this.isLButtonDown)&& -this.movableRange < this.transform.position.x) {
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow)||this.isRButtonDown)&& this.transform.position.x < this.movableRange) {
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}


	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other){
		//障害物に衝突した場合
		if(other.gameObject.tag=="CarTag"||other.gameObject.tag=="TrafficConeTag"){
			this.isEnd=true;
			this.stateText.GetComponent<Text>().text="GAME OVER";//stratTextにGAME OVERを表示
		}
	//ゴール地点に到着した場合
		if(other.gameObject.tag=="GoalTag"){
			this.isEnd=true;
			this.stateText.GetComponent<Text>().text="CLEAR!!";
		}

		if (other.gameObject.tag == "CoinTag") {
			this.score += 10;
			//ScoreTextに獲得した点数を表示
			this.scoreText.GetComponent<Text>().text="Score"+ this.score + "pt";
			//パーティクルを再生
			GetComponent<ParticleSystem>().Play();
			//接触したコインのオブジェクトを破棄
			Destroy (other.gameObject);
		}
	}


	//ジャンプボタンを押した時の処理
	public void GetMyJumpButtonDown(){
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown(){
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理
	public void GetMyLeftButttonUp(){
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown(){
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理
	public void GetMyRightButttonUp(){
		this.isRButtonDown = false;
	}








}
