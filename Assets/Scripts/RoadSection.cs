using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSection : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		SpawnDecor();
	}
	
}
