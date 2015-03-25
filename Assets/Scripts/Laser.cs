using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public enum LaserOwner { Player, AI }
	public LaserOwner Owner;

	public float Damage = 15.0f;
	public float Speed = 3.0f;
	public float destroyTimer = 5.0f;

	// Use this for initialization
	void Start () {
		Destroy(transform.parent.gameObject, destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent.position += transform.parent.forward * Speed;
	}

	void OnTriggerEnter(Collider c)
	{
		// Early returns due to fix null refrence errors
		if (c == null) return;
		if (c.gameObject == null) return;

		switch (Owner)
		{
			case LaserOwner.Player:
				if (c.gameObject.tag == "AI")
				{
					//c.gameObject.GetComponent<AI>().health.DecreaseHealth(Damage);
					Destroy(gameObject.transform.parent.gameObject);
				}
				break;
			case LaserOwner.AI:
				if (c.gameObject.tag == "Player")
				{
					Player.me.health.DecreaseHealth(Damage);
					Destroy(gameObject.transform.parent.gameObject);
				}
				break;
			default:
				break;
		}
	}
}
