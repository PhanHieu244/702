/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text levelText;
    public Text coinText;
    public Animator coinImage;
    public Animator settings;

    public GameObject menuPanel, winPanel, lostPanel, shopPanel;

    private bool settingsState = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    public void UpdateCoins(int amount, bool animation)
    {
        coinText.text = amount.ToString();
        if (animation)
            coinImage.SetTrigger("Up");
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "LEVEL " + level;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void HandleSettings()
    {
        if (!settingsState)
            settings.SetTrigger("Open");
        else
            settings.SetTrigger("Close");

        settingsState = !settingsState;
    }
}
