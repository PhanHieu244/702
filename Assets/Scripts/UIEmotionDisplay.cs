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
using UnityEngine;
using UnityEngine.UI;

public class UIEmotionDisplay : MonoBehaviour
{
    public static UIEmotionDisplay instance;

    public Canvas canvas;
    public Camera mainCamera;
    public Vector3 offset;
    public GameObject[] emotions;

    public class TargetEmotion
    {
        public Transform target;
        public Image emotion;
    }

    public List<TargetEmotion> targetEmotions = new List<TargetEmotion>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void FixedUpdate()
    {
        foreach(TargetEmotion tEmotion in targetEmotions)
        {
            Vector3 newPos = mainCamera.WorldToScreenPoint(tEmotion.target.position + offset);
            newPos.x = Mathf.Clamp(newPos.x, 150, Screen.width - 150);
            newPos.y = Mathf.Clamp(newPos.y, 150, Screen.height - 150);

            tEmotion.emotion.transform.position = newPos;
        }
    }

    public void ShowEmotion(int emotionID, Transform target)
    {
        TargetEmotion newTEmotion = new TargetEmotion();
        newTEmotion.emotion = Instantiate(emotions[emotionID], canvas.transform).GetComponent<Image>();
        newTEmotion.target = target;

        targetEmotions.Add(newTEmotion);
    }
}
