using UnityEngine;
using System.Collections;

public class NewGameScript : MonoBehaviour {

    void OnClick() {
        Debug.Log("Start Clicked");
        Application.LoadLevel("MainScene");
    }
}
