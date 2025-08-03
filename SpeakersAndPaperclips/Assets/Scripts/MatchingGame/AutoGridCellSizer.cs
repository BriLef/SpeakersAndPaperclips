using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(GridLayoutGroup))]
public class AutoGridCellSizer : MonoBehaviour
{
    public enum FitMode { FixedColumns, FixedRows, BestSquareFit }

    [Header("Fit Settings")]
    public FitMode fitMode = FitMode.BestSquareFit;
    [Min(1)] public int fixedColumns = 5; // Used when FitMode = FixedColumns
    [Min(1)] public int fixedRows = 6;    // Used when FitMode = FixedRows
    public Vector2 minCellSize = new Vector2(10, 10); // Optional floor
    public bool keepSquareCells = true;

    private GridLayoutGroup _grid;
    private RectTransform _rect;

    void OnEnable()
    {
        _grid = GetComponent<GridLayoutGroup>();
        _rect = GetComponent<RectTransform>();
        UpdateCellSize();
    }

    void OnRectTransformDimensionsChange()
    {
        if (!isActiveAndEnabled) return;
        if (_grid == null) _grid = GetComponent<GridLayoutGroup>();
        if (_rect == null) _rect = GetComponent<RectTransform>();
        UpdateCellSize();
    }

    void Update()
    {
        // Keep editor preview responsive
        #if UNITY_EDITOR
        if (!Application.isPlaying) UpdateCellSize();
        #endif
    }

    void UpdateCellSize()
    {
        if (_grid == null || _rect == null) return;

        int childCount = transform.childCount;
        if (childCount == 0) return;

        var padding = _grid.padding;
        var spacing = _grid.spacing;

        float totalW = _rect.rect.width - padding.left - padding.right;
        float totalH = _rect.rect.height - padding.top - padding.bottom;

        int cols, rows;

        switch (fitMode)
        {
            case FitMode.FixedColumns:
                cols = Mathf.Max(1, fixedColumns);
                rows = Mathf.CeilToInt(childCount / (float)cols);
                break;

            case FitMode.FixedRows:
                rows = Mathf.Max(1, fixedRows);
                cols = Mathf.CeilToInt(childCount / (float)rows);
                break;

            default: // BestSquareFit
                // Try to make a near-square grid based on child count
                cols = Mathf.CeilToInt(Mathf.Sqrt(childCount));
                rows = Mathf.CeilToInt(childCount / (float)cols);
                break;
        }

        cols = Mathf.Max(1, cols);
        rows = Mathf.Max(1, rows);

        float cellW = (totalW - (cols - 1) * spacing.x) / cols;
        float cellH = (totalH - (rows - 1) * spacing.y) / rows;

        if (keepSquareCells)
        {
            float side = Mathf.Floor(Mathf.Min(cellW, cellH));
            cellW = cellH = side;
        }

        cellW = Mathf.Max(cellW, minCellSize.x);
        cellH = Mathf.Max(cellH, minCellSize.y);

        _grid.cellSize = new Vector2(cellW, cellH);
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = cols; // With cellSize set, this keeps layout stable
    }
}
