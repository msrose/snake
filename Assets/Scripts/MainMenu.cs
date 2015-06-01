using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Texture background;
    public Sprite soundOn;
    public Sprite soundOff;
    public Button soundButton; 

    private float lastVolume = -1;
    private bool isVolumeOn = true;

    public void QuitGameClicked() {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }

    public void NewGameClicked() {
        Debug.Log("Start Clicked");
        Application.LoadLevel(1);
    }

    public void MultiplayerClicked() {
        Debug.Log("Multiplayer Clicked");
        Application.LoadLevel(2);
    }

    public void FlipSound() {
        
        if (isVolumeOn) {
            AudioListener.pause = true;
            lastVolume = AudioListener.volume;  
            AudioListener.volume = 0;
            soundButton.GetComponent<Image>().sprite = soundOff;
        } else {
            AudioListener.pause = false;
            AudioListener.volume = lastVolume;
            soundButton.GetComponent<Image>().sprite = soundOn;
        }

        isVolumeOn = !isVolumeOn;
    }

    void onGUI() {
        GUI.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            background
        );
    }
}
