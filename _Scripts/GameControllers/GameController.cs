using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{

	public static GameController instance;

	private GameData data;

	public int currentLevel = -1;

	public int currentScore;

	public int currentLives;

	public bool isGameStartedFromLevelMenu;

	public bool isGameStartedFirstTime;

	public bool isMusicOn;

	public bool doubleCoins;

	public int selectedPlayer;
	public int selectedWeapon;
	public int coins;
	public int highScore;

	public bool[] players;
	public bool[] levels;
	public bool[] weapons;
	public bool[] achievements;
	public bool[] collectedItems;

	void Awake()
	{
		MakeSingleton();
		InitializeGameVariables();
	}

	// Use this for initialization

	void MakeSingleton()
	{

		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

	}

	void InitializeGameVariables()
	{

		Load();

		if (data != null)
		{
			isGameStartedFirstTime = true;
		}
		else
		{
			isGameStartedFirstTime = true;
		}

		if (isGameStartedFirstTime)
		{
			// the game is started for the first time

			highScore = 500;
			coins = 100000;

			selectedPlayer = 0;
			selectedWeapon = 0;

			isGameStartedFirstTime = false;
			isMusicOn = true;

			players = new bool[6];
			levels = new bool[40];
			weapons = new bool[4];
			achievements = new bool[8];
			collectedItems = new bool[40]; 

			players[0] = true;
			players[1] = true;
			for (int i = 2; i < players.Length; i++)
			{
				players[i] = false;
			}

			levels[0] = true;
			for (int i = 1; i < levels.Length; i++)
			{
				levels[i] = false;
			}

			weapons[0] = true;
			for (int i = 1; i < weapons.Length; i++)
			{
				weapons[i] = false;
			}

			for (int i = 0; i < achievements.Length; i++)
			{
				achievements[i] = false;
			}

			for (int i = 0; i < collectedItems.Length; i++)
			{
				collectedItems[i] = false;
			}

			data = new GameData();

			data.setHighScore(highScore);
			data.setCoins(coins);
			data.setSelectedPlayer(selectedPlayer);
			data.setSelectedWeapon(selectedWeapon);
			data.setIsGameStartedFirstTime(isGameStartedFirstTime);
			data.setIsMusicOn(isMusicOn);
			data.setPlayers(players);
			data.setLevels(levels);
			data.setWeapons(weapons);
			data.setAchievements(achievements);
			data.setCollectedItems(collectedItems);

			Save();

			Load();

		}
		else
		{
			// the game has been played already so load 

			highScore = data.getHighScore();
			coins = data.getCoins();
			selectedPlayer = data.getSelectedPlayer();
			selectedWeapon = data.getSelectedWeapon();
			isGameStartedFirstTime = data.getIsGameStartedFirstTime();
			isMusicOn = data.getIsMusicOn();
			players = data.getPlayers();
			levels = data.getLevels();
			weapons = data.getWeapons();
			achievements = data.getAchievements();
			collectedItems = data.getCollectedItems();

		}


	} // initialize variables

	public void Save()
	{

		FileStream file = null;

		try
		{

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Create(Application.persistentDataPath + "/GameData.dat");

			if (data != null)
			{

				data.setHighScore(highScore);
				data.setCoins(coins);
				data.setIsGameStartedFirstTime(isGameStartedFirstTime);
				data.setPlayers(players);
				data.setLevels(levels);
				data.setWeapons(weapons);
				data.setSelectedPlayer(selectedPlayer);
				data.setSelectedWeapon(selectedWeapon);
				data.setIsMusicOn(isMusicOn);
				data.setAchievements(achievements);
				data.setCollectedItems(collectedItems);

				bf.Serialize(file, data);

			}

		}
		catch (Exception e)
		{

		}
		finally
		{
			if (file != null)
			{
				file.Close();
			}
		}

	} // save data

	public void Load()
	{

		FileStream file = null;

		try
		{

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

			data = (GameData)bf.Deserialize(file);

		}
		catch (Exception e)
		{

		}
		finally
		{
			if (file != null)
			{
				file.Close();
			}
		}
	} // load data

} // game controller

[Serializable]
class GameData
{

	private bool isGameStartedFirstTime;

	private bool isMusicOn;

	private bool doubleCoins;

	private int selectedPlayer;
	private int selectedWeapon;
	private int coins;
	private int highScore;

	private bool[] players;
	private bool[] levels;
	private bool[] weapons;
	private bool[] achievements;
	private bool[] collectedItems;

	public void setDoubleCoins(bool doubleCoins)
	{
		this.doubleCoins = doubleCoins;
	}

	public bool getDoubleCoins()
	{
		return this.doubleCoins;
	}

	public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
	{
		this.isGameStartedFirstTime = isGameStartedFirstTime;
	}

	public bool getIsGameStartedFirstTime()
	{
		return this.isGameStartedFirstTime;
	}

	public void setIsMusicOn(bool isMusicOn)
	{
		this.isMusicOn = isMusicOn;
	}

	public bool getIsMusicOn()
	{
		return this.isMusicOn;
	}

	public void setSelectedPlayer(int selectedPlayer)
	{
		this.selectedPlayer = selectedPlayer;
	}

	public int getSelectedPlayer()
	{
		return this.selectedPlayer;
	}

	public void setSelectedWeapon(int selectedWeapon)
	{
		this.selectedWeapon = selectedWeapon;
	}

	public int getSelectedWeapon()
	{
		return this.selectedWeapon;
	}

	public void setCoins(int coins)
	{
		this.coins = coins;
	}

	public int getCoins()
	{
		return this.coins;
	}

	public void setHighScore(int highScore)
	{
		this.highScore = highScore;
	}

	public int getHighScore()
	{
		return this.highScore;
	}

	public void setPlayers(bool[] players)
	{
		this.players = players;
	}

	public bool[] getPlayers()
	{
		return this.players;
	}

	public void setLevels(bool[] levels)
	{
		this.levels = levels;
	}

	public bool[] getLevels()
	{
		return this.levels;
	}

	public void setWeapons(bool[] weapons)
	{
		this.weapons = weapons;
	}

	public bool[] getWeapons()
	{
		return this.weapons;
	}

	public void setAchievements(bool[] achievements)
	{
		this.achievements = achievements;
	}

	public bool[] getAchievements()
	{
		return this.achievements;
	}

	public void setCollectedItems(bool[] collectedItems)
	{
		this.collectedItems = collectedItems;
	}

	public bool[] getCollectedItems()
	{
		return this.collectedItems;
	}

} // game data




















