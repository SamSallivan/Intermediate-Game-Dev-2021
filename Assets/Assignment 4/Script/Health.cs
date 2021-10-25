using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
	public float maxHealthPoint = 10f;

	public float HealthPoint;

	public bool invincible = false;

	public bool dead = false;

	//public GameObject StrawberryPrefab;

	private void Start()
	{
		HealthPoint = maxHealthPoint;
	}

	private void Update()
	{
		if (HealthPoint <= 0)
		{
			dead = true;
			if (name == "Madeline")
			{
				GetComponent<PlayerMovement>().canMove = false;
				GetComponent<PlayerMovement>().Animation("Player_Death");
			}
            else
            {
				StartCoroutine(DelayedDeath());
				GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().HealthPoint++;
			}
		}

		if (HealthPoint > maxHealthPoint)
		{
			HealthPoint = maxHealthPoint;
		}

	}


	IEnumerator DelayedDeath()
	{
		yield return new WaitForSeconds(0.5f);
		Instantiate(GameObject.Find("Strawberry"), transform.position - new Vector3(0, 1), Quaternion.identity);
		Destroy(gameObject);
	}

}
