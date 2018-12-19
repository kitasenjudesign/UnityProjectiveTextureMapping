using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webcam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var webcamtex = new WebCamTexture();   //コンストラクタ
        
        Renderer renderer = GetComponent<Renderer>();  //Planeオブジェクトのレンダラ
        renderer.material.mainTexture = webcamtex;    //mainTextureにWebCamTextureを指定
        webcamtex.Play();  //ウェブカムを作動させる

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
