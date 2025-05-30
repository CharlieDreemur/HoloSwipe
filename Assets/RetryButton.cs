using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void RetryGame() 
    {
        Destroy( GameManager.instance);
        PlayerStatManager.ResetStats();
        SceneManager.LoadScene("MainMenu");
    }
}
