using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AI : MonoBehaviour {
	public Health health;
	public float DamageUponCrash = 100.0f;
	//float time = 0f;

	public GameObject thruster;
	public GameObject lights;
	private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!health.Alive)
		{
			// Get the Boid
			Boid boid = health.gameObject.GetComponent<Boid>();

			// Go to its manager, then kill the boid
			boid.manager.KillBoid(boid);
		}

		CheckCulling();
	}

	void CheckCulling()
	{
		if (Camera.current == null) return;

		Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(transform.position);

		if (viewPortPoint.x > 0.0f && viewPortPoint.x < 1.0f &&
			viewPortPoint.y > 0.0f && viewPortPoint.y < 1.0f &&
			viewPortPoint.z > 0.0f)
		{
			//Debug.Log("In Screen");

			if (meshRenderer.enabled) return;
			else
			{
				meshRenderer.enabled = true;

				/// Uncomment for a full culling || keep commented to leave trail for immersion
				//thruster.SetActive(true);
				lights.SetActive(true);
			}
		}
		else
		{
			if (!meshRenderer.enabled) return;
			else
			{
				meshRenderer.enabled = false;

				/// Uncomment for a full culling || keep commented to leave trail for immersion
				//thruster.SetActive(false);
				lights.SetActive(false);
			}
		}
	}

	void OnBecameVisible()
	{
		//Debug.Log("Visible");
		//meshRenderer.enabled = true;
		//lights.SetActive(true);
		//thruster.SetActive(true);
	}
	void OnBecameInvisible()
	{
		//Debug.Log("InVisible");
		//meshRenderer.enabled = false;
		//lights.SetActive(false);
		//thruster.SetActive(false);
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "Player")
		{
			health.DecreaseHealth(DamageUponCrash);
		}

		if (c.tag == "Player Laser")
		{
			health.DecreaseHealth(c.GetComponent<Laser>().Damage);
		}
	}
}
