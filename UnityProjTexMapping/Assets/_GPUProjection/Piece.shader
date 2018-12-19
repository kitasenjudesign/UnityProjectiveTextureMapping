// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "AA/Piece"
{
    Properties
    {
        _Color ("Color", Vector) = (1.0, 0.0, 0.0, 1.0)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        //Tags { "RenderType"="Opaque" }
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
        cull off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 tangent : TANGENT;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4x4 _tMat;
			float4x4 _projMat;
			float4x4 _viewMat;

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color) // Make _Color an instanced property (i.e. an array)
#define _Color_arr Props
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v)
            {
                v2f o;

                //インスタンス ID がシェーダー関数にアクセス可能になります。頂点シェーダーの最初に使用する必要があります
                UNITY_SETUP_INSTANCE_ID (v);

                //頂点シェーダーで入力構造体から出力構造体へインスタンス ID をコピーします。
                //フラグメントシェーダーでは、インスタンスごとのデータにアクセスするときのみ必要です
                UNITY_TRANSFER_INSTANCE_ID (v, o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

				float4 worldPos = mul(unity_ObjectToWorld,float4(v.vertex.xyz, 1.0));
				float4 viewPos = mul(_viewMat, worldPos );
				float4 projPos = mul( _projMat, viewPos );

				o.tangent = mul(_tMat, projPos);//TRANSFORM_TEX(v.uv, _MainTex);


                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID (i);
                // 変数にアクセス
                fixed4 col = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                //fixed4 col2 = tex2D( _MainTex, i.tangent.xy / i.tangent.w );
                fixed4 col2 = tex2D( _MainTex, i.tangent.xy / i.tangent.w );


                col = max(col,col2);

                return col;
            }
            ENDCG
        }
    }
}