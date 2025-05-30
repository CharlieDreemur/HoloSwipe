using UnityEngine;

public class LoopMenuMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalAudio.instance.loopMenuMusic();
    }
}
