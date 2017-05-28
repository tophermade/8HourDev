using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lumbergh : MonoBehaviour {

	// prefab references
	public GameObject[] boards;
	public GameObject[] trees;
	public GameObject[] bushes;
	public GameObject[] obstacles;
	public GameObject destroyedPrefab;
	public GameObject explosionEffect;
	public AudioClip click;
	public AudioClip laneChange;
	public AudioClip explode;




	// references
	public GameObject player;
	public GameObject mainCam;
	public GameObject spawnParent;
	public GameObject miscParent;
	public Rigidbody body;
	public GameObject[] spawnedBoards;
	public GameObject canvas;
	public GameObject panelIndex;
	public GameObject panelPlay;
	public GameObject panelGameOver;
	public GameObject scoreDisplayPlay;
	public GameObject highScoreDisplayPlay;
	public GameObject scoreDisplayGameOver;
	public GameObject highScoreDisplayGameOver;
	public AudioSource audioPlayer;


	// bools
	public bool playing = false;


	// numerical
	public int boardLength = 30;
	public int lastBoardAt = 30;
	public int currentScore = 0;

	public float leftLane = 8.9f;
	public float rightLane = 5.4f;

	public float baseSpeed = 6.0f;
	private float currentSpeed = 0.0f;
	private Vector3 startPosition;
	private Vector3 startCameraPosition;
	private float positionAtRoundStart = 0.0f;
	public float shakeAmount = 0;
	public float shakeSpeed = .8f;


	void Start()
	{
		body = player.GetComponent<Rigidbody>();
		startCameraPosition = mainCam.transform.position;
		startPosition = player.transform.position;
		audioPlayer = GetComponent<AudioSource>();
	}


	void AddBoardToGame()
	{
		lastBoardAt += boardLength;
		GameObject newBoard = Instantiate(boards[0], new Vector3(lastBoardAt, -5.9f, 7.1f), Quaternion.identity);
		newBoard.transform.parent = spawnParent.transform;
	}


	void ManageBoard()
	{
		if(player.transform.position.x > lastBoardAt - (boardLength *2)){
			AddBoardToGame();
		}
	}

	void MovePlayer()
	{
		if(body.velocity.x < baseSpeed){
			body.AddForce(player.transform.right * (body.velocity.x - baseSpeed));
		}
		mainCam.transform.position = new Vector3(player.transform.position.x - 7.4f, mainCam.transform.position.y, mainCam.transform.position.z);
	}

	void KeepScore(){
		currentScore = (int)((player.transform.position.x - positionAtRoundStart)/10);
		if(currentScore < 0){
			currentScore = 0;
		}
		UpdateScoreDisplay();
	}

	void UpdateScoreDisplay(){
		scoreDisplayPlay.GetComponent<Text>().text = currentScore.ToString();
		scoreDisplayGameOver.GetComponent<Text>().text = "Score: " + currentScore.ToString();
	}

	void SetupHighscore(){
		int highScore;
		if(PlayerPrefs.HasKey("highscore")){
			highScore = PlayerPrefs.GetInt("highscore");
		} else {
			highScore = 0;
		}

		if(currentScore > highScore){
			highScore = currentScore;
			PlayerPrefs.SetInt("highscore", highScore);
		}
		
		highScoreDisplayPlay.GetComponent<Text>().text = highScore.ToString();
		highScoreDisplayGameOver.GetComponent<Text>().text = "Best: " + highScore.ToString();
	}

	void MakeShake(float val){
		shakeAmount = val;
	}

	void ShakeScreen(){
		Vector2 shakeSpot = new Vector2(0,shakeAmount);
		if(shakeAmount > 0){
			shakeAmount *= shakeSpeed;
		}
		shakeAmount = -shakeAmount;
		mainCam.transform.Translate(shakeSpot, Space.Self);
	}


	void EndRound(){
		playing = false;
		SetupHighscore();
		StartCoroutine(ShowRoundEndMenu());
	}

	IEnumerator ShowRoundEndMenu(){
		yield return new WaitForSeconds(1.5f);
		panelPlay.SetActive(false);
		panelIndex.SetActive(false);
		panelGameOver.SetActive(true);
	}

	void ClearSpawned(){
		foreach(Transform spawned in spawnParent.transform){
			Destroy(spawned.gameObject); // gross
		}
		foreach(Transform spawned in miscParent.transform){
			Destroy(spawned.gameObject); // gross
		}
	}


	void FixedUpdate()
	{
		MovePlayer();
		ManageBoard();
		ShakeScreen();
		if(playing){
			KeepScore();
		}	
	}


	public void StartRound(){
		print("start round");
		positionAtRoundStart = player.transform.position.x;
		audioPlayer.PlayOneShot(click, 1.0f);
		RoundSetup();
	}

	void RoundSetup(){
		playing = true;
		currentScore = 0;
		panelPlay.SetActive(true);
		panelIndex.SetActive(false);
		panelGameOver.SetActive(false);
		
		spawnParent.transform.GetChild(spawnParent.transform.childCount -1).gameObject.GetComponent<RoadSection>().LateSpawnObstacles();
		spawnParent.transform.GetChild(spawnParent.transform.childCount -2).gameObject.GetComponent<RoadSection>().LateSpawnObstacles();	

		SetupHighscore();
	}

	public void RestartRound(){
		audioPlayer.PlayOneShot(click, 1.0f);
		player.transform.position = startPosition;
		player.SetActive(true);
		mainCam.transform.position = startCameraPosition;
		lastBoardAt = 30;
		ClearSpawned();
		RoundSetup();
		UpdateScoreDisplay();
	}
	
	public void ChangeLanes(){
		audioPlayer.PlayOneShot(laneChange, 1.0f);
		print("called changelanes " + player.transform.position.z);
		Vector3 temp = player.transform.position;
		if(player.transform.position.z > 8.0f){
			temp.z = rightLane;
		} else {
			temp.z = leftLane;
		}
		player.transform.position = temp;
	}

	public void HitObstacle(){
		audioPlayer.PlayOneShot(explode, 1.0f);
		print("hit obstacle");
		GameObject newWreck = Instantiate(destroyedPrefab, player.transform.position, Quaternion.identity);
		GameObject newExplosion = Instantiate(explosionEffect, player.transform.position, Quaternion.identity);
		newWreck.transform.parent = spawnParent.transform;
		player.SetActive(false);
		MakeShake(1.0f);
		EndRound();
	}

}
