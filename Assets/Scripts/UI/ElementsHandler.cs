using UnityEngine.UI;
using UnityEngine;

public class ElementsHandler : MonoBehaviour {
    [SerializeField]
    Text attempts;
    [SerializeField]
    Text levelCount;

    void Update()
    {
        attempts.text = "x" + GameProcessHandler.Attempts;
        levelCount.text = "Level: " + (GameProcessHandler.CountOfWonRounds + 1);
    }
}
