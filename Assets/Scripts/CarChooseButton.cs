/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine.UI;
using UnityEngine;

public class CarChooseButton : MonoBehaviour
{
    public CarSetUp set;
    public Image carIcon;
    public Text costText;
    public GameObject locked;

    public void UpdateButton()
    {
        carIcon.sprite = set.icon;
        costText.text = set.cost.ToString() + '$';

        if (PlayerPrefs.GetInt(set.carName + "State", 0) == 0)
        {
            locked.SetActive(true);
            carIcon.enabled = false;
        }
        else
        {
            locked.SetActive(false);
            carIcon.enabled = true;
        }
    }

    public void ChooseCar()
    {
        if (PlayerPrefs.GetInt(set.carName + "State", 0) == 0)
            return;

        PlayerPrefs.SetString("Choosen Car", set.carName);

        SetupButtonManager.instance.choosenBorder.position = transform.position;
        CarSetUpManager.instance.UpdateCar(set.prefab);

        AudioManager.instance.Change();
    }

    public void BuyCar()
    {
        if (PlayerPrefs.GetInt(set.carName + "State", 0) == 0 && GameManager.instance.coins >= set.cost)
        {
            GameManager.instance.coins -= set.cost;
            PlayerPrefs.SetInt("Coins", GameManager.instance.coins);
            UIManager.instance.UpdateCoins(GameManager.instance.coins, true);

            PlayerPrefs.SetInt(set.carName + "State", 1);

            UpdateButton();

            AudioManager.instance.Spend();
        }
    }
}
