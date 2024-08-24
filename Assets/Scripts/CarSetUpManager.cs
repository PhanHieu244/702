/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using Cinemachine;
using UnityEngine;

public class CarSetUpManager : MonoBehaviour
{
    public static CarSetUpManager instance;
    public int currentID = 0;

    public CinemachineStateDrivenCamera cameraState;
    public Transform player;
    public CarController carController;
    public SnowCleaner snowCleaner;

    private GameObject currentCar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);   
    }

    public void UpdateCar(GameObject newCar)
    {
        if (currentCar != null)
            Destroy(currentCar);

        currentCar = Instantiate(newCar, player);
        CarParts cp = currentCar.GetComponent<CarParts>();

        cameraState.m_AnimatedTarget = cp.anim;
        carController.anim = cp.anim;
        snowCleaner.spawnPoint = cp.snowCleanerSpawnPoint;
    }
}
