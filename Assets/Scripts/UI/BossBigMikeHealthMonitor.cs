using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class BossBigMikeHealthMonitor : MonoBehaviour
{
    public RectTransform healthBarMiddle;
    public RectTransform healthBarBack;

    void Start()
    {

    }

    public void MinusHealth() {
        if (healthBarBack.anchoredPosition.x > 10) {
            healthBarMiddle.sizeDelta = new Vector2(healthBarMiddle.sizeDelta.x - 1.8f, healthBarMiddle.sizeDelta.y);
            healthBarBack.anchoredPosition = new Vector2(healthBarBack.anchoredPosition.x - 1.8f, healthBarBack.anchoredPosition.y);
        }
    }

    public void AddHealth2() {
        if (healthBarBack.anchoredPosition.x < 189) {
            healthBarMiddle.sizeDelta = new Vector2(healthBarMiddle.sizeDelta.x + 3.6f, healthBarMiddle.sizeDelta.y);
            healthBarBack.anchoredPosition = new Vector2(healthBarBack.anchoredPosition.x + 3.6f, healthBarBack.anchoredPosition.y);
        }
    }

    void Update()
    {

    }
}