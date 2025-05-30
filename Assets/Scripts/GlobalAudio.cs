using UnityEngine;
using System.Collections;


public class GlobalAudio : MonoBehaviour
{

    public static GlobalAudio instance;
    public float musicvol = 1, sfxvol = 1;

    public AudioSource source1, source2, sfxsource; //for potential crossfade

    //source 1: Main game music
    //source 2: Main menu music, time slow music
    //sfx source: global sound effects

    public AudioClip menutheme, gamemusic, timeslow, timeslowreverse, earthshake, collect, clocktick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void loopGameMusic()
    {
        //start coroutine to crossfade to source 1
        source1.clip = gamemusic;
        StartCoroutine(CrossFade1());
    }

    public void loopMenuMusic()
    {
        //start coroutine to crossfade to source 2
        source2.clip = menutheme;
        StartCoroutine(CrossFade2());
    }

    public void enterTimeSlow()
    {
        //start coroutine to crossfade from source 1 to source 2
        source2.clip = clocktick;
        source2.PlayOneShot(timeslow);
        StartCoroutine(CrossFade2());
    }

    public void exitTimeSlow()
    {
        //start coroutine to crossfade from source 2 to source 1
        source2.PlayOneShot(timeslowreverse);
        StartCoroutine(CrossFade1());

    }

    public void CollectSound()
    {
        sfxsource.PlayOneShot(collect);
    }

    public void EarthShake()
    {
        sfxsource.PlayOneShot(earthshake);
    }


    IEnumerator CrossFade2() //crossfades out from source1 to source 2
    {
        source2.Play();
        for (float i = 0; i < 1; i+= 0.1f)
        {
            source1.volume -= 0.1f * musicvol;
            source2.volume += 0.1f * musicvol;
            yield return (new WaitForSeconds(0.1f));
        }
        source1.volume = 0;
        source1.Stop();
        source2.volume =musicvol;
        
    }
    IEnumerator CrossFade1() //crossfades out from source2 to source 1
    {
        source1.Play();
        for (float i = 0; i < 1; i += 0.1f)
        {
            source1.volume += 0.1f * musicvol;
            source2.volume -= 0.1f * musicvol;
            yield return (new WaitForSeconds(0.1f));
        }
        source2.volume = 0;
        source1.volume = musicvol;
        source2.Stop();
    }


}
