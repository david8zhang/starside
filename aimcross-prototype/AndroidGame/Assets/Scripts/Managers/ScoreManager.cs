﻿using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


/// <summary>
/// Manage Scorekeeping and all Google Play Services
/// </summary>
public class ScoreManager : MonoBehaviour {

	public int lastScore;
	public int highScore;

	public int gamesPlayed;
	public int buttonsPressed;

	public static ScoreManager instance;

	void Awake()
	{
		// Make this a singleton
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		if (!PlayerPrefs.HasKey("TutorialComplete"))
			PlayerPrefs.SetInt ("TutorialLevel", 1);

		// get the high score from the playerPrefs
		highScore = PlayerPrefs.GetInt("High Score");
		gamesPlayed = PlayerPrefs.GetInt ("Games Played");

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		//PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform

		PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
		});

		Application.targetFrameRate = 35;
	}

	public void UpdateScore(int score)
	{
		lastScore = score;
		if (lastScore > highScore)
			highScore = lastScore;

		// if this level is not the tutorial
		if (Application.loadedLevelName.Equals("Game"))
		{
			// store the high score locally
			PlayerPrefs.SetInt ("High Score", highScore);
			
			if (highScore >= 50)
			{
				GPGUnlockAchievement(
					"CgkItczL6uMHEAIQBQ");
			}
			if (highScore >= 100)
			{
				GPGUnlockAchievement(
					"CgkItczL6uMHEAIQCA");
			}
			if (highScore >= 175)
			{
				GPGUnlockAchievement(
					"CgkItczL6uMHEAIQCQ");
			}
		}
	}

	public void GPGReportScore()
	{
		Social.ReportScore(highScore, "CgkItczL6uMHEAIQBw", (bool success) => {
		});
	}

	public void IncrementButtonsPressed()
	{
		buttonsPressed ++;
		GPGIncrementAchievement(
			"CgkItczL6uMHEAIQAw", 1);
		GPGIncrementAchievement(
			"CgkItczL6uMHEAIQBA", 1);
		GPGIncrementAchievement(
			"CgkItczL6uMHEAIQAg", 1);
		GPGIncrementAchievement(
			"CgkItczL6uMHEAIQCg", 1);

	}

	public void GPGUnlockAchievement(string code)
	{
		Social.ReportProgress(code, 100.0f, (bool success) => {
		});
	}

	public void GPGIncrementAchievement(string code, int progress)
	{
		PlayGamesPlatform.Instance.IncrementAchievement(
			code, progress, (bool success) => {
		});
	}
}
