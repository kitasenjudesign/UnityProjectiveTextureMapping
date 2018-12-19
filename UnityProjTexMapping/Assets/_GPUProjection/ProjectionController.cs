using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//葉っぱたち

public class ProjectionController : MonoBehaviour {

    [SerializeField] private Pieces _pieces;
    [SerializeField] private Cam _cam;

    [SerializeField] private Mesh[] _meshes;

    void Start(){

        //_loop();
        Invoke("_loop",0.5f);
    }

    void _loop(){

        Debug.Log("loop");

        int n = Mathf.FloorToInt(Random.value * 3f);

        _pieces.SetCount( Mathf.FloorToInt( 300 + 200 * Random.value ) );
        _pieces.SetScale(n);
        _pieces.Tween();
        
        if(n==1 || n==2){
            if(Random.value<0.5f){
                _pieces.SetMesh(_meshes[0]);
            }else{
                _pieces.SetMesh(_meshes[1]);
            }
        }else{
            _pieces.SetMesh(_meshes[0]);
        }


        _cam.Rotate(10f);

        Invoke("_loop",11f);
    }

    void Update(){



    }

}