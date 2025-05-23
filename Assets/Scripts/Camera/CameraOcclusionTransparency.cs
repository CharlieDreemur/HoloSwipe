using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraOcclusionMaterialSwap : MonoBehaviour
{
    [Header("遮挡检测设置")]
    public Transform target;
    [Range(0.1f, 5f)]
    public float checkRadius = 0.5f;  // 新增检测半径参数
    [Range(0.1f, 10f)]
    public float maxDistance = 5f;
    public LayerMask occlusionLayers;

    [Header("材质设置")]
    public Material transparentMaterial;
    [Range(0.1f, 1f)]
    public float checkInterval = 0.2f;

    [Header("调试设置")]
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

        // 调试显示
        if (showDebugRay)
        {
            Debug.DrawRay(transform.position, (target.position - transform.position).normalized * maxDistance,
                        Color.cyan);
        }
    }

    void PerformOcclusionCheck()
    {
        CleanupDestroyedRenderers();
        // 计算目标点（角色中心向上偏移）
        Vector3 targetPosition = target.position + Vector3.up * 1.0f; // 假设角色高度2米

        // 计算检测参数
        Vector3 origin = transform.position;
        Vector3 direction = (targetPosition - origin).normalized;
        float maxDistance = Vector3.Distance(origin, targetPosition);

        // 使用精确的胶囊体检测
        RaycastHit[] hits = Physics.CapsuleCastAll(
            origin,
            origin + direction * 0.1f, // 微小偏移形成有效胶囊体
            checkRadius,
            direction,
            maxDistance,
            occlusionLayers
        );

        // 筛选有效遮挡物
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

        // 恢复不再被遮挡的物体
        foreach (var renderer in currentRenderers)
        {
            if (!newRenderers.Contains(renderer))
            {
                RestoreOriginalMaterials(renderer);
            }
        }

        // 处理新遮挡物
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
            // 绘制精确检测区域
            Gizmos.color = Color.cyan;
            Vector3 targetPos = target.position + Vector3.up * 1.0f;
            Gizmos.DrawLine(transform.position, targetPos);

            // 绘制检测胶囊体
            Vector3 direction = (targetPos - transform.position).normalized;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
            Gizmos.DrawWireSphere(transform.position + direction * maxDistance, checkRadius);
        }
    }
}