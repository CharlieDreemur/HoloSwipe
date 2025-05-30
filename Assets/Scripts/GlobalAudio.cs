using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalAudio : MonoBehaviour
{

    public static GlobalAudio instance;
    public float musicvol = 0.8f, sfxvol = 0.8f;

    public AudioSource source1, source2, sfxmusicsource, sfxsource; //for potential crossfade

    //source 1: Main game music
    //source 2: Main menu music, time slow music
    //sfx music source: this is specifically for the timeslow effects and such, it would sound weird if they were a diff volume than the other music, but they shoudlnt be modified by the respective sound sources
    //sfx source: global sound effects


    public AudioClip menutheme, gamemusic, timeslow, timeslowreverse, earthshake, collect, clocktick, openinventory,closeinventory, clickon, clickoff, buyitem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        
        instance = this;
        loopMenuMusic();
        DontDestroyOnLoad(gameObject);

       
    }

    public void loopGameMusic()
    {
        //start coroutine to crossfade to source 1
        source1.clip = gamemusic;
        source1.Play();
        StartCoroutine(CrossFade1());
        delayStop2();
    }

    public void loopMenuMusic()
    {
        //start coroutine to crossfade to source 2
        source2.clip = menutheme;
        source2.Play();
        StartCoroutine(CrossFade2());
        delayStop1();
    }

    public void enterTimeSlow()
    {
        //start coroutine to crossfade from source 1 to source 2
        
        source2.clip = clocktick;
        source2.Play();
        if (source1.isPlaying && source1.volume == musicvol)
        {
            sfxmusicsource.PlayOneShot(timeslow);
        }
        StartCoroutine(CrossFade2());
    }

    public void exitTimeSlow()
    {
        //start coroutine to crossfade from source 2 to source 1

        
        if (source2.isPlaying && source2.volume == musicvol)
        {
            sfxmusicsource.PlayOneShot(timeslowreverse);
        }
        source2.Stop();
        StartCoroutine(CrossFade1Delay());

    }

    public void CollectSound()
    {
        sfxsource.PlayOneShot(collect);
    }

    public void EarthShake()
    {
        sfxsource.PlayOneShot(earthshake);
    }

    public void OpenInventory()
    {
        sfxsource.PlayOneShot(openinventory);
    }

    public void CloseInventory()
    {
        sfxsource.PlayOneShot(closeinventory);
    }

    public void ClickOn()
    {
        sfxsource.PlayOneShot(clickon);
    }

    public void ClickOff()
    {
        sfxsource.PlayOneShot(clickoff);
    }

    public void BuyItem()
    {
        sfxsource.PlayOneShot(buyitem);
    }

    IEnumerator delayStop1()
    {
        yield return (new WaitForSeconds(1f));
        source1.Stop();
    }
    IEnumerator delayStop2()
    {
        yield return (new WaitForSeconds(1f));
        source2.Stop();
    }




    IEnumerator CrossFade2() //crossfades out from source1 to source 2
    {
        source2.volume = 0;
        source1.volume = musicvol;

        for (float i = 0; i < 1; i+= 0.1f)
        {
            source1.volume -= 0.1f * musicvol;
            source2.volume += 0.1f * musicvol;
            yield return (new WaitForSeconds(0.1f));
        }
        source1.volume = 0;
        
        source2.volume =musicvol;
        
    }
    IEnumerator CrossFade1() //crossfades out from source2 to source 1
    {
        source1.volume = 0;
        source2.volume = musicvol;
        for (float i = 0; i < 1; i += 0.1f)
        {
            source1.volume += 0.1f * musicvol;
            source2.volume -= 0.1f * musicvol;
            yield return (new WaitForSeconds(0.1f));
        }
        source2.volume = 0;
        source1.volume = musicvol;
    }

    IEnumerator CrossFade1Delay() //crossfades out from source2 to source 1 delayed by 0.5 s
    {
        yield return (new WaitForSeconds(1f));
        for (float i = 0; i < 1; i += 0.1f)
        {
            source1.volume += 0.1f * musicvol;
            source2.volume -= 0.1f * musicvol;
            yield return (new WaitForSeconds(0.1f));
        }
        source2.volume = 0;
        source1.volume = musicvol;
    }


}
