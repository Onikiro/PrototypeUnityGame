using UnityEngine;

/// <summary>
/// Класс с информацией о точке.
/// </summary>
public class Dot : MonoBehaviour {

    /// <summary>
    /// Позиция в ряду массива.
    /// </summary>
    public int RowPosition { get; set; }

    /// <summary>
    /// Позиция в столбце массива.
    /// </summary>
    public int ColPosition { get; set; }

    /// <summary>
    /// Булевая отвечающая, использована ли точка в построении пути.
    /// </summary>
    public bool IsChosen = false;
}
