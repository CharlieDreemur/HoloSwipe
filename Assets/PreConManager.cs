using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreConManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText, neededValue;

    [SerializeField] string GameScene;

    private void Start()
    {
        if(GameManager.instance == null) 
        {
            return;
        }

        dayText.text = "Day : " + GameManager.instance.day;
        neededValue.text = "Needed value = " + GameManager.instance.neededValue;
    }

    public void GoToCon() 
    {
        SceneManager.LoadScene(GameScene);
    }
}
