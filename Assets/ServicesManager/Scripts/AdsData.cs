/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;

public class AdsData : MonoBehaviour
{
    public static bool AdmobIsEnable
    {
        get
        {
            if (PlayerPrefs.GetString("AdmobIsEnable") == "true")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                PlayerPrefs.SetString("AdmobIsEnable", "true");
            else
                PlayerPrefs.SetString("AdmobIsEnable", "false");
            PlayerPrefs.Save();
        }
    }
    public static bool UnityAdsIsEnable
    {
        get
        {
            if (PlayerPrefs.GetString("UnityAdsIsEnable") == "true")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                PlayerPrefs.SetString("UnityAdsIsEnable", "true");
            else
                PlayerPrefs.SetString("UnityAdsIsEnable", "false");
            PlayerPrefs.Save();
        }
    }
    public static bool FacebookIsEnable
    {
        get
        {
            if (PlayerPrefs.GetString("FacebookIsEnable") == "true")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                PlayerPrefs.SetString("FacebookIsEnable", "true");
            else
                PlayerPrefs.SetString("FacebookIsEnable", "false");
            PlayerPrefs.Save();
        }
    }
    public static bool IAPIsEnable
    {
        get
        {
            if (PlayerPrefs.GetString("IapIsEnable") == "true")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                PlayerPrefs.SetString("IapIsEnable", "true");
            else
                PlayerPrefs.SetString("IapIsEnable", "false");
            PlayerPrefs.Save();
        }
    }
}
