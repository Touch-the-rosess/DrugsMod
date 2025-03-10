Shader "Legacy Shaders/Particles/Alpha Blended"
{
  Properties
  {
    _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
    _MainTex ("Particle Texture", 2D) = "white" {}
    _InvFade ("Soft Particles Factor", Range(0.01, 3)) = 1
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      uniform float4 _TintColor;
      uniform float4 _MainTex_ST;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      //uniform float4 _ProjectionParams;
      //uniform float4x4 unity_MatrixV;
      uniform sampler2D _MainTex;
      uniform float _InvFade;
      //uniform float4 _ZBufferParams;
      uniform sampler2D _CameraDepthTexture;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 color :COLOR;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 vertex :SV_POSITION;
          float4 color :COLOR;
          float2 texcoord :TEXCOORD0;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct v2f
      {
          float4 vertex :SV_POSITION;
          float4 color :COLOR;
          float2 texcoord :TEXCOORD0;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = mul(unity_ObjectToWorld, float4(in_v.vertex.xyz,1.0));
          u_xlat1 = mul(unity_MatrixVP, u_xlat0);
          out_v.vertex = u_xlat1;
          out_v.color = (in_v.color * _MainTex_ST);
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          u_xlat0.y = (u_xlat0.y * conv_mxt4x4_-7(unity_MatrixVP).z);
          u_xlat0.x = ((conv_mxt4x4_-8(unity_MatrixVP).z * u_xlat0.x) + u_xlat0.y);
          u_xlat0.x = ((conv_mxt4x4_-6(unity_MatrixVP).z * u_xlat0.z) + u_xlat0.x);
          u_xlat0.x = ((conv_mxt4x4_-5(unity_MatrixVP).z * u_xlat0.w) + u_xlat0.x);
          out_v.texcoord2.z = (-u_xlat0.x);
          u_xlat0.x = (u_xlat1.y * _ProjectionParams.x);
          u_xlat0.w = (u_xlat0.x * 0.5);
          u_xlat0.xz = (u_xlat1.xw * float2(0.5, 0.5));
          out_v.texcoord2.w = u_xlat1.w;
          out_v.texcoord2.xy = (u_xlat0.zz + u_xlat0.xw);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float4 u_xlat1_d;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = (in_f.texcoord2.xy / in_f.texcoord2.ww);
          u_xlat0_d = tex2D(_CameraDepthTexture, u_xlat0_d.xy);
          u_xlat0_d.x = ((_ZBufferParams.z * u_xlat0_d.x) + _ZBufferParams.w);
          u_xlat0_d.x = (1 / u_xlat0_d.x);
          u_xlat0_d.x = (u_xlat0_d.x - in_f.texcoord2.z);
          u_xlat0_d.x = saturate((u_xlat0_d.x * _InvFade.x));
          u_xlat0_d.w = (u_xlat0_d.x * in_f.color.w);
          u_xlat0_d.xyz = in_f.color.xyz;
          u_xlat0_d = (u_xlat0_d + u_xlat0_d);
          u_xlat1_d = tex2D(_MainTex, in_f.texcoord.xy);
          u_xlat0_d = (u_xlat0_d * u_xlat1_d);
          out_f.color.w = saturate(u_xlat0_d.w);
          out_f.color.xyz = u_xlat0_d.xyz;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
