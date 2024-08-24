/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel = 1;
    public int coins = 0;

    public CarController player;
    public Material roadMaterial;

    [HideInInspector]
    public bool playerLost = false;
    [HideInInspector]
    public bool playerWon = false;
    private bool gameStarted = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        coins = PlayerPrefs.GetInt("Coins", 0);
        currentLevel = PlayerPrefs.GetInt("LastLevel", 1);

        roadMaterial.mainTextureOffset = Vector2.zero;

        if(PlayerPrefs.GetInt("noAds") == 0 && ServicesManager.instance != null)
        {
            ServicesManager.instance.InitializeAdmob();
            ServicesManager.instance.InitializeUnityAds();
        }
    }

    private void Start()
    {
        UIManager.instance.UpdateCoins(coins, false);
        UIManager.instance.UpdateLevel(currentLevel);

        AudioManager.instance.MenuMusic();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            roadMaterial.mainTextureOffset += Vector2.down * 3 * Time.deltaTime;
        }
    }

    public void PlayerLost()
    {
        if (PlayerPrefs.GetInt("noAds") == 0 && ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowInterstitialAdmob();
            ServicesManager.instance.ShowInterstitialUnityAds();
        }

        if (!playerLost && !playerWon)
        {
            playerLost = true;

            UIManager.instance.lostPanel.SetActive(true);
            AudioManager.instance.Lost();
            AudioManager.instance.StopMusic();
        }
    }

    public void PlayerWon()
    {
        if (PlayerPrefs.GetInt("noAds") == 0 && ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowInterstitialAdmob();
            ServicesManager.instance.ShowInterstitialUnityAds();
        }

        if (playerWon)
            return;

        playerWon = true;

        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("LastLevel", currentLevel + 1);

        UIManager.instance.winPanel.SetActive(true);
        AudioManager.instance.Win();
        AudioManager.instance.StopMusic();

        Invoke("FakeMovement", 2f);
        Invoke("Reload", 4f);
    }

    private void Reload()
    {
        SceneManager.LoadScene(0);
    }

    private void FakeMovement()
    {
        player.StartMoving();
        player.fakeMovement = true;

        AudioManager.instance.MenuMusic();
    }

    public void StartGame()
    {
        player.StartMoving();

        gameStarted = true;

        UIManager.instance.menuPanel.SetActive(false);
        UIManager.instance.winPanel.SetActive(false);
        UIManager.instance.lostPanel.SetActive(false);

        AudioManager.instance.GameMusic();
    }

    public void AddCoin()
    {
        coins++;

        UIManager.instance.UpdateCoins(coins, true);
        AudioManager.instance.CoinPickUp();
    }
    
    public void AddCoin(int amount)
    {
        coins+= amount;

        UIManager.instance.UpdateCoins(coins, true);
        AudioManager.instance.CoinPickUp();
    }
    
    public void WatchVideoAds()
    {
        if (PlayerPrefs.GetInt("noAds") == 0 && ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowRewardedVideoAdAdmob();
            ServicesManager.instance.ShowRewardedVideoUnityAds();
        }
    }
    public void SecondChance()
    {
        playerLost = false;
        gameStarted = true;

        UIManager.instance.menuPanel.SetActive(false);
        UIManager.instance.winPanel.SetActive(false);
        UIManager.instance.lostPanel.SetActive(false);

        AudioManager.instance.GameMusic();

        player.ResetCar();
    }
}
