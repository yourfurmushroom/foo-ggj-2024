Shader "Custom/ScrollingBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeedX ("Scroll Speed X", Float) = 1.0
        _ScrollSpeedY ("Scroll Speed Y", Float) = 0.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        float _ScrollSpeedX;
        float _ScrollSpeedY;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 scrolledUV = IN.uv_MainTex;
            float x = fmod(_Time.y * _ScrollSpeedX, 1);
            float y = fmod(_Time.y * _ScrollSpeedY, 1);
            scrolledUV += float2(x, y);
            
            fixed4 c = tex2D (_MainTex, scrolledUV);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}