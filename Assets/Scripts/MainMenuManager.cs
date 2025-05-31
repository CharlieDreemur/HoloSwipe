using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string GameScene;


    public void StartGame() 
    {
        SceneManager.LoadScene(GameScene);
    }


    public void Start()
    {
        if (GlobalAudio.instance != null)
        {
            if (!(GlobalAudio.instance.source2.isPlaying))
            {
                GlobalAudio.instance.loopMenuMusic();
            }
        }
        
        
    }
}
