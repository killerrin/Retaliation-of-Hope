using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public bool invulnerable = false;
	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public bool resurrectable = false;

	public bool autoRegenHealth = false;
	public float TimeBeforeAutoRegenInSeconds = 3.0f;
	public float RateOfRegeneration = 2.0f;
	private float timeSinceLastDamageDecrease = 0.0f;

	public bool setActiveWhenNoHealth = true;
	public bool destroyObjectWhenNoHealth = true;
	public float deathTimer = 1.5f;

	public bool Alive {get;set;}

    public GameObject healthIncreasePrefab;
	public GameObject decreaseHealthPrefab;
	public GameObject deadPrefab;

	public void SetHealth(float _health) { health = _health; }
	public float IncreaseHealth(float _health) 
	{
		if (invulnerable) return health;
		if (health >= maxHealth) return health;

		if (!Alive)
		{
			if (!resurrectable) return health;

			Alive = true;
			if (setActiveWhenNoHealth) { gameObject.SetActive(true); }
		}

		health += _health;
		if (health > maxHealth) health = maxHealth;

        if (healthIncreasePrefab != null) { }

		return health; 
	}
	public float DecreaseHealth(float _health) 
	{
		// If the player is invulerable, there is no point being in the method, so leave.
		if (invulnerable) return health;

		health -= _health;

        if (decreaseHealthPrefab != null) { Instantiate(decreaseHealthPrefab, transform.position, transform.rotation); }

		if (health <= 0) { 
			health = 0;

			Alive = false;

            if (deadPrefab != null) { Instantiate(deadPrefab, transform.position, transform.rotation); }

			if (setActiveWhenNoHealth) { gameObject.SetActive(false); }
			if (destroyObjectWhenNoHealth) { Destroy(gameObject, deathTimer); }
		}

		timeSinceLastDamageDecrease = 0.0f;

		return health;
	}
	void RegenerateHealth()
	{
		if (timeSinceLastDamageDecrease >= TimeBeforeAutoRegenInSeconds) { 
            IncreaseHealth(RateOfRegeneration);
        }
		else { timeSinceLastDamageDecrease += Time.deltaTime; }
	}

	void Start () {
		if (health > 0)
			Alive = true;
	}
	
	void Update () {
		if (autoRegenHealth) { RegenerateHealth(); }
	}
}
