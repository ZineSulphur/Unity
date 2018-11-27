using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowC : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var t = new RenderTexture(Screen.width, Screen.height, 16);
        GetComponent<Camera>().targetTexture = t;
        Shader.SetGlobalTexture("_LOSMaskTexture", t);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
