using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraOcclusionMaterialSwap : MonoBehaviour
{
    public Transform target;
    [Range(0.1f, 5f)]
    public float checkRadius = 0.5f; 
    [Range(0.1f, 10f)]
    public float maxDistance = 5f;
    public LayerMask occlusionLayers;
    public Material transparentMaterial;
    [Range(0.1f, 1f)]
    public float checkInterval = 0.2f;

    public bool showDebugRay = true;

    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> currentRenderers = new List<Renderer>();
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            PerformOcclusionCheck();
            timer = 0;
        }
        if (showDebugRay)
        {
            Debug.DrawRay(transform.position, (target.position - transform.position).normalized * maxDistance,
                        Color.cyan);
        }
    }

    void PerformOcclusionCheck()
    {
        CleanupDestroyedRenderers();
        Vector3 targetPosition = target.position + Vector3.up * 1.0f;

        Vector3 origin = transform.position;
        Vector3 direction = (targetPosition - origin).normalized;
        float maxDistance = Vector3.Distance(origin, targetPosition);

        RaycastHit[] hits = Physics.CapsuleCastAll(
            origin,
            origin + direction * 0.1f, 
            checkRadius,
            direction,
            maxDistance,
            occlusionLayers
        );


        var validHits = hits.Where(hit =>
            Vector3.Dot(direction, (hit.point - origin).normalized) > 0.98f
        ).ToList();

        var newRenderers = new List<Renderer>();
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<Renderer>(out var renderer))
            {
                newRenderers.Add(renderer);
            }
        }

        foreach (var renderer in currentRenderers)
        {
            if (!newRenderers.Contains(renderer))
            {
                RestoreOriginalMaterials(renderer);
            }
        }

        foreach (var renderer in newRenderers)
        {
            if (!originalMaterials.ContainsKey(renderer))
            {
                BackupAndReplaceMaterials(renderer);
            }
        }

        currentRenderers = newRenderers;

    }
    void CleanupDestroyedRenderers()
    {
        var invalidRenderers = new List<Renderer>();

        foreach (var kvp in originalMaterials)
        {
            if (kvp.Key == null || kvp.Key.gameObject == null)
            {
                invalidRenderers.Add(kvp.Key);
            }
        }

        foreach (var renderer in invalidRenderers)
        {
            originalMaterials.Remove(renderer);
            currentRenderers.Remove(renderer);
        }
    }


    void BackupAndReplaceMaterials(Renderer renderer)
    {
        originalMaterials[renderer] = renderer.sharedMaterials;
        var newMaterials = new Material[renderer.sharedMaterials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = transparentMaterial;
        }
        renderer.sharedMaterials = newMaterials;
    }

    void RestoreOriginalMaterials(Renderer renderer)
    {
        if (renderer == null || renderer.gameObject == null) return;
        
        if (originalMaterials.TryGetValue(renderer, out var mats))
        {
            try
            {
                renderer.sharedMaterials = mats;
            }
            catch (MissingReferenceException)
            {
                originalMaterials.Remove(renderer);
            }
            originalMaterials.Remove(renderer);
        }
    }

    void OnDisable()
    {
        var validRenderers = new List<Renderer>(originalMaterials.Keys);
        foreach (var renderer in validRenderers)
        {
            if (renderer != null && renderer.gameObject != null)
            {
                RestoreOriginalMaterials(renderer);
            }
        }
        originalMaterials.Clear();
    }

    void OnDrawGizmos()
    {
        if (target && showDebugRay)
        {
            Gizmos.color = Color.cyan;
            Vector3 targetPos = target.position + Vector3.up * 1.0f;
            Gizmos.DrawLine(transform.position, targetPos);

            Vector3 direction = (targetPos - transform.position).normalized;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
            Gizmos.DrawWireSphere(transform.position + direction * maxDistance, checkRadius);
        }
    }
}