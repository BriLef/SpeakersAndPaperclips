using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridResizer : MonoBehaviour
{
    [Header("Target Area")]
    [Tooltip("RectTransform to fit the grid content into. If null, uses the grid's parent RectTransform.")]
    [SerializeField] private RectTransform fitTarget;

    [Header("Options")]
    [Tooltip("Clamp the computed scale (0 disables clamping).")]
    [SerializeField] private float minScale = 0f;
    [SerializeField] private float maxScale = 0f;

    [Tooltip("Recompute every frame (handy for responsive UIs). You can switch this off and call Refresh() manually.")]
    [SerializeField] private bool liveUpdate = true;

    private GridLayoutGroup _grid;
    private RectTransform _gridRect;
    private Vector2 _cell0, _spacing0;
    private RectOffset _padding0;

    private void Awake()
    {
        _grid = GetComponent<GridLayoutGroup>();
        _gridRect = GetComponent<RectTransform>();
        CacheInitialMetrics();
        EnsureFitTarget();
    }

    private void OnValidate()
    {
        if (_grid == null) _grid = GetComponent<GridLayoutGroup>();
        if (_gridRect == null) _gridRect = GetComponent<RectTransform>();
        EnsureFitTarget();
    }

    private void Start()
    {
        // In case values were changed at runtime before Start
        CacheInitialMetrics();
        Refresh();
    }

    private void Update()
    {
        if (liveUpdate) Refresh();
    }

    public void Refresh()
    {
        if (_grid == null || _gridRect == null) return;
        EnsureFitTarget();
        if (fitTarget == null) return;

        // Determine rows/cols based on constraint and current child count
        int childCount = Mathf.Max(1, transform.childCount);
        int rows, cols;
        switch (_grid.constraint)
        {
            case GridLayoutGroup.Constraint.FixedRowCount:
                rows = Mathf.Max(1, _grid.constraintCount);
                cols = Mathf.CeilToInt(childCount / (float)rows);
                break;

            case GridLayoutGroup.Constraint.FixedColumnCount:
                cols = Mathf.Max(1, _grid.constraintCount);
                rows = Mathf.CeilToInt(childCount / (float)cols);
                break;

            default: // Flexible: approximate a square
                cols = Mathf.CeilToInt(Mathf.Sqrt(childCount));
                rows = Mathf.CeilToInt(childCount / (float)cols);
                break;
        }

        // Content size in canvas units using initial metrics (so scale is stable)
        float contentW = cols * _cell0.x + (cols - 1) * _spacing0.x + _padding0.left + _padding0.right;
        float contentH = rows * _cell0.y + (rows - 1) * _spacing0.y + _padding0.top  + _padding0.bottom;
        if (contentW <= 0f || contentH <= 0f) return;

        // Target rect size (canvas units)
        Vector2 targetSize = fitTarget.rect.size;
        float targetW = Mathf.Max(1f, targetSize.x);
        float targetH = Mathf.Max(1f, targetSize.y);

        // Uniform scale to fit both dimensions
        float scaleX = targetW / contentW;
        float scaleY = targetH / contentH;
        float scale  = Mathf.Min(scaleX, scaleY);

        if (minScale > 0f) scale = Mathf.Max(minScale, scale);
        if (maxScale > 0f) scale = Mathf.Min(maxScale, scale);

        _grid.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private void CacheInitialMetrics()
    {
        if (_grid == null) return;
        _cell0    = _grid.cellSize;
        _spacing0 = _grid.spacing;
        _padding0 = new RectOffset(
            _grid.padding.left, _grid.padding.right,
            _grid.padding.top,  _grid.padding.bottom
        );
    }

    private void EnsureFitTarget()
    {
        if (fitTarget == null && _gridRect != null && _gridRect.parent is RectTransform p)
            fitTarget = p; // default to parent area
    }
}
