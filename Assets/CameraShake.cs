using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Vector3 position;
    public float shake = 0;
    public float shakeAmount;
    public float decreaseFactor;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = position;
        if (shake > 0)
        {
            gameObject.transform.localPosition += Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0;
        }
    }
}
