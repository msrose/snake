using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundControl : MonoBehaviour {

    private bool isVolumeOn = true;
    private float lastVolume = -1F;
    
    public Button soundButton;
    public Sprite soundOff;
    public Sprite soundOn;
    
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

}
