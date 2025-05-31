using UnityEngine;

public class LoopGameMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalAudio.instance.loopGameMusic();
    }

}
