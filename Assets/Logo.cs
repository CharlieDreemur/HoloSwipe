using UnityEngine;
using System.Collections;
public class Logo : MonoBehaviour
{
    float drag = 1f;
    float vel = -10;
    float ypos = 150;

    // Update is called once per frame
    void Update()
    {
        vel -= (gameObject.transform.localPosition.y - ypos)*drag*Time.deltaTime;
        gameObject.transform.localPosition += new Vector3(0, vel, 0) * Time.deltaTime;
    }

}
