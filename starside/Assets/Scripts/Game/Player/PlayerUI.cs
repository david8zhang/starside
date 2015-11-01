using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	/** EXP bar */
	public Slider EXPBar;
	/** HP bar */
	public Slider healthBar;
	/** HP text */
	public Text HP;
	/** EXP text */
	public Text EXP;
	/** Defense text */
	public Text Defense;
	/** Luck text */
	public Text Luck;
	/** Level text */
	public Text level;

	/** The Player that this UI gets information from. */
	private Player player;

	void Awake () {
		player = GetComponent<Player> ();
		EXPBar.maxValue = player.getTotalEXP ();
		EXPBar.value = player.getEXP ();
		healthBar.maxValue = player.getHP ();
		healthBar.value = player.getCurrHP ();
		HP.text = "HP " + player.getCurrHP () + "/" + player.getHP();
		EXP.text = "EXP " + player.getEXP () + "/" + player.getTotalEXP ();
		Defense.text = "Defense: " + player.getDefense ();
		Luck.text = "Luck: " + player.getLuck ();
		level.text = "Level: " + player.getLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		EXPBar.maxValue = player.getTotalEXP ();
		EXPBar.value = player.getEXP ();
		healthBar.maxValue = player.getHP ();
		healthBar.value = player.getCurrHP ();
		HP.text = "HP " + player.getCurrHP () + "/" + player.getHP();
		EXP.text = "EXP " + player.getEXP () + "/" + player.getTotalEXP ();
		Defense.text = "Defense: " + player.getDefense ();
		Luck.text = "Luck: " + player.getLuck ();
		level.text = "Level: " + player.getLevel ();
	}
}
