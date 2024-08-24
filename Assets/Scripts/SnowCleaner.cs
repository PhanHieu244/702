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

public class SnowCleaner : MonoBehaviour
{
    public GameObject pathPrefab;
    public Transform spawnPoint;
    public float spawnRate = 0.5f;
    public int poolSize = 100;

    private List<GameObject> pool = new List<GameObject>();
    private int currentPrefab = 0;
    private float distanceTravelled;
    private Vector3 lastPosition;

    private void Start()
    {
        GameObject newPP;
        GameObject poolObject = new GameObject("Pool");

        for (int i = 0; i < poolSize; i++)
        {
            newPP = Instantiate(pathPrefab);
            newPP.transform.SetParent(poolObject.transform);

            pool.Add(newPP);
        }

        lastPosition = transform.position;
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled >= spawnRate)
        {
            if (currentPrefab >= poolSize)
                currentPrefab = 0;

            pool[currentPrefab].transform.position = spawnPoint.position;
            pool[currentPrefab].transform.rotation = spawnPoint.rotation;

            currentPrefab++;
            distanceTravelled = 0;
        }
    }
}
