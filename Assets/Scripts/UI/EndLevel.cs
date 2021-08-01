using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndLevel: MonoBehaviour{
    //point display on endscreen
    // foreach (Transform eachChild in transform)
    //   {
    //       if (eachChild.name != "Point")
    //       {
    //           Debug.Log("Child found. Name: " + eachChild.name);
    //           // disable them
    //           eachChild.gameObject.SetActive(false);
    //           Time.timeScale = 1.0f;
    //       }
    //   }
    public Text pointText;
    private int point = 0;
    private bool countPointState = false;
    Update(){

      }

}