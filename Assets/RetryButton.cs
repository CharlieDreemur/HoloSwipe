using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void RetryGame() 
    {
        Destroy( GameManager.instance);
        SceneManager.LoadScene("MainMenu");
    }
}
