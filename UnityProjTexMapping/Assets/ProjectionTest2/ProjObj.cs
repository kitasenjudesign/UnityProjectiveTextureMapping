using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class ProjObj : MonoBehaviour {

	private MeshRenderer _renderer;
	private MaterialPropertyBlock _propertyBlock;

	private Vector3 _rotSpeed;
	private Vector3 _rot;
	private Matrix4x4 _mat;

	// Use this for initialization
	void Awake () {

		_rotSpeed = new Vector3(
			1.5f*(Random.value-0.5f),
			1.5f*(Random.value-0.5f),
			1.5f*(Random.value-0.5f)
		);

		_rot = transform.rotation.eulerAngles;

		_propertyBlock = new MaterialPropertyBlock();
		_renderer =GetComponent<MeshRenderer>();
		
		gameObject.SetActive(false);

	}
	
	public void Hide(){
		gameObject.SetActive(false);
	}

	public void SetMatrix(){
		
		Debug.Log("SetMat");
		
		gameObject.SetActive(true);

		//_mat = _renderer.localToWorldMatrix;
		_mat = transform.localToWorldMatrix;
		if(_propertyBlock!=null) _propertyBlock.SetMatrix("_modelMat", _mat );
		if(_renderer!=null) _renderer.SetPropertyBlock(_propertyBlock);
		
		//_rot.y += 360f;
		//transform.DORotate(_rot,1f);
		//transform.dolo

	}

	// Update is called once per frame
	void Update () {
		
		//transform.Rotate(_rotSpeed);

	}
}
