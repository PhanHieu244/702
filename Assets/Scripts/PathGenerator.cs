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

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator instance;

    public float spawnRate = 0.5f;
    public List<Vector3> pathPoints = new List<Vector3>();

    private float distanceTravelled;
    private Vector3 lastPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled >= spawnRate)
        {
            pathPoints.Add(transform.position);

            distanceTravelled = 0;
        }
    }

    public int GetClosestPoint()
    {
        return pathPoints.Count - 1;
    }
}
