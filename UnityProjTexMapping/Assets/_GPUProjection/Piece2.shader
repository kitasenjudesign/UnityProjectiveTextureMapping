Shader "AA/Piece2" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount ("_Amount", Range(0,5)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows
		#pragma surface surf Standard addshadow vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
        #pragma multi_compile_instancing

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
            float4 hoge;
            UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		half _Glossiness;
		half _Metallic;
		float _Amount;

        float4x4 _tMat;
        float4x4 _projMat;
        float4x4 _viewMat;

        UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color) // Make _Color an instanced property (i.e. an array)
#define _Color_arr Props
        UNITY_INSTANCING_BUFFER_END(Props)


        void vert(inout appdata_full v, out Input o )
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
			
                //インスタンス ID がシェーダー関数にアクセス可能になります。頂点シェーダーの最初に使用する必要があります
                UNITY_SETUP_INSTANCE_ID (v);

                //頂点シェーダーで入力構造体から出力構造体へインスタンス ID をコピーします。
                //フラグメントシェーダーでは、インスタンスごとのデータにアクセスするときのみ必要です
                UNITY_TRANSFER_INSTANCE_ID (v, o);

				float4 worldPos = mul(unity_ObjectToWorld,float4(v.vertex.xyz, 1.0));
				float4 viewPos = mul(_viewMat, worldPos );
				float4 projPos = mul( _projMat, viewPos );

                //o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				o.hoge = mul(_tMat, projPos);//TRANSFORM_TEX(v.uv, _MainTex);


		}

		void surf (Input IN, inout SurfaceOutputStandard o) {

            //UNITY_SETUP_INSTANCE_ID (IN);
            fixed4 col = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);

			// Albedo comes from a texture tinted by color
            fixed4 col2 = tex2D( _MainTex, IN.hoge.xy / IN.hoge.w );
			
            fixed4 c = max(col,col2);
			o.Albedo = c.rgb;//col2.rgb;//c.rgb;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//o.Emission = col2.rgb*0.3;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}