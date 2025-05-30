using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanv : MonoBehaviour
{

    [SerializeField] GameObject settingsMenu, playMenu;
    [SerializeField] Slider music, sfx;

    private void Start()
    {
        if (GlobalAudio.instance != null) {
            music.value = GlobalAudio.instance.musicvol;
            sfx.value = GlobalAudio.instance.sfxvol;
        }

        
    }

    public void setMusicVol()
    {
        GlobalAudio.instance.musicvol = music.value;
        if (GlobalAudio.instance.source1.volume != 0)
        {
            GlobalAudio.instance.source1.volume = music.value;
        }
        if (GlobalAudio.instance.source2.volume != 0)
        {
            GlobalAudio.instance.source2.volume = music.value;
        }

        if (GlobalAudio.instance.sfxmusicsource.volume != 0)
        {
            GlobalAudio.instance.sfxmusicsource.volume = music.value;
        }

    }

    public void setSFXvol()
    {
        GlobalAudio.instance.sfxvol = sfx.value;
        GlobalAudio.instance.sfxsource.volume = sfx.value;
    }

    public void GoSettings()
    {
        settingsMenu.SetActive(true);
        playMenu.SetActive(false);
    }
    public void GoPlay()
    {
        settingsMenu.SetActive(false);
        playMenu.SetActive(true);
    }
}
