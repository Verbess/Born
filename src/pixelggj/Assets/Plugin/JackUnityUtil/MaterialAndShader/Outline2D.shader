Shader "Custom/2D/Outline2D" {

    Properties {

        [PerRendererData] _MainTex ("Texture2D", 2D) = "" {}
        _MainTex_TexSize ("TextureSize", Vector) = (0, 0, 0, 0)
        _Outline ("Outline", Range(0, 1)) = 0.0125
        _OutlineColor ("OutlineColor", Color) = (0.1, 0.2, 0.3, 1)

    }

    SubShader {

        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Opaque"
        }

        ZWrite Off

        Pass {

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            fixed _Outline;
            fixed4 _OutlineColor;
            sampler2D _MainTex;
            fixed4 _MainTex_TexSize;

            struct appdata {
                fixed4 vertex : POSITION;
                fixed3 normal : NORMAL;
                fixed4 color : COLOR;
                fixed4 uv : TEXCOORD0;
            };

            struct v2f {
                fixed4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                fixed4 uv : TEXCOORD0;
            };

            v2f vert(appdata d) {
                v2f o;
                o.uv = d.uv;
                o.color = d.color;
                o.vertex = UnityObjectToClipPos(d.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET {

                fixed4 color = tex2D(_MainTex, i.uv);

                float2 uv_up = i.uv + _MainTex_TexSize.xy * float2(0, 1 * _Outline);
                float2 uv_down = i.uv + _MainTex_TexSize.xy * float2(0, -1 * _Outline);
                float2 uv_left = i.uv + _MainTex_TexSize.xy * float2(-1 * _Outline, 0);
                float2 uv_right = i.uv + _MainTex_TexSize.xy * float2(1 * _Outline, 0);

                float w = tex2D(_MainTex, uv_up).a * tex2D(_MainTex, uv_down).a
                        * tex2D(_MainTex, uv_left).a * tex2D(_MainTex, uv_right).a;
                
                color.rgb = lerp(_OutlineColor, color.rgb, w);

                return color;

            }

            ENDCG

        }

    }
}