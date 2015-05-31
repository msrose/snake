using UnityEngine;
using System.Collections;

public class QuitGameScript : MonoBehaviour {

	void OnClick () {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }

}
