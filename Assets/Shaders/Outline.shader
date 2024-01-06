Shader "Custom/Outline" {
    Properties{
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1, 0, 0, 1)
    }

        SubShader{
            Tags { "Queue" = "Transparent" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float4 _OutlineColor;
                sampler2D _MainTex;

                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    // Calculate the gradient using ddx and ddy
                    float2 ddx_uv = ddx(i.uv);
                    float2 ddy_uv = ddy(i.uv);
                    float gradient = sqrt(ddx_uv.x * ddx_uv.x + ddy_uv.y * ddy_uv.y);

                    // Calculate the edge using the gradient
                    float edge = saturate(1 - gradient * 10);

                    // Mix the outline color with the main texture
                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    return lerp(_OutlineColor, texColor, edge);
                }
                ENDCG
            }
        }
}
