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
        if (healthBarBack.anchoredPosition.x > 20) {
            healthBarMiddle.sizeDelta = new Vector2(healthBarMiddle.sizeDelta.x - 3.6f, healthBarMiddle.sizeDelta.y);
            healthBarBack.anchoredPosition = new Vector2(healthBarBack.anchoredPosition.x - 3.6f, healthBarBack.anchoredPosition.y);
        }
    }

    public void AddHealth2() {
        if (healthBarBack.anchoredPosition.x < 375) {
            healthBarMiddle.sizeDelta = new Vector2(healthBarMiddle.sizeDelta.x + 7.2f, healthBarMiddle.sizeDelta.y);
            healthBarBack.anchoredPosition = new Vector2(healthBarBack.anchoredPosition.x + 7.2f, healthBarBack.anchoredPosition.y);
        }
    }

    void Update()
    {

    }
}