using UnityEngine;
using System.Collections;
//using DG.Tweening;

public class CamX : MonoBehaviour {

	public GameObject target = null;
	public float amp = 2f;
	public float radX = 0f;
	public float radY = Mathf.PI/4;

	public float spdX = 0.01f;
	public float spdY = 0.01f;

	private Vector3 mPosition;
	private int counter = 0;

	private bool _isStart = false;

	// Use this for initialization
	void Start () {
		mPosition = this.transform.position;
		Invoke("_onStart",2f);
	}
	
	void _onStart(){
		_isStart = true;
	}

	/*
	//一周回転させる
	public void Rotate(float duration){

		Debug.Log("Roate");
		DOVirtual.Float(-Mathf.PI*2f,Mathf.PI*2f,duration,(value)=>{
			radX=value;
		}).SetEase(Ease.InOutCubic);

		float tgtY = 1.5f*Mathf.PI/2f*(Random.value-0.5f);
		DOVirtual.Float(0,tgtY,duration*0.5f,(value)=>{
			radY=value;
		}).SetEase(Ease.InCubic);

		DOVirtual.Float(tgtY,0,duration*0.5f,(value)=>{
			radY=value;
		}).SetEase(Ease.OutCubic).SetDelay(duration*0.5f);

	}*/

	// Update is called once per frame
	void Update () {
	
		

		if ( target != null ){
			

			
			this.radX = 0.5f*Mathf.Sin(Time.realtimeSinceStartup*1.0f);
			this.radY = 0.5f*Mathf.Cos(Time.realtimeSinceStartup*1.0f);

			float amp1 	= this.amp * Mathf.Cos(this.radY);
			Vector3 tt = target.transform.position;

			float x 	= tt.x + amp1 * Mathf.Sin( this.radX );//横
			float y		= tt.y + this.amp * Mathf.Sin(this.radY);
			float z		= tt.z + amp1 * Mathf.Cos( this.radX );//横
			
			mPosition.x += (x - mPosition.x) / 10f;
			mPosition.y += (y - mPosition.y) / 10f;
			mPosition.z += (z - mPosition.z) / 10f;
			transform.position = mPosition;
			//transform

			transform.LookAt(target.transform);

		}

	}
}