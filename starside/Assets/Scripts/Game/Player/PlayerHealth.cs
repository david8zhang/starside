using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int currentHP = 100;
	public int defense = 5;
	public Slider healthSlider;
	public int startingHP;

	public void Awake() {
		healthSlider.value = currentHP;
		startingHP = currentHP;
		healthSlider.maxValue = startingHP;
		healthSlider.minValue = 0;
	}

	public void takeDamage(int damage) {
		if (damage >= defense) {
			currentHP -= (damage - defense);
		}
		healthSlider.value = currentHP;
		if (currentHP < 0) {
			// die
		}
	}
}
