Shader "Custom/BubblingWater" {
    Properties {
        _Color ("Color", Color) = (0.3, 0.6, 0.9, 1)
        _BubbleColor ("Bubble Color", Color) = (1, 1, 1, 1)
        _BubbleSpeed ("Bubble Speed", Float) = 1.0
        _BubbleScale ("Bubble Scale", Float) = 1.0
        _BubbleIntensity ("Bubble Intensity", Float) = 0.5
        _BubbleTiling ("Bubble Tiling", Float) = 1.0
        _BubbleTexture ("Bubble Texture", 2D) = "white" {}
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}

        Cull Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _BubbleTexture;
            float4 _Color;
            float4 _BubbleColor;
            float _BubbleSpeed;
            float _BubbleScale;
            float _BubbleIntensity;
            float _BubbleTiling;

            float2 hash2(float2 p) {
                p = float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)));
                return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
            }

            float noiseFunction(float2 uv, float time) {
                float2 ip = floor(uv);
                float2 u = frac(uv);
                u = u * u * (3.0 - 2.0 * u);

                float res = lerp(lerp(dot(hash2(ip + float2(0.0, 0.0)), u),
                                       dot(hash2(ip + float2(1.0, 0.0)), u - float2(1.0, 0.0)),
                                       u.x),
                                 lerp(dot(hash2(ip + float2(0.0, 1.0)), u),
                                       dot(hash2(ip + float2(1.0, 1.0)), u - float2(1.0, 0.0)),
                                       u.x),
                                 u.y);

                float res2 = lerp(lerp(dot(hash2(ip * 2.0 + float2(0.0, 0.0)), u),
                                        dot(hash2(ip * 2.0 + float2(1.0, 0.0)), u - float2(1.0, 0.0)),
                                        u.x),
                                  lerp(dot(hash2(ip * 2.0 + float2(0.0, 1.0)), u),
                                        dot(hash2(ip * 2.0 + float2(1.0, 1.0)), u - float2(1.0, 0.0)),
                                        u.x),
                                  u.y);

                res = res * res;
                res2 = res2 * res2;
                float noise = (res + res2) / 2.0;

                // Add circular motion to the noise
                float2 center = float2(0.5, 0.5);
                float radius = 0.3;
                float angle = time * 0.5;
                float2 offset = center + radius * float2(cos(angle), sin(angle));
                noise += length(offset - uv) * 0.2;

                noise = (noise + 1.0) / 2.0;
                noise = pow(noise, 2.0);
                return noise;
            }

            v2f vert (appdata_t IN) {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            fixed4 frag (v2f IN) : SV_Target {
                float2 uv = IN.texcoord;
                float2 noiseUV = uv * _BubbleTiling + float2(0.5, 0.5);
                float noise = noiseFunction(noiseUV, _Time.y);
                float bubble = noise * _BubbleIntensity;
                fixed4 bubbleTex = tex2D(_BubbleTexture, uv);
                fixed4 color = _Color + _BubbleColor * bubble * fixed4(bubbleTex.rgb, bubbleTex.a);
                color.a = bubble * bubbleTex.a;

                // Add distortion to the UVs based on the noise
                float2 distortedUV = uv + noise * _BubbleScale * (float2(1.0, -1.0) + float2(1.0, 1.0) * noise);
                fixed4 distortedColor = tex2D(_BubbleTexture, distortedUV);

                // Mix the original color with the distorted color based on the bubble intensity
                color.rgb = lerp(color.rgb, distortedColor.rgb, _BubbleIntensity);

                // Remove the line by not adding the bubble value to the color
                color.rgb += bubble * _BubbleColor.rgb;

                // Remove the line by not multiplying the bubble value with the color
                color.rgb = lerp(color.rgb, _Color.rgb, bubble);

                return color;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
