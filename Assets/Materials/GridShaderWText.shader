Shader "Custom/GridShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0.5, 0.5, 0.5, 1)
        _LineColor ("Line Color", Color) = (0, 0, 0, 1)
        _LineWidth ("Line Width", Range(0.01, 0.1)) = 0.02
        _GridScale ("Grid Scale", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            float4 _MainColor;
            float4 _LineColor;
            float _LineWidth;
            float _GridScale;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _GridScale;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 grid = frac(i.uv);
                float xLine = step(grid.x, _LineWidth) + step(1.0 - grid.x, _LineWidth);
                float yLine = step(grid.y, _LineWidth) + step(1.0 - grid.y, _LineWidth);
                float gridLine = saturate(xLine + yLine);
                return lerp(_MainColor, _LineColor, gridLine);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}











