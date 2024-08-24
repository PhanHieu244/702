/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using UnityEngine;

public class AICar : MonoBehaviour
{
    public float speed = 0;
    public float turnSpeed = 1;
    public Transform rayForward;
    public Transform rayLeft;

    private int myTargetID = 0;
    [HideInInspector]
    public bool isStuck = true;
    private bool finished = false;
    private bool almostFree = false;
    private Vector3 target;
    private Vector3 direction;
    private Quaternion lookRotation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isStuck)
            return;

        RaycastHit hit;

        if (Physics.Raycast(rayForward.position, transform.forward, out hit, 1.5f) || Physics.Raycast(rayLeft.position, transform.forward - transform.right, out hit, 2))
        {
            if (hit.collider.tag == "Car")
                speed = 3;

            if (hit.collider.tag == "Player")
                speed = 5;
        }
        else
        {
            speed = 10;
        }
    }

    private void FixedUpdate()
    {
        if (isStuck)
            return;

        if (!finished)
        {
            direction = (target - transform.position).normalized;
            direction.y = 0;

            lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
        }

        if (Vector3.Distance(transform.position, target) < 3f)
        {
            if (PathGenerator.instance.pathPoints.Count - 1 > myTargetID)
            {
                myTargetID++;

                target = PathGenerator.instance.pathPoints[myTargetID];
            }
        }

        Move();
    }

    private void Move()
    {
        Vector3 move = transform.forward * speed;
        rb.velocity = Vector3.Lerp(rb.velocity, move, 0.2f); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            finished = true;

            int r = Random.Range(0, 100);

            if (r > 50)
                lookRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            else
                lookRotation = Quaternion.Euler(new Vector3(0, -90, 0));

            turnSpeed = turnSpeed / 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Crash();
        }

        if (collision.gameObject.tag == "Car")
        {
            if (GameManager.instance.playerLost)
            {
                Crash();
            }
        }
    }

    private void Crash()
    {
        isStuck = true;
        rb.isKinematic = true;

        UIEmotionDisplay.instance.ShowEmotion(2, transform);
    }

    public IEnumerator SetFree()
    {
        GameManager.instance.AddCoin();
        almostFree = true;

        UIEmotionDisplay.instance.ShowEmotion(0, transform);

        yield return new WaitForSeconds(0.4f);

        isStuck = false;

        myTargetID = PathGenerator.instance.GetClosestPoint();
        target = PathGenerator.instance.pathPoints[myTargetID];
    }

    public void BecomeSad()
    {
        if (!almostFree)
            UIEmotionDisplay.instance.ShowEmotion(1, transform);
    }
}
