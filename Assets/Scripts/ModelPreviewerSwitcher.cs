using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ModelPreviewSwitcher : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float switchInterval = 0.5f;
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private float targetSize = 2f; // Desired size for all models
    [SerializeField] private Vector3 scaleFrom = Vector3.zero;
    
    [Header("References")]
    [SerializeField] private MerchCollcetion merchPoolCollection;
    [SerializeField] private Transform modelContainer;
    
    private List<GameObject> modelInstances = new List<GameObject>();
    private List<Vector3> originalScales = new List<Vector3>();
    private List<float> sizeMultipliers = new List<float>();
    private int currentIndex = 0;
    private GameObject currentModel;

    private void Awake()
    {
        // Create container if none exists
        if (modelContainer == null)
        {
            modelContainer = new GameObject("Model Container").transform;
            modelContainer.SetParent(transform);
            modelContainer.localPosition = Vector3.zero;
        }
        
        // Instantiate all models and calculate their size multipliers
        foreach (var merchSO in merchPoolCollection.merches)
        {
            if (merchSO.mesh != null)
            {
                var instance = Instantiate(merchSO.mesh, modelContainer);
                instance.SetActive(false);
                
                // Calculate the model's bounds
                Bounds bounds = CalculateBounds(instance);
                float modelSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
                
                // Store original scale and calculate multiplier
                originalScales.Add(instance.transform.localScale);
                sizeMultipliers.Add(targetSize / modelSize);
                
                modelInstances.Add(instance);
            }
        }
    }

    private Bounds CalculateBounds(GameObject obj)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(obj.transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }

    private void Start()
    {
        if (modelInstances.Count == 0)
        {
            Debug.LogWarning("No valid models found in the merch collection.");
            return;
        }

        ShowModel(0);
        InvokeRepeating(nameof(NextModel), switchInterval, switchInterval);
    }

    private void NextModel()
    {
        currentIndex = (currentIndex + 1) % modelInstances.Count;
        ShowModel(currentIndex);
    }

    private void ShowModel(int index)
    {
        // Hide current model with animation
        if (currentModel != null)
        {
            currentModel.transform.DOScale(scaleFrom, transitionDuration)
                .OnComplete(() => currentModel.SetActive(false));
        }

        // Show new model with animation
        currentModel = modelInstances[index];
        currentModel.SetActive(true);
        
        // Apply normalized scale
        Vector3 normalizedScale = originalScales[index] * sizeMultipliers[index];
        currentModel.transform.localScale = scaleFrom;
        currentModel.transform.DOScale(normalizedScale, transitionDuration);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
        foreach (var model in modelInstances)
        {
            if (model != null) Destroy(model);
        }
    }
}