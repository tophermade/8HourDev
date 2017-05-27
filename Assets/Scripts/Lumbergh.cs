using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumbergh : MonoBehaviour {

	// prefab references
	public GameObject[] boards;
	public GameObject[] trees;
	public GameObject[] bushes;
	public GameObject[] obstacles;



	// references
	public GameObject player;
	public GameObject mainCam;
	public GameObject spawnParent;
	public Rigidbody body;
	public GameObject[] spawnedBoards;
	public GameObject canvas;
	public GameObject panelIndex;
	public GameObject panelPlay;
	public GameObject panelGameOver;


	// bools
	public bool playing = false;


	// numerical
	public int boardLength = 30;
	public int lastBoardAt = 30;
	public int currentScore = 0;

	public float leftLane = 9.0f;
	public float rightLane = 5.0f;

	public float baseSpeed = 6.0f;
	private float currentSpeed = 0.0f;


	void Start()
	{
		body = player.GetComponent<Rigidbody>();
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
			body.AddForce(player.transform.right * (body.velocity.x - 6.2f));
		}
		mainCam.transform.position = new Vector3(player.transform.position.x - 7.4f, mainCam.transform.position.y, mainCam.transform.position.z);
	}
	void FixedUpdate()
	{
		MovePlayer();
		ManageBoard();

		if(playing){
		}	
	}


	public void StartRound(){
		print("start round");
		panelPlay.SetActive(true);
		panelIndex.SetActive(false);
		panelGameOver.SetActive(false);
	}
}
