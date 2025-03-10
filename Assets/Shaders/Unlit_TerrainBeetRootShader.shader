Shader "Unlit/TerrainBeetRootShader"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    LOD 100
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 100
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
          float4 color :COLOR;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
          float4 color :COLOR;
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
          out_v.texcoord.xy = in_v.texcoord.xy;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.color = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float4 u_xlat1_d;
      float4 u_xlat2;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = (float2(0, 0)<in_f.color.xy);
          u_xlat0_d.z = (_Time.x * 200);
          u_xlat0_d.z = sin(u_xlat0_d.z);
          u_xlat1_d = ((u_xlat0_d.zzzz * float4(0.1, 0.1, 0.1, 0.5)) + float4(0.9, 0.9, 0.9, 0.5));
          u_xlat0_d.zw = ((in_f.texcoord.xy * float2(0.007813, (-0.007813))) + float2(0, 1));
          u_xlat2 = tex2D(_MainTex, u_xlat0_d.zw);
          u_xlat0_d.z = ((u_xlat1_d.w * u_xlat2.x) + u_xlat2.z);
          u_xlat1_d.xyz = (u_xlat1_d.xyz * u_xlat2.xyz);
          u_xlat2.z = (u_xlat0_d.y)?(u_xlat0_d.z):(u_xlat2.z);
          u_xlat2.xyz = (u_xlat0_d.xxx)?(u_xlat1_d.xyz):(u_xlat2.xyz);
          u_xlat0_d.x = (u_xlat2.w<0.1);
          out_f.color = (u_xlat0_d.xxxx)?(float4(0.16, 0.12, 0.06, 1)):(u_xlat2);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
