using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap : MonoBehaviour {

	[SerializeField] private RenderTexture _renderTexture;
	private bool _isCapture = false;


	void Start () {
	
	}
	
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
		if(_isCapture){
            Graphics.Blit(source, _renderTexture);
			_isCapture=false;
		}

        Graphics.Blit(source, destination);
    }

	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)){
			_isCapture=true;
		}

	}

	
}
