using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionTest2 : MonoBehaviour {

	[SerializeField] private Material _material;
	[SerializeField] private Camera _projectionCam;
	[SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private ProjObj[] _projObjs;
	private Matrix4x4 _tMat;
	private Matrix4x4 _projMat;
	private Matrix4x4 _viewMat;

	private bool _isCapture = false;
	void Start () {
		
		_tMat = new Matrix4x4(
			new Vector4(0.5f,0,0,0),//m00,m10,m20,m30
			new Vector4(0,0.5f,0,0),//m01,m11,m21,m31
			new Vector4(0,0,1f,0),//m02,m12,m22,m32
			new Vector4(0.5f,0.5f,0,1f)//m03,m13,m23,m33
		);
		
		_tMat[0] = 0.5f;    	//m00
    	_tMat[1] = 0;        	//m10
    	_tMat[2] = 0;        	//m20
    	_tMat[3] = 0;        	//m30

    	_tMat[4] = 0;        //m01
    	_tMat[5] = 0.5f;     //m11
    	_tMat[6] = 0;        //m21
		_tMat[7] = 0;        //m31

    	_tMat[8] = 0;        //m02
    	_tMat[9] = 0;        //m12
    	_tMat[10] = 1f;        //m22
    	_tMat[11] = 0;        //m32

    	_tMat[12] = 0.5f;        //m03
    	_tMat[13] = 0.5f;        //m13
    	_tMat[14] = 0;        //m23
    	_tMat[15] = 1f;        //m33
		
	}
	

    void OnGUI()
    {
       if(_renderTexture!=null){
        	//GUI.DrawTexture(new Rect(0, 0, 120, 120), _renderTexture, ScaleMode.StretchToFill, true, 10.0F);
       }
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
		if(_isCapture){
			
            _projMat = _projectionCam.projectionMatrix;
            _viewMat = _projectionCam.worldToCameraMatrix;



			Graphics.Blit(source, _renderTexture);

            for(int i=0;i<_projObjs.Length;i++){
                _projObjs[i].SetMatrix();
            }

            //
            

			_isCapture=false;
		}

        Graphics.Blit(source, destination);
    }

	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)){
			for(int i=0;i<_projObjs.Length;i++){
				_projObjs[i].Hide();
			}
			_isCapture = true;
		}

		_material.SetMatrix("_tMat", 		_tMat );
		_material.SetMatrix("_projMat", 	_projMat);//_projectionCam.projectionMatrix );
		_material.SetMatrix("_viewMat", 	_viewMat);
		_material.SetTexture("_MainTex", _renderTexture );

	}

	
}
