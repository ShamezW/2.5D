Shader "Custom/BlockShader" {
    Properties {
        _MainTex("MainTex", 2D) = "grey" {}
        _AddColor("Add Tint", Color) = (0, 0, 0)
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType" = "Transparent" "IgnoreProjector"="True" "Bent"="Bent"}
        LOD 200
 
        Pass {
            ZWrite Off
            Cull Off
            ColorMask RGB
            Lighting Off
            Blend DstColor SrcColor
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
                
                fixed4 _AddColor;
                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                    return o;
                }
           
                float4 frag(v2f IN) : COLOR
                {
                    return tex2D(_MainTex, IN.uv_MainTex);
                }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
