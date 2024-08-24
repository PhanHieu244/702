/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("SFX Settings")]
    public AudioSource SFX;
    public AudioClip[] hitSounds;
    public AudioClip crashSound, coinPickUpSound, winSound, loseSound, clickSound, changeSound, spendSound;

    [Header("Music Settings")]
    public AudioSource music;
    public AudioClip menu, game;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateSettings();
    }

    public void Hit()
    {
        int r = Random.Range(0, hitSounds.Length);

        SFX.PlayOneShot(hitSounds[r]);
    }

    public void Crash()
    {
        SFX.PlayOneShot(crashSound);
    }

    public void CoinPickUp()
    {
        SFX.PlayOneShot(coinPickUpSound);
    }

    public void MenuMusic()
    {
        if (music.clip == menu && music.isPlaying)
            return;

        music.Stop();

        music.clip = menu;

        music.Play();
    }

    public void GameMusic()
    {
        if (music.clip == game && music.isPlaying)
            return;

        music.Stop();

        music.clip = game;

        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void Win()
    {
        SFX.PlayOneShot(winSound);
    }

    public void Lost()
    {
        SFX.PlayOneShot(loseSound);
    }

    public void Click()
    {
        SFX.PlayOneShot(clickSound);
    }

    public void Change()
    {
        SFX.PlayOneShot(changeSound);
    }

    public void Spend()
    {
        SFX.PlayOneShot(spendSound);
    }

    public void UpdateSettings()
    {
        if (PlayerPrefs.GetInt("SFX", 1) == 0)
            SFX.volume = 0;
        else
            SFX.volume = 1;

        if (PlayerPrefs.GetInt("Music", 1) == 0)
            music.volume = 0;
        else
            music.volume = 0.7f;
    }
}
