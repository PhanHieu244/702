/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;

public class SetupButtonManager : MonoBehaviour
{
    public static SetupButtonManager instance;

    public CarSetUp[] sets;
    public GameObject button;
    public Transform shopPanel;
    public RectTransform choosenBorder;

    private List<CarChooseButton> buttonList = new List<CarChooseButton>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start()
    {
        for(int i = 0; i < sets.Length; i++)
        {
            CarChooseButton newButton = Instantiate(button, shopPanel).GetComponent<CarChooseButton>();
            newButton.set = sets[i];

            if (newButton.set.carName == PlayerPrefs.GetString("Choosen Car", "default"))
                CarSetUpManager.instance.UpdateCar(sets[i].prefab);

            newButton.UpdateButton();
        }

        if (PlayerPrefs.GetString("Choosen Car", "default") == "default" || PlayerPrefs.GetString("Choosen Car", "default") == sets[0].carName)
        {
            PlayerPrefs.SetString("Choosen Car", sets[0].carName);
            PlayerPrefs.SetInt(sets[0].carName + "State", 1);
            CarSetUpManager.instance.UpdateCar(sets[0].prefab);
        }
    }

    public void UpdateSet()
    {
        foreach(CarChooseButton button in buttonList)
        {        
            button.UpdateButton();
        }
    }
}
 
