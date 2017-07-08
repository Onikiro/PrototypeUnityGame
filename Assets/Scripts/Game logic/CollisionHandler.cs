using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обработчик коллизий игрового поля.
/// </summary>
public class CollisionHandler : MonoBehaviour {

    /// <summary>
    /// Камера, с которой будет начинаться raycast
    /// </summary>
    public Camera cam;

    /// <summary>
    /// Путь, который проходит игрок.
    /// </summary>
    public List<Dot> realisedPath = new List<Dot>();

    /// <summary>
    /// Правильный путь, сгенерированный в Map.
    /// </summary>
    public List<Dot> truePath = new List<Dot>();

    /// <summary>
    /// Обрабатывает столкновения луча с точками на поле.
    /// </summary>
    public void RaycastCollide()
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString("#EFFF5CFF", out color); //Светло-желтый цвет. Здесь по желанию его можно изменить. Немного magic

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if (!realisedPath.Contains(hit.collider.gameObject.GetComponent<Dot>()))
            {
                realisedPath.Add(hit.collider.gameObject.GetComponent<Dot>());
            }
            hit.collider.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
