/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ServicesManager))]
public class AdsManagerEditor : Editor
{
    SerializedObject targetSer;
    ServicesManager targetScript;

    GUIStyle labelGuiStyle = new GUIStyle();
    GUIStyle networkGuiStyle = new GUIStyle();
    GUIStyle tooltipGuiStyle = new GUIStyle();
    GUIStyle supportGuiStyle = new GUIStyle();

    bool showAdmobSettings = false;
    bool showUnitySettings = false;
    bool showFacebookSettings = false;
    bool showIAPSettings = false;

    private void OnEnable()
    {
        SetGuistyles();

        targetScript = (ServicesManager)target;
        targetSer = new SerializedObject(target);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(target, "Service Manager");
        {
            serializedObject.Update();
            GUILayout.Space(15);
            DrawAdmobBox();
            GUILayout.Space(8);
            DrawUnityBox();
            GUILayout.Space(8);
            DrawIAPBox();
            GUILayout.Space(15);

            serializedObject.ApplyModifiedProperties();
        }
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
        GUILayout.Space(15);
        DrawPluginsButtons();
        GUILayout.Space(10);
    }
    void SetGuistyles()
    {
        labelGuiStyle.normal.textColor = Color.blue;
        labelGuiStyle.fontSize = 13;
        labelGuiStyle.fontStyle = FontStyle.BoldAndItalic;
        labelGuiStyle.alignment = TextAnchor.LowerRight;

        networkGuiStyle.normal.textColor = Color.blue;
        networkGuiStyle.fontSize = 14;
        networkGuiStyle.fontStyle = FontStyle.Bold;
        networkGuiStyle.alignment = TextAnchor.LowerCenter;

        tooltipGuiStyle.fontSize = 10;
        tooltipGuiStyle.alignment = TextAnchor.MiddleLeft;

        supportGuiStyle.alignment = TextAnchor.LowerRight;
        supportGuiStyle.fontStyle = FontStyle.Italic;
        supportGuiStyle.fontSize = 11;
    }
    bool EnableAdmob
    {
        get
        {
            return AdsData.AdmobIsEnable;
        }
        set
        {
            if (value == AdsData.AdmobIsEnable)
                return;
            AdsData.AdmobIsEnable = value;
            AdsDefineSymbol.AddDefineSymbol(AdsDefineSymbol.admobSymbol, value);
        }
    }
    bool EnableUnityAds
    {
        get
        {
            return AdsData.UnityAdsIsEnable;
        }
        set
        {
            if (value == AdsData.UnityAdsIsEnable)
                return;
            AdsData.UnityAdsIsEnable = value;
            AdsDefineSymbol.AddDefineSymbol(AdsDefineSymbol.unityAdsSymbol, value);
        }
    }
    bool EnableFacebook
    {
        get
        {
            return AdsData.FacebookIsEnable;
        }
        set
        {
            if (value == AdsData.FacebookIsEnable)
                return;
            AdsData.FacebookIsEnable = value;
            AdsDefineSymbol.AddDefineSymbol(AdsDefineSymbol.facebookSymbol, value);
        }
    }
    bool EnableIAP
    {
        get
        {
            return AdsData.IAPIsEnable;
        }
        set
        {
            if (value == AdsData.IAPIsEnable)
                return;
            AdsData.IAPIsEnable = value;
            AdsDefineSymbol.AddDefineSymbol(AdsDefineSymbol.iapSymbol, value);
        }
    }
    void DrawAdmobBox()
    {
        EditorGUILayout.LabelField("ADMOB ADS    ", networkGuiStyle);
        EditorGUILayout.BeginVertical("BOX");
        GUILayout.Space(10);
        if (GUILayout.Button(EnableAdmob ? "DISABLE ADMOB" : "ENABLE ADMOB",GUILayout.Height(30)))
        {
            if (!EnableAdmob)
            {
                EditorApplication.Beep();
                if (EditorUtility.DisplayDialog("Warning!", "Enable a network only if you have already downloaded the appropriate plugin.", "Enable it", "Cancel"))
                    EnableAdmob = !EnableAdmob;
            }
            else
            {
                EnableAdmob = !EnableAdmob;
            }
        }
        if (EnableAdmob)
        {
            GUILayout.Space(5);
            EditorGUI.indentLevel++;
            showAdmobSettings = EditorGUILayout.Foldout(showAdmobSettings, "SETTINGS", true);
            if (showAdmobSettings)
            {
                EditorGUI.indentLevel++;
                DrawAdmobSettings();
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        GUILayout.Space(10);
        EditorGUILayout.EndVertical();
    }
    void DrawAdmobSettings()
    {
        GUILayout.Space(5); 
        targetScript.enableTestMode = EditorGUILayout.Toggle("Test mode*", targetScript.enableTestMode);
        EditorGUILayout.LabelField("*use test mode during development", tooltipGuiStyle);
        GUILayout.Space(8);
        targetScript.appID = EditorGUILayout.TextField("App ID", targetScript.appID); 
        GUILayout.Space(15); 
        targetScript.bannerID = EditorGUILayout.TextField("Banner ID", targetScript.bannerID); 
        GUILayout.Space(5); 
        targetScript.interstitialID = EditorGUILayout.TextField("Interstitial ad ID", targetScript.interstitialID);
        GUILayout.Space(5); 
        targetScript.rewardedVideoAdsID = EditorGUILayout.TextField("Rewarded ad ID", targetScript.rewardedVideoAdsID);
        GUILayout.Space(20);
        targetScript.rewardedCoins = EditorGUILayout.IntField("Rewarded coins", targetScript.rewardedCoins);
    }
    void DrawUnityBox()
    {
        EditorGUILayout.LabelField("UNITY ADS   ", networkGuiStyle); 
        EditorGUILayout.BeginVertical("BOX");
        GUILayout.Space(10);
        if (GUILayout.Button(EnableUnityAds ? "DISABLE UNITY ADS" : "ENABLE UNITY ADS", GUILayout.Height(30)))
        {
            if (!EnableUnityAds)
            {
                EditorApplication.Beep();
                if (EditorUtility.DisplayDialog("Warning!", "Enable a network only if you have already downloaded the appropriate plugin.", "Enable it", "Cancel"))
                    EnableUnityAds = !EnableUnityAds;
            }
            else
            {
                EnableUnityAds = !EnableUnityAds;
            }
        }
        if (EnableUnityAds)
        {
            GUILayout.Space(5);
            EditorGUI.indentLevel++;
            showUnitySettings = EditorGUILayout.Foldout(showUnitySettings, "SETTINGS", true);
            if (showUnitySettings)
            {
                EditorGUI.indentLevel++;
                DrawUnitySettings();
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        GUILayout.Space(10);
        EditorGUILayout.EndVertical();
    }

    void DrawUnitySettings()
    {
        GUILayout.Space(5);
        targetScript.testMode = EditorGUILayout.Toggle("Test mode*", targetScript.testMode);
        EditorGUILayout.LabelField("*use test mode during development", tooltipGuiStyle);
        GUILayout.Space(15);
        targetScript.gameID = EditorGUILayout.TextField("App Android ID", targetScript.gameID);
        GUILayout.Space(5);
        targetScript.bannerPlacementID = EditorGUILayout.TextField("Banner placement ID",targetScript.bannerPlacementID);
        targetScript.videoAdPlacementID = EditorGUILayout.TextField("Video placement ID", targetScript.videoAdPlacementID);
        targetScript.rewardedVideoAdPlacementID = EditorGUILayout.TextField("Rewarded video placement ID", targetScript.rewardedVideoAdPlacementID);
        GUILayout.Space(20);
        targetScript.rewardedCoins = EditorGUILayout.IntField("Rewarded coins", targetScript.rewardedCoins);
    }
    void DrawIAPBox()
    {
        EditorGUILayout.LabelField("IAP   ", networkGuiStyle);
        EditorGUILayout.BeginVertical("BOX");
        GUILayout.Space(10);
        if (GUILayout.Button(EnableIAP ? "DISABLE IAP" : "ENABLE IAP", GUILayout.Height(30)))
        {
            if (!EnableIAP)
            {
                EditorApplication.Beep();
                if (EditorUtility.DisplayDialog("Warning!", "Enable a IAP only if you have already activated IAP from SERVICES.", "Enable it", "Cancel"))
                    EnableIAP = !EnableIAP;
            }
            else
            {
                EnableIAP = !EnableIAP;
            }
        }
        if (EnableIAP)
        {
            GUILayout.Space(5);
            EditorGUI.indentLevel++;
            showIAPSettings = EditorGUILayout.Foldout(showIAPSettings, "SETTINGS", true);
            if (showIAPSettings)
            {
                EditorGUI.indentLevel++;
                DrawIapSettings();
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        GUILayout.Space(10);
        EditorGUILayout.EndVertical();
    }
    void DrawIapSettings()
    {
        GUILayout.Space(5);
        EditorGUILayout.LabelField("Put your IAP ids from you google play or appstore account", tooltipGuiStyle);
        GUILayout.Space(10);
        targetScript.buy10000chicks = EditorGUILayout.TextField("Buy 10000 chicks ID", targetScript.buy10000chicks);
        GUILayout.Space(10);
        targetScript.buy100000chicks = EditorGUILayout.TextField("Buy 100000 chicks ID", targetScript.buy100000chicks);
        GUILayout.Space(10);
        targetScript.buy1000000chicks = EditorGUILayout.TextField("Buy 1000000 chicks ID", targetScript.buy1000000chicks);
    }
    void DrawPluginsButtons()
    {
        EditorGUILayout.LabelField("PLUGINS", networkGuiStyle);
        EditorGUILayout.BeginVertical("BOX");
        GUILayout.Space(8);
        if (GUILayout.Button("DOWNLOAD ADMOB SDK", GUILayout.Height(30)))
        {
            Application.OpenURL("https://github.com/googleads/googleads-mobile-unity/releases");
        }
        GUILayout.Space(8);

        if (GUILayout.Button("DOWNLOAD UNITY ADS SDK*", GUILayout.Height(30)))
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/66123");
        }
        GUILayout.Space(8);
        EditorGUILayout.EndVertical();
    }

    [MenuItem("BubbleShooter/Services Manager/Add to the scene")]
    static void AddServicesManagerToScene()
    {
        var prefab = (GameObject)Instantiate(Resources.Load("ServicesManager"));
        Undo.RegisterCreatedObjectUndo(prefab, "Added Services Manager");
        prefab.name = "ServicesManager";
    }
}
