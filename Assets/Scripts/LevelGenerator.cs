/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public bool generateOnStart = false;
    public int minLenght = 8, maxLength = 40;
    public float lengthPerLevel = 1f;
    [HideInInspector]
    public int length;
    public int carProbability;

    [Header("Prefabs")]
    public GameObject road;
    public GameObject finish;
    public GameObject corner, middle, carPlace;
    public GameObject snowMiddle, snowTrans;
    public GameObject[] traps;

    private void Start()
    {
        if (generateOnStart)
            GenerateLevel();
    }

    public void GenerateLevel()
    {
        length = (int)(GameManager.instance.currentLevel * lengthPerLevel) + minLenght;

        if (length > maxLength)
            length = maxLength;

        if (GameObject.Find("Level"))
        {
            if (Application.isEditor)
                DestroyImmediate(GameObject.Find("Level"));
            else
                Destroy(GameObject.Find("Level"));
        }

        GameObject levelHolder = new GameObject("Level");
        GameObject newPart;

        // road
        for(int i = 0; i < length; i++)
        {
            newPart = Instantiate(road, Vector3.forward * 8 * i, Quaternion.identity);
            newPart.transform.localScale = new Vector3(2, 1, 1);
            newPart.transform.SetParent(levelHolder.transform);    
        }

        // finish
        for (int i = length; i < length + 8; i++)
        {
            newPart = Instantiate(road, Vector3.forward * 8 * i, Quaternion.identity);
            newPart.transform.localScale = new Vector3(2, 1, 1);
            newPart.transform.SetParent(levelHolder.transform);
        }

        newPart = Instantiate(finish, Vector3.forward * 8 * (length - 1), Quaternion.identity);
        newPart.transform.localScale = new Vector3(2, 1, 1);
        newPart.transform.SetParent(levelHolder.transform);

        // platforms
        newPart = Instantiate(corner, (Vector3.forward * 8) + (Vector3.left * 12), Quaternion.identity);
        newPart.transform.SetParent(levelHolder.transform);

        newPart = Instantiate(corner, (Vector3.forward * 8) + (Vector3.right * 12), Quaternion.Euler(0, 90, 0));
        newPart.transform.SetParent(levelHolder.transform);

        for (int i = 3; i < length - 3; i++)
        {
            int r = Random.Range(0, 100);
            if (r > carProbability)
                newPart = Instantiate(middle, (Vector3.forward * 8 * i) + (Vector3.left * 12), Quaternion.identity);
            else
                newPart = Instantiate(carPlace, (Vector3.forward * 8 * i) + (Vector3.left * 12), Quaternion.identity);

            newPart.transform.SetParent(levelHolder.transform);

            r = Random.Range(0, 100);
            if (r > carProbability)
                newPart = Instantiate(middle, (Vector3.forward * 8 * i) + (Vector3.right * 12), Quaternion.Euler(0, 180, 0));
            else
                newPart = Instantiate(carPlace, (Vector3.forward * 8 * i) + (Vector3.right * 12), Quaternion.Euler(0, 180, 0));

            newPart.transform.SetParent(levelHolder.transform);
        }

        newPart = Instantiate(corner, (Vector3.forward * 8 * (length - 2)) + (Vector3.left * 12), Quaternion.Euler(0, -90, 0));
        newPart.transform.SetParent(levelHolder.transform);

        newPart = Instantiate(corner, (Vector3.forward * 8 * (length - 2)) + (Vector3.right * 12), Quaternion.Euler(0, 180, 0));
        newPart.transform.SetParent(levelHolder.transform);

        // snow
        newPart = Instantiate(snowTrans, (Vector3.forward * 8) + (Vector3.up * 0.2f), Quaternion.identity);
        newPart.transform.SetParent(levelHolder.transform);

        for (int i = 1; i < (length / 2) - 1; i++)
        {
            newPart = Instantiate(snowMiddle, (Vector3.forward * 16 * i) + (Vector3.up * 0.2f), Quaternion.identity);
            newPart.transform.SetParent(levelHolder.transform);
        }

        newPart = Instantiate(snowTrans, new Vector3(0, 0.2f, 16 * ((length / 2) - 1) - 8), Quaternion.identity);
        newPart.transform.SetParent(levelHolder.transform);

        // traps
        for (int i = 1; i < (length / 4); i++)
        {
            int r = Random.Range(0, traps.Length);

            newPart = Instantiate(traps[r], (Vector3.forward * 32 * i), Quaternion.identity);
            newPart.transform.SetParent(levelHolder.transform);
        }
    }
}
