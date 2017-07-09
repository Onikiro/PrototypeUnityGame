using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Обработчик игрового процесса
/// </summary>
public class GameProcessHandler : MonoBehaviour {

    //Экземпляры классов для работы с игровой логикой
    [SerializeField]
    Map mapField;
    [SerializeField]
    DisplayField display;
    [SerializeField]
    CollisionHandler collisions;

    /// <summary>
    /// Счетчик пройденных уровней
    /// </summary>
    public static int CountOfWonRounds { get; private set; }
    /// <summary>
    /// Булевая переменная, начался ли новый уровень
    /// </summary>
    bool isRoundStarted = false;
    /// <summary>
    /// Булевая переменная, пройден ли текущий уровень
    /// </summary>
    bool isRoundComplete = false;
    /// <summary>
    /// Булевая переменная, сгенерировался ли новый уровень
    /// </summary>
    bool changes = false;

    /// <summary>
    /// Количество точек в пути
    /// </summary>
    int countOfDotsInPath;
    /// <summary>
    /// Время задержки между появлением точек на экране
    /// </summary>
    float secondsDelay;
    /// <summary>
    /// Количество прав на ошибку игрока.
    /// </summary>
    public static int Attempts { get; private set; }


	void FixedUpdate () {
		if(!isRoundStarted)
        {
            if (!changes)
            {
                ChangeComplexity();
                RechangePaths();
                StartCoroutine(display.DisplayPath(mapField, secondsDelay)); 
                changes = true;
            }
            if (display.IsDisplayFinished)
            {
                isRoundStarted = true;
            }
        }
        if (isRoundStarted)
        {
            if (Input.GetMouseButton(0))
            {
                if (collisions.realisedPath.Count < collisions.truePath.Count)
                {
                    collisions.RaycastCollide();
                }
                else
                {
                    EndRound();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                ClearLastCollisions(collisions.realisedPath);
            }
        }
	}

    /// <summary>
    /// Проверяет, правильный ли путь выбрал игрок и оканчивает игру в зависимости от результата
    /// </summary>
    void EndRound()
    {
        if (collisions.truePath.SequenceEqual(collisions.realisedPath))
        {
            isRoundComplete = true;
            isRoundStarted = false;
            changes = false;
            CountOfWonRounds++;
            return;
        }
        else
        {
            isRoundComplete = false;
            isRoundStarted = false;
            changes = false;
            return;
        }
    }

    /// <summary>
    /// Очищает все задействованные в уровне пути и генерирует новые.
    /// </summary>
    void RechangePaths()
    {
        ClearLastCollisions(mapField.Path);
        ClearLastCollisions(collisions.truePath);
        ClearLastCollisions(collisions.realisedPath);

        mapField.ChangePath(countOfDotsInPath);

        collisions.truePath = mapField.Path;
    }

    /// <summary>
    /// Меняет цвет каждой точки в коллекции на стандартный цвет всех точек(белый) и удаляет коллекцию.
    /// </summary>
    /// <param name="items">Коллекция точек</param>
    void ClearLastCollisions(List<Dot> items)
    {
        foreach (var item in items)
        {
            item.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        items.Clear();
    }

    /// <summary>
    /// Увеличивает сложность игры.
    /// </summary>
    void IncreaseComplexity()
    {
        if (CountOfWonRounds % 2 == 0)
        {
            countOfDotsInPath++;

            //Проверяем скорость демонстрации пути, настраиваем ограничения
            if (secondsDelay >= 0.3)
            {
                secondsDelay -= 0.1f;
            }
            else if(secondsDelay >= 0.2)
            {
                secondsDelay -= 0.05f;
            }
            else
            {
                secondsDelay = 0.1f;
            }

        }
    }

    /// <summary>
    /// Сбрасывает сложность игры на начальный уровень.
    /// </summary>
    void ResetComplexity()
    {
        //Стандартные значения для начала игры
        CountOfWonRounds = 0;
        countOfDotsInPath = 3;
        secondsDelay = 0.5f;
        Attempts = 3;
    }

    /// <summary>
    /// Настраивает сложность игры.
    /// </summary>
    void ChangeComplexity()
    {
        if (isRoundComplete)
        {
            IncreaseComplexity();
        }
        else if (!isRoundComplete && Attempts > 1)
        {
            Attempts--;
        }
        else
        {
            ResetComplexity();
        }
    }
}
