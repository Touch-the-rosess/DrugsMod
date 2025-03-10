Shader "Unlit/TerrainPotatoShader"
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
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float4 color :COLOR;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float4 vertex :SV_POSITION;
          float4 color :COLOR;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
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
          out_v.texcoord1.xy = in_v.texcoord1.xy;
          out_v.texcoord2.xy = in_v.texcoord2.xy;
          out_v.texcoord3.xy = in_v.texcoord3.xy;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.color = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float4 u_xlat1_d;
      float4 u_xlat2;
      float4 u_xlat3;
      float4 u_xlat4;
      float4 u_xlat5;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.x = (in_f.texcoord1.x==0);
          if((u_xlat0_d.x!=0))
          {
              u_xlat0_d.xy = ((in_f.texcoord2.xy * float2(0.007813, (-0.007813))) + float2(0, 1));
              u_xlat0_d = tex2D(_MainTex, u_xlat0_d.xy);
              u_xlat1_d.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
              out_f.color.xyz = (u_xlat0_d.xyz * u_xlat1_d.xxx);
              out_f.color.w = u_xlat0_d.w;
          }
          else
          {
              u_xlat0_d.x = (in_f.texcoord1.x==4);
              if((u_xlat0_d.x!=0))
              {
                  u_xlat0_d.xy = ((in_f.texcoord2.xy * float2(0.007813, (-0.007813))) + float2(0, 1));
                  out_f.color = tex2D(_MainTex, u_xlat0_d.xy);
              }
              else
              {
                  u_xlat0_d.x = (in_f.texcoord1.x==1);
                  if((u_xlat0_d.x!=0))
                  {
                      u_xlat0_d.xy = ((in_f.texcoord2.xy * float2(0.007813, (-0.007813))) + float2(0, 1));
                      u_xlat0_d = tex2D(_MainTex, u_xlat0_d.xy);
                      u_xlat1_d.x = ((-in_f.texcoord1.y) + 15);
                      u_xlat1_d.yz = (in_f.texcoord.xy * in_f.texcoord.xy);
                      u_xlat1_d.y = max(u_xlat1_d.z, u_xlat1_d.y);
                      u_xlat1_d.y = ((-u_xlat1_d.y) + 1);
                      u_xlat1_d.z = (u_xlat1_d.y * u_xlat1_d.y);
                      u_xlat1_d.y = (u_xlat1_d.z * u_xlat1_d.y);
                      u_xlat1_d.z = ((-in_f.texcoord.y) + in_f.texcoord.x);
                      u_xlat1_d.w = (in_f.texcoord.y + in_f.texcoord.x);
                      u_xlat1_d.x = trunc(u_xlat1_d.x);
                      u_xlat2.xyz = (u_xlat1_d.xxx * float3(0.25, 0.125, 0.0625));
                      u_xlat2.xyz = frac(u_xlat2.xyz);
                      u_xlat2.xyz = (u_xlat2.xyz>=float3(0.5, 0.5, 0.5));
                      u_xlat1_d.x = (0<u_xlat1_d.z);
                      u_xlat2.xy = (uint2(u_xlat1_d.xx) & uint2(u_xlat2.xy));
                      u_xlat1_d.x = (0<u_xlat1_d.w);
                      u_xlat1_d.x = (uint(u_xlat1_d.x) & uint(u_xlat2.x));
                      u_xlat3.xyz = (u_xlat0_d.xyz * u_xlat1_d.yyy);
                      u_xlat0_d.xyz = (u_xlat1_d.xxx)?(u_xlat3.xyz):(u_xlat0_d.xyz);
                      u_xlat1_d.xz = (u_xlat1_d.wz<float2(0, 0));
                      u_xlat1_d.zw = (uint2(u_xlat1_d.zx) & uint2(u_xlat2.zy));
                      u_xlat2.xyw = (u_xlat1_d.yyy * u_xlat0_d.xyz);
                      u_xlat0_d.xyz = (u_xlat1_d.www)?(u_xlat2.xyw):(u_xlat0_d.xyz);
                      u_xlat1_d.x = (uint(u_xlat1_d.x) & uint(u_xlat1_d.z));
                      u_xlat1_d.yzw = (u_xlat1_d.yyy * u_xlat0_d.xyz);
                      out_f.color.xyz = (u_xlat1_d.xxx)?(u_xlat1_d.yzw):(u_xlat0_d.xyz);
                      out_f.color.w = u_xlat0_d.w;
                  }
                  else
                  {
                      u_xlat0_d.x = (in_f.texcoord1.x==3);
                      if((u_xlat0_d.x!=0))
                      {
                          out_f.color.w = 0;
                      }
                      else
                      {
                          u_xlat0_d.w = (in_f.texcoord1.x>=32);
                          u_xlat1_d.x = (in_f.texcoord1.x==5);
                          u_xlat0_d.w = (uint(u_xlat0_d.w) | uint(u_xlat1_d.x));
                          if((u_xlat0_d.w!=0))
                          {
                              u_xlat1_d = float4(((in_f.texcoord2 * float4(0.007813, (-0.007813), 0.007813, (-0.007813))) + float4(0, 1, 0, 1)), 0, 0);
                              u_xlat2 = tex2D(_MainTex, u_xlat1_d.xy);
                              u_xlat0_d.w = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
                              u_xlat2.xyz = (u_xlat0_d.www * u_xlat2.xyz);
                              u_xlat1_d = tex2D(_MainTex, u_xlat1_d.zw);
                              u_xlat0_d.w = ((-u_xlat1_d.w) + 1);
                              u_xlat2.xyz = (u_xlat2.xyz * u_xlat0_d.www);
                              out_f.color.xyz = ((u_xlat1_d.xyz * u_xlat1_d.www) + u_xlat2.xyz);
                              out_f.color.w = u_xlat2.w;
                          }
                          else
                          {
                              u_xlat0_d.w = (in_f.texcoord1.x>=16);
                              if((u_xlat0_d.w!=0))
                              {
                                  u_xlat1_d = float4(((in_f.texcoord2 * float4(0.007813, (-0.007813), 0.007813, (-0.007813))) + float4(0, 1, 0, 1)), 0, 0);
                                  u_xlat2 = tex2D(_MainTex, u_xlat1_d.xy);
                                  u_xlat0_d.w = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
                                  u_xlat2.xyz = (u_xlat0_d.www * u_xlat2.xyz);
                                  u_xlat1_d = tex2D(_MainTex, u_xlat1_d.zw);
                                  u_xlat3.xy = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.261719, 0.988281));
                                  u_xlat3 = tex2D(_MainTex, u_xlat3.xy).wxyz;
                                  u_xlat0_d.w = (in_f.texcoord1.x - 16);
                                  u_xlat0_d.w = trunc(u_xlat0_d.w);
                                  u_xlat4 = (u_xlat0_d.wwww * float4(0.5, 0.25, 0.125, 0.0625));
                                  u_xlat4 = frac(u_xlat4);
                                  u_xlat4 = (u_xlat4.x >= float4(0.5, 0.5, 0.5, 0.5).x && u_xlat4.y >= float4(0.5, 0.5, 0.5, 0.5).y && u_xlat4.z >= float4(0.5, 0.5, 0.5, 0.5).z && u_xlat4.w >= float4(0.5, 0.5, 0.5, 0.5).w);
                                  if((u_xlat4.x!=0))
                                  {
                                      u_xlat3.yz = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.261719, 0.992188));
                                      u_xlat5 = tex2D(_MainTex, u_xlat3.yz);
                                      u_xlat3.x = (u_xlat3.x + u_xlat5.w);
                                  }
                                  if((u_xlat4.y!=0))
                                  {
                                      u_xlat3.yz = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.265625, 0.988281));
                                      u_xlat5 = tex2D(_MainTex, u_xlat3.yz);
                                      u_xlat3.x = (u_xlat3.x + u_xlat5.w);
                                  }
                                  if((u_xlat4.z!=0))
                                  {
                                      u_xlat3.yz = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.261719, 0.984375));
                                      u_xlat5 = tex2D(_MainTex, u_xlat3.yz);
                                      u_xlat3.x = (u_xlat3.x + u_xlat5.w);
                                  }
                                  if((u_xlat4.w!=0))
                                  {
                                      u_xlat3.yz = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.257813, 0.988281));
                                      u_xlat4 = tex2D(_MainTex, u_xlat3.yz);
                                      u_xlat3.x = (u_xlat3.x + u_xlat4.w);
                                  }
                                  u_xlat0_d.w = (1<u_xlat3.x);
                                  u_xlat0_d.w = (u_xlat0_d.w)?(1):(u_xlat3.x);
                                  u_xlat1_d.w = ((-u_xlat0_d.w) + 1);
                                  u_xlat2.xyz = (u_xlat2.xyz * u_xlat1_d.www);
                                  out_f.color.xyz = ((u_xlat1_d.xyz * u_xlat0_d.www) + u_xlat2.xyz);
                                  out_f.color.w = u_xlat2.w;
                              }
                              else
                              {
                                  out_f.color = float4(1, 1, 1, 1);
                              }
                          }
                      }
                  }
              }
          }
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
