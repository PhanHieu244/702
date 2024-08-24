/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 10;
    private float speed = 0;
    public float turnSpeed = 10;
    [HideInInspector]
    public bool fakeMovement = false;

    [Header("Effects")]
    public Animator anim;
    public ParticleSystem crashSmoke;
    public ParticleSystem gasSmoke;

    private Vector3 rotOffset;
    private Vector3 move;
    private Rigidbody rb;
    private bool finished = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        anim.SetFloat("Turn", Mathf.Lerp(anim.GetFloat("Turn"), CrossPlatformInputManager.GetAxis("Horizontal"), 0.1f));     
    }

    private void FixedUpdate()
    {
        if (finished)
        {
            if (speed <= 0.1f)
                rb.isKinematic = true;
            else
                speed = Mathf.Lerp(speed, 0, 0.05f);
        }
        else
            rotOffset += new Vector3(0, CrossPlatformInputManager.GetAxis("Horizontal") * turnSpeed, 0);
        rotOffset.y = Mathf.Clamp(rotOffset.y, -90, 90);

        Quaternion newRotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotOffset), 0.2f);
        rb.rotation = newRotation;

        move = transform.forward * speed;
        rb.velocity = Vector3.Lerp(rb.velocity, move, 0.3f);

        Vector3 pos = new Vector3(Mathf.Clamp(rb.position.x, -6f, 6f), rb.position.y, rb.position.z);
        rb.position = pos;

        if (fakeMovement)
        {
            rotOffset = Vector3.Lerp(rotOffset, Vector3.zero, 0.05f);
            Vector3 newPos = new Vector3(Mathf.Lerp(rb.position.x, 0, 0.05f), rb.position.y, rb.position.z);
            rb.position = newPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        {
            finished = true;
            anim.SetTrigger("Stop");
            anim.SetTrigger("Won");

            GameManager.instance.PlayerWon();
        }     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            finished = true;
            anim.SetTrigger("Stop");

            speed = 0;

            GameManager.instance.PlayerLost();

            if (crashSmoke != null)
                crashSmoke.Play();
            if (gasSmoke != null)
                gasSmoke.Stop();
        }
    }

    public void StartMoving()
    {
        finished = false;
        rb.isKinematic = false;
        speed = maxSpeed;
        anim.SetTrigger("Start");
    }

    public void ResetCar()
    {
        Vector3 pos = transform.position;
        pos.x = 0;
        pos.z += 8;
        transform.position = pos;
        transform.rotation = Quaternion.identity;

        rotOffset.y = 0;

        StartMoving();

        if (crashSmoke != null)
            crashSmoke.Stop();
        if (gasSmoke != null)
            gasSmoke.Play();
    }
}
