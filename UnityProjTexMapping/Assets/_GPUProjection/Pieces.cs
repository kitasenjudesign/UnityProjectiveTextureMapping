using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//葉っぱたち

public class Pieces : DrawMeshInstancedBase {

    private PieceData[] _data;

    void Start(){

        _propertyBlock = new MaterialPropertyBlock();
        _matrices = new Matrix4x4[_count];
        _data = new PieceData[_count];
        _colors = new Vector4[_count];

        for (int i = 0; i < _count; i++)
        {
            _matrices[i] = Matrix4x4.identity;
            _data[i] = new PieceData();//
            _data[i].rot = Quaternion.Euler(
                360f*Random.value,
                360f*Random.value,
                360f*Random.value
            );
            _data[i].scale = new Vector3(
                0.1f,0.2f+5.0f*Random.value,0.1f
            );
            _colors[i] = new Vector4(
                Random.value,
                Random.value*0.8f,
                Random.value*0.4f,
                1f
            );
            
        }

        //SetScale();
    }

    public void SetColor(bool isMono){

        for (int i = 0; i < _count; i++)
        {
            _colors[i] = new Vector4(
                Random.value,
                Random.value*0.8f,
                Random.value*0.4f,
                1f
            );
        }  

    }

    public void Tween(){

        for (int i = 0; i < _count; i++)
        {
            _data[i].Tween(5f,4f*Random.value);
        }        

    }

    public void SetMesh(Mesh m){
        _mesh = m;
    }

    public void SetCount(int n){
        _count=n;
    }

    public void SetScale(int mode){
        
        
        int n = mode;
        float ss=1f;
        float s0 = (Random.value<0.5f)? 2f : 0.1f;

        for (int i = 0; i < _count; i++)
        {
            switch(n){
                case 0:
                    _data[i].scale = new Vector3(
                        s0,
                        0.2f+5.0f*Random.value,
                        s0
                    );
                    break;
                case 1:
                    ss = 0.5f + 0.7f*Random.value;
                    _data[i].scale = new Vector3(
                        ss,ss,ss
                    );
                    break;                    
                case 2:
                    ss = 0.3f + 0.5f*Random.value;
                    _data[i].scale = new Vector3(
                        ss,ss,ss
                    );
                    break;

            }
        }

    }


    void Update(){

        for (int i = 0; i < _count; i++)
        {
            //_data[i].angles.x += 0.5f;
            //_data[i].angles.y += 0.4f;
            //_data[i].angles.z += 0.3f;

            _data[i].rot = Quaternion.Euler( _data[i].angles );

            _matrices[i].SetTRS( 
                _data[i].pos,
                _data[i].rot,
                _data[i].scale
            );
            _matrices[i] = transform.localToWorldMatrix * _matrices[i];

        }

        _propertyBlock.SetVectorArray("_Color", _colors);

        Graphics.DrawMeshInstanced(
                _mesh, 
                0, 
                _mat, 
                _matrices, 
                _count, 
                _propertyBlock, 
                ShadowCastingMode.Off, 
                false, 
                gameObject.layer
        );

    }

}