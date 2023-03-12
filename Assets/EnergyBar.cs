using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnergyBar : MonoBehaviour
{
	public float energy = 100.0f;
	//public static int planetsRemaining = 9;
	public RectTransform rectTransform;
	//public TextMeshProUGUI planetsRemainingTxt;
	bool dead = false;

	//public float deathTimer = 3f;
	public float getEnergy()
	{
		return energy;
	}

	public void depleteEnergy(float amount)
	{
		energy -= amount;
	}

	public void replenishEnergy(float amount)
	{
		energy += amount;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float width = 100;
		float height = 80;

		width *= energy / 100.0f;
		rectTransform.sizeDelta = new Vector2(width, height);
		//planetsRemainingTxt.text = "PLANETS REMAINING: " + planetsRemaining;
		if (energy > 100.0f)
		{
			energy = 100.0f;
		}

		if (energy <= 0.0f)
		{
			energy = 0.0f;
            if (!GetComponentInParent<CarController>().dead)
            {
				GetComponentInParent<CarController>().Die();
			}
				
			//deathTimer -= Time.deltaTime;

		}

		/*if (deathTimer <= 0f)
		{
			if (!dead)
			{
				dead = true;
				Debug.Log("You died");
				SceneManager.LoadSceneAsync("GameOver");

			}
		}*/
	}
}
