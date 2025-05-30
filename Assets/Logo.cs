using UnityEngine;

public class Logo : MonoBehaviour
{
    float drag = 0.2f;
    float vel = -10;
    float ypos = 150;
    bool shaking = false;
    float nextShake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        nextShake = Random.Range(5, 20);
    }
    // Update is called once per frame
    void Update()
    {
        vel -= (gameObject.transform.localPosition.y - ypos)*Time.deltaTime;
        gameObject.transform.localPosition += new Vector3(0, vel, 0) * Time.deltaTime;
    }
}
