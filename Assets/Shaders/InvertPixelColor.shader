// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ddShaders/dd_Invert"
{
    Properties
    {
        _Color ("Tint Color", Color) = (1,1,1,1)
        _MainTex ("Sprite Texture", 2D) = "white" {} // Adding the sprite texture
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType" = "Transparent"
        }

        Pass
        {
            ZWrite Off
            ColorMask RGB
            Blend OneMinusDstColor OneMinusSrcAlpha //invert blending, so long as FG color is 1,1,1,1
            BlendOp Add


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            uniform float4 _Color;
            uniform sampler2D _MainTex; // Texture of the sprite

            struct vertexInput
            {
                float4 vertex: POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0; // UV for texture sampling
            };

            struct fragmentInput
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR0;
                float2 uv : TEXCOORD0; // UV coordinates passed to fragment shader
            };

            fragmentInput vert(vertexInput i)
            {
                fragmentInput o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.color = _Color * i.color; // Multiply the tint color by the vertex color
                o.uv = i.texcoord; // Pass UV coordinates
                return o;
            }

            half4 frag(fragmentInput i) : COLOR
            {
                half4 texColor = tex2D(_MainTex, i.uv); // Sample the sprite texture
                texColor.rgb *= texColor.a;
                return texColor * i.color; // Modulate texture color with tint
            }
            ENDCG
        }
    }
}