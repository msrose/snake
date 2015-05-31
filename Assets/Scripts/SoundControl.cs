using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundControl : MonoBehaviour {

    private bool isVolumeOn = true;
    private float lastVolume = -1F;    
    
    public void Update() {
		if (Input.GetKeyDown (KeyCode.M)) {
			if (isVolumeOn) {
				AudioListener.pause = true;
				lastVolume = AudioListener.volume;
				AudioListener.volume = 0;
			} else {
				AudioListener.pause = false;
				AudioListener.volume = lastVolume;
			}

			isVolumeOn = !isVolumeOn;
		}
	}

}
