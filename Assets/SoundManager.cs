using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public List<AudioSource> ambient;
    public List<AudioSource> sfx;

    private void OnEnable()
    {
        MergeItem._mergeSFX += PlayMerge;
        ForagingManager._forageSFX += PlayForaging;
        SellPotion._sellSFX += PlaySell;
    }
    private void OnDisable()
    {
        MergeItem._mergeSFX -= PlayMerge;
        ForagingManager._forageSFX -= PlayForaging;
        SellPotion._sellSFX -= PlaySell;
    }

    public void PlayForaging()
    {
        sfx[0].Play();
    }

    public void PlayMerge()
    {
        sfx[1].Play();
    }

    public void PlaySell()
    {
        sfx[2].Play();
    }


}
