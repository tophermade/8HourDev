using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumbergh : MonoBehaviour {

	// prefab references
	public GameObject[] boards;
	public GameObject[] trees;
	public GameObject[] bushes;

	public GameObject[] obstacles;


	// object references
	public GameObject player;
	public GameObject spawnParent;


	// bools
	public bool playing = false;


	// numerical
	public int boardLength = 30;
	public int lastBoardAt = 0;
	public int currentScore = 0;

	public float leftLane = 9.0f;
	public float rightLane = 5.0f;



	void AddBoardToGame(){
		lastBoardAt += boardLength;
	}


	void ManageBoard(){
		if(player.transform.position.x > lastBoardAt - (boardLength *2)){
			AddBoardToGame();
		}
	}

}
