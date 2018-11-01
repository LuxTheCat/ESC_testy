using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsCount : MonoBehaviour {

    public float deltaTime;
    public Text text;

	void Start () {
        text = GetComponent<Text>();
	}
	
	void Update () {
        deltaTime = (1f / Time.unscaledDeltaTime);
        text.text = deltaTime.ToString();
    }
}
