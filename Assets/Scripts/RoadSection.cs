using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSection : MonoBehaviour {

	public GameObject lumbergh;

	public GameObject[] decorSpawns;
	public GameObject[] obstacleSpawns;

	public GameObject[] decorPrefabs;
	public GameObject[] obstaclePrefabs;

	void SpawnDecor(){
		foreach(GameObject spawn in decorSpawns){
			if(Random.Range(1,6) == 3){
				GameObject newDecor = Instantiate(decorPrefabs[Random.Range(0,decorPrefabs.Length)], spawn.transform.position, Quaternion.identity);
			}
		}
	}

	void SpawnObstacles(){
		float lastX = 0f;
		foreach(GameObject spawn in obstacleSpawns){
			if(lastX != spawn.transform.position.x && Random.Range(0,2) == 1){
				GameObject newObstacle = Instantiate(obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)], spawn.transform.position, Quaternion.identity);
				lastX = spawn.transform.position.x;
			}
		}
	}

	public void LateSpawnObstacles(){
		SpawnObstacles();
	}

	// Use this for initialization
	void Start () {
		lumbergh = GameObject.Find("Lumbergh");
		if(lumbergh.GetComponent<Lumbergh>().playing){
			SpawnObstacles();
		}
		SpawnDecor();
	}
	
}
