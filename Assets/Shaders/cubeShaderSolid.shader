Shader "Custom/BlockShaderSolid" {
    Properties {
        _MainTex("MainTex", 2D) = "grey" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType" = "Transparent" "IgnoreProjector"="True" "Bent"="Bent"}
        LOD 200
 
        Pass {
            Lighting Off
        Tags { "LightMode" = "ForwardBase" }
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                #pragma target 2.0
           
                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv_MainTex : TEXCOORD0;
                };
                
                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;

                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                    return o;
                }
           
                float4 frag(v2f IN) : COLOR
                {
                    return tex2D(_MainTex, IN.uv_MainTex) * _Color;
                }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
