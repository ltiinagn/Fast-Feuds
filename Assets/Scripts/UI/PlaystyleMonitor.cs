using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class PlaystyleMonitor : MonoBehaviour
{
    public GameObject straightCutFry;
    public GameObject meateor;
    public void Start()
    {
        meateor.SetActive(false);
    }

    public void ChangePlaystyle()
    {
        straightCutFry.SetActive(!straightCutFry.activeSelf);
        meateor.SetActive(!meateor.activeSelf);
    }

    void Update()
    {

    }
}