using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Range(2, 10)]
    [Tooltip("M : Count of ROW \nBetween 2 to 10")]
    public int rowCount;
    [Range(2, 10)]
    [Tooltip("N : Count of COLUMN \nBetween 2 to 10")]
    public int columnCount;
    [Range(1, 6)]
    [Tooltip("K : Count of COLOR \nBetween 1 to 6")]
    public int colorsCount;

    [Space]
    public int a;
    public int b;
    public int c;

    private void Awake()
    {
        Instance = this;
    }
}
