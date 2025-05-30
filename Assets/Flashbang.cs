using UnityEngine;
using UnityEngine.UI;
public class Flashbang : MonoBehaviour
{
    [SerializeField] Image flashbang;

    private void Start()
    {
        flashbang.color = new Color(255, 255, 255, 1);
    }

    // Update is called once per frame
    void Update()
    {
        flashbang.color -= new Color(0, 0,0,Time.deltaTime);
        if (flashbang.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
