using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionTest : MonoBehaviour {

	[SerializeField] private Material _material;
	[SerializeField] private Camera _projectionCam;
	// Use this for initialization
	[SerializeField] private Camera _mainCam;
	private Matrix4x4 _tMat;
	private Matrix4x4 _projMatrix;
	private Matrix4x4 _viewMat;

	void Start () {
		_tMat = new Matrix4x4();
		
		
		_tMat[0] = -0.5f;    	//m00
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
	
	// Update is called once per frame
	void Update () {
		
		_material.SetMatrix("_tMat", 		_tMat );
		_material.SetMatrix("_projMat", 	_projectionCam.projectionMatrix );
		_material.SetMatrix("_viewMat", 	_projectionCam.worldToCameraMatrix);
		
	}

	
}
