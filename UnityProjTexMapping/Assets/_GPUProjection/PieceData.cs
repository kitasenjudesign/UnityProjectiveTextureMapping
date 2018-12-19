using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using DG.Tweening;

public class PieceData {

    public Vector3 pos = new Vector3(
        9f*( Random.value-0.5f ),
        9f*( Random.value-0.5f ),
        8f*( Random.value-0.5f )      
    );
    public Quaternion rot =Quaternion.Euler(0,0,0);
    public Vector3 scale = new Vector3(1f,1f,1f);

    public Vector3 angles = new Vector3(
        180f * ( Random.value-0.5f ),
        180f * ( Random.value-0.5f ),
        180f * ( Random.value-0.5f )
    );

    public void Tween(float duration, float delay){

        Vector3 s = new Vector3(pos.x,pos.y,pos.z);

        Vector3 tgt = new Vector3(
            9f*( Random.value-0.5f ),
            12f*( Random.value-0.5f ),
            8f*( Random.value-0.5f )  
        );

        //DOVirtual.Float(0,1f,duration,(value)=>{
        //    pos = Vector3.Lerp(s,tgt,value);
        //}).SetDelay(delay).SetEase(Ease.InOutCubic);

    }

}