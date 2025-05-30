using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene("PreCon");
    }

    public void Start()
    {
        if (!(GlobalAudio.instance.source2.isPlaying))
        {
            GlobalAudio.instance.loopMenuMusic();
        }
        
    }
}
