using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Warning : MonoBehaviour
{
    [SerializeField] CanvasRenderer nam, desc;
    [SerializeField] float duration;
    //[SerializeField] float fadeTime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        nam.SetAlpha(1);
        desc.SetAlpha(1);
        
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return (new WaitForSeconds(duration));

        for (float alpha = 1f; alpha >= 0; alpha -= 0.05f)
        {
            nam.SetAlpha(alpha);
            desc.SetAlpha(alpha);
            yield return new WaitForSeconds(.05f);
        }
        gameObject.SetActive(false);
    }

}
