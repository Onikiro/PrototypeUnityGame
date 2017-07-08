using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для работы с игровым полем.
/// </summary>
public class Map : MonoBehaviour {
    const byte ROWS = 3;
    const byte COLS = 3;

    Dot[,] dotMap = new Dot[ROWS, COLS];
    List<Dot> path = new List<Dot>();

    public List<Dot> Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }

    void Awake()
    {
        GetDots();
    }

    /// <summary>
    /// Получает двумерный массив, заполненный точками.
    /// </summary>
    void GetDots()
    {
        int count = 1;
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                dotMap[i, j] = GameObject.Find("Dot" + count).GetComponent<Dot>();
                dotMap[i, j].RowPosition = i;
                dotMap[i, j].ColPosition = j;
                dotMap[i, j].IsChosen = false;
                count++;
            }
        }
    }

    /// <summary>
    /// Получает соседние точки указанной входной точки.
    /// </summary>
    /// <param name="dot">Входная точка, соседей которой нужно получить.</param>
    /// <returns>Возвращает массив типа List со всеми соседями.</returns>
    List<Dot> GetNiegbours(Dot dot)
    {
        List<Dot> dotsNeigbours = new List<Dot>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if((dot.RowPosition + i) >= 0 && (dot.ColPosition + j) >= 0 && (dot.RowPosition + i) < ROWS && (dot.ColPosition + j) < COLS)
                {
                    Dot currentDot = dotMap[dot.RowPosition + i, dot.ColPosition + j];
                    if (!currentDot.IsChosen)
                    {
                        dotsNeigbours.Add(currentDot);
                    }
                }
            }
        }
        return dotsNeigbours;
    }

    /// <summary>
    /// Генерирует случайный путь по двумерному массиву точек.
    /// </summary>
    /// <param name="count">Количество точек, по которым необходимо построить путь.</param>
    /// <returns>Возвращает массив типа List с точками маршрута.</returns>
    public void ChangePath(int count)
    {
        foreach (var item in dotMap)
        {
            item.IsChosen = false;
        }

        List<Dot> path = new List<Dot>();
        List<Dot> neigbours = new List<Dot>(); 

        Dot currentDot = dotMap[Random.Range(0, ROWS), Random.Range(0, COLS)]; //Выбираем случайную стартовую позицию

        path.Add(currentDot); 
        currentDot.IsChosen = true;

        while (path.Count < count) 
        {
            neigbours = GetNiegbours(currentDot);


            /* Если последнюю точку не окружают неиспользованные точки, 
             * будет возвращен пустой массив, 
             * а значит необходимо завершить цикл */
            if (neigbours.Count == 0) 
            {
                break;
            }

            currentDot = neigbours[Random.Range(0, neigbours.Count - 1)];
            if (!currentDot.IsChosen)
            {
                path.Add(currentDot);
                currentDot.IsChosen = true;
            }

            //Если все точки вокруг использованы (IsChosen), то выходим из цикла
            else
            {
                break;
            }
        }
        this.path = path;
    }
}
