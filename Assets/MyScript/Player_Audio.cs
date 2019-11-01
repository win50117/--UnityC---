using UnityEngine;
using System.Collections;

public class Player_Audio : MonoBehaviour 
{
    public AudioClip Attack;
    public AudioClip Skill_1;
    public AudioClip Skill_2;
    public AudioClip Skill_3;
    public AudioClip Skill_4;

    public void Audio_Attack()
    {
        audio.PlayOneShot(Attack, 1.0f); //播放剪輯音緣,音量為0~1     
    }
    public void Audio_Skill_1()
    {
        audio.PlayOneShot(Skill_1, 1.0f);
    }
    public void Audio_Skill_2()
    {
        audio.PlayOneShot(Skill_2, 1.0f);
    }
    public void Audio_Skill_3()
    {
        audio.PlayOneShot(Skill_3, 1.0f);
    }
    public void Audio_Skill_4()
    {
        audio.PlayOneShot(Skill_4, 1.0f);
    }
}
