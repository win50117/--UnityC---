using UnityEngine;
using System.Collections;

public class Dragon_Audio : MonoBehaviour  //
{
    public AudioClip NearAttack;
    public AudioClip FireBall;
    public AudioClip Roar;
    public AudioClip FlyIng;
    public AudioClip AngryMode;
    public AudioClip AreaFire;
    public AudioClip CrazyMode;
    public AudioClip RayShoot;

    public void Audio_NearAttack()
    {
        audio.PlayOneShot(NearAttack, 0.8f); //播放剪輯音緣,音量為0~1     
    }

    public void Audio_FireBall()
    {
        audio.PlayOneShot(FireBall, 0.7f); 
    }

    public void Audio_Roar()
    {
        audio.PlayOneShot(Roar, 1.0f);
    }

    public void Audio_FlyIng()
    {
        audio.PlayOneShot(FlyIng, 0.5f);        
    }

    public void Audio_AngryMode()
    {
        audio.PlayOneShot(AngryMode, 0.7f);
    }

    public void Audio_AreaFire()
    {
        audio.PlayOneShot(AreaFire, 0.9f); 
    }

    public void Audio_CrazyMode()
    {
        audio.PlayOneShot(CrazyMode, 0.7f);
    }

    public void Audio_RayShoot()
    {
        audio.PlayOneShot(RayShoot, 0.8f);
    }
}
