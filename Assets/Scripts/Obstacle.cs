using UnityEngine;

public class Obstacle : MonoBehaviour {

	private GameObject lumbergh;

	void Start(){
		lumbergh = GameObject.Find("Lumbergh");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player"){
			lumbergh.SendMessage("HitObstacle");
		}
	}

}
