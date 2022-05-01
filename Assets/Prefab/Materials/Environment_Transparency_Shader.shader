Shader "Unlit/Environment_Transparency_Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        
        _Cutoff ("Alpha Cutoff", Range(0.0, 1.0)) = 0.5 
        
        _Glossiness ("Smoothness", Range(0.0, 1.0)) = 0.5
        _GlossMapScale ("Smoothness Scale", Range(0.0, 1.0)) = 0.5
        [Enum(Metallic Alpha, 0, Albedo Alpha, 1)] _SmoothnessTextureChannel ("Smoothness Texture Channel", Float) = 0
        
        [Gamma]_Metallic ("Metallic", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap ("Metallic", 2D) = "white" {}
        
        [ToggleOff] _SpecularHighlights ("Specular Highlights", Float) = 1.0
        [ToggleOff] _GlossyReflections ("Glossy Reflections", Float) = 1.0
        
        _BumpScale ("Scale", Float) = 1.0
        [Normal] _BumpMap ("Normal Map", 2D) = "bump" {}
        
        _Parallax ("Heigh Scale", Range(0.005, 0.08)) = 0.02
        _ParallaxMap ("Heigh Map", 2D) = "black" {}
        
        _OcclusionStrength ("Strength", Range(0.0, 1.0)) = 1.0
        _OcclusionMap ("Occlusion", 2D) = "white" {}
        
        _EmissionColor ("Color", Color) = (0, 0, 0)
        _EmissionMap ("Emission", 2D) = "white" {}
        
        _DetailMask ("Detail Mask", 2D) = "white" {}
        
        _DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" {}
        _DetailNormalMapScale ("Scale", Float) = 1.0
        [Normal] _DetailNormalMap ("Normal Map", 2D) = "bump" {}
        
        [Enum(UV0, 0, UV1, 1)] _UVSec ("UV Set for secondary textures", Float) = 0
        
        //Blending state
        [HideInInspector] _Mode ("_mode", Float) = 0.0
        [HideInInspector] _SrcBlend ("_src", Float) = 1.0
        [HideInInspector] _DstBlend ("_dst", Float) = 0.0
        [HideInInspector] _ZWrite ("_zw", Float) = 1.0
    }
    
//    CGINCLUDE
//        #define UNITY_SETUP_BRDF_INPUT MetallicSetup
//    ENDCG
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "FORWARD"
            Tags {"LightMode" = "ForwardBase"}
            
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]
            
            Stencil
            {
                Ref 1
                Comp NotEqual
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1)
                UNITY_FOG_COORDS(2)
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal, 1));
                TRANSFER_SHADOW(o);
                //o.diff.rgb += ShadeSH9(half4(worldNormal, 1));
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed3 lighting = i.diff * shadow + i.ambient;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                //col.rgb *= i.diff;
                col.rgb *= lighting;
                return col;
            }
            ENDCG
        }
        
        Pass
        {
            Tags {"LightMode"="ShadowCaster"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f
            {
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i);
            }
            
            ENDCG
        }
    }
}
