using System.Collections;
using UnityEngine;

/// <summary>
/// Класс отрисовки событий на игровом поле.
/// </summary>
public class DisplayField : MonoBehaviour {

    public bool IsDisplayFinished { get; set; }

    /// <summary>
    /// Корутина отрисовки пути на игровом поле.
    /// </summary>
    /// <param name="field">Экземпляр класса Map</param>
    /// <param name="seconds">Задержка между сменой цвета точек.</param>
    /// <returns></returns>
    public IEnumerator DisplayPath(Map field, float seconds)
    {
        IsDisplayFinished = false;
        int i = 2;

        while(true)
        {
            field.Path[i - 2].gameObject.GetComponent<Renderer>().material.color = Color.blue;
            yield return new WaitForSeconds(seconds);

            field.Path[i - 1].gameObject.GetComponent<Renderer>().material.color = Color.blue;
            yield return new WaitForSeconds(seconds);

            field.Path[i - 2].gameObject.GetComponent<Renderer>().material.color = Color.white;

            if(i >= field.Path.Count)
            {
                yield return new WaitForSeconds(seconds);

                field.Path[i - 1].gameObject.GetComponent<Renderer>().material.color = Color.white;

                IsDisplayFinished = true;
                yield break;
            }
            i++;
        }
    }
}
