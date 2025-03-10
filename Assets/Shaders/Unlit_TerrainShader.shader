Shader "Unlit/TerrainShader"
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
      uniform float Epsilon;
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float2 texcoord4 :TEXCOORD4;
          float2 texcoord5 :TEXCOORD5;
          float4 color :COLOR;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float2 texcoord4 :TEXCOORD4;
          float2 texcoord5 :TEXCOORD5;
          float4 vertex :SV_POSITION;
          float4 color :COLOR;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float2 texcoord4 :TEXCOORD4;
          float2 texcoord5 :TEXCOORD5;
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
          out_v.texcoord4.xy = in_v.texcoord4.xy;
          out_v.texcoord5.xy = in_v.texcoord5.xy;
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
      float4 u_xlat6;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = (in_f.texcoord5.xx==float2(3, 2));
          if((u_xlat0_d.x!=0))
          {
              u_xlat0_d.x = (in_f.texcoord5.y * _Time.x);
              u_xlat0_d.x = (u_xlat0_d.x * 400.200012);
              u_xlat0_d.x = floor(u_xlat0_d.x);
              u_xlat0_d.z = (u_xlat0_d.x / 6);
              u_xlat0_d.x = (((-6) * u_xlat0_d.z) + u_xlat0_d.x);
              u_xlat0_d.x = u_xlat0_d.x;
              u_xlat1_d.z = (u_xlat0_d.x + in_f.texcoord3.x);
              u_xlat1_d.xy = in_f.texcoord2.xy;
          }
          else
          {
              u_xlat0_d.x = (in_f.texcoord5.y * _Time.x);
              u_xlat0_d.x = (u_xlat0_d.x * 400.200012);
              u_xlat0_d.x = floor(u_xlat0_d.x);
              u_xlat0_d.z = (u_xlat0_d.x / 6);
              u_xlat0_d.z = (((-6) * u_xlat0_d.z) + u_xlat0_d.x);
              u_xlat0_d.z = u_xlat0_d.z;
              u_xlat2.z = ((u_xlat0_d.z * 2) + in_f.texcoord3.x);
              u_xlat0_d.z = floor(in_f.texcoord4.y);
              u_xlat0_d.w = (u_xlat0_d.x / u_xlat0_d.z);
              u_xlat0_d.x = (((-u_xlat0_d.z) * u_xlat0_d.w) + u_xlat0_d.x);
              u_xlat0_d.x = u_xlat0_d.x;
              u_xlat3.xyz = (in_f.texcoord5.xxx==float3(4, 6, 8));
              u_xlat4.xy = ((in_f.texcoord4.xx * u_xlat0_d.xx) + in_f.texcoord2.yx);
              u_xlat5.x = (u_xlat3.z)?(u_xlat4.y):(in_f.texcoord2.x);
              u_xlat4.z = in_f.texcoord2.x;
              u_xlat5.y = in_f.texcoord2.y;
              u_xlat4.xy = (u_xlat3.yy)?(u_xlat4.zx):(u_xlat5.xy);
              u_xlat2.xy = in_f.texcoord2.xy;
              u_xlat4.z = in_f.texcoord3.x;
              u_xlat1_d.xyz = (u_xlat3.xxx)?(u_xlat2.xyz):(u_xlat4.xyz);
          }
          u_xlat0_d.x = (in_f.texcoord1.x==0);
          if((u_xlat0_d.x!=0))
          {
              u_xlat0_d.x = (u_xlat1_d.x * 0.007812);
              u_xlat0_d.z = (((-u_xlat1_d.y) * 0.007812) + 1);
              u_xlat2 = tex2D(_MainTex, u_xlat0_d.xz);
              u_xlat0_d.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
              u_xlat3.xyz = (u_xlat0_d.xxx * u_xlat2.xyz);
              out_f.color.w = u_xlat2.w;
          }
          else
          {
              u_xlat0_d.x = (in_f.texcoord1.x==4);
              if((u_xlat0_d.x!=0))
              {
                  u_xlat0_d.x = (u_xlat1_d.x * 0.007812);
                  u_xlat0_d.z = (((-u_xlat1_d.y) * 0.007812) + 1);
                  u_xlat3 = tex2D(_MainTex, u_xlat0_d.xz);
                  out_f.color.w = u_xlat3.w;
              }
              else
              {
                  u_xlat0_d.x = (in_f.texcoord1.x==1);
                  if((u_xlat0_d.x!=0))
                  {
                      u_xlat0_d.x = (u_xlat1_d.x * 0.007812);
                      u_xlat0_d.z = (((-u_xlat1_d.y) * 0.007812) + 1);
                      u_xlat2 = tex2D(_MainTex, u_xlat0_d.xz);
                      u_xlat0_d.x = ((-in_f.texcoord1.y) + 15);
                      u_xlat0_d.zw = (in_f.texcoord.xy * in_f.texcoord.xy);
                      u_xlat0_d.z = max(u_xlat0_d.w, u_xlat0_d.z);
                      u_xlat0_d.z = ((-u_xlat0_d.z) + 1);
                      u_xlat0_d.w = (u_xlat0_d.z * u_xlat0_d.z);
                      u_xlat0_d.z = (u_xlat0_d.w * u_xlat0_d.z);
                      u_xlat0_d.w = ((-in_f.texcoord.y) + in_f.texcoord.x);
                      u_xlat1_d.w = (in_f.texcoord.y + in_f.texcoord.x);
                      u_xlat0_d.x = trunc(u_xlat0_d.x);
                      u_xlat4.xyz = (u_xlat0_d.xxx * float3(0.25, 0.125, 0.0625));
                      u_xlat4.xyz = frac(u_xlat4.xyz);
                      u_xlat4.xyz = (u_xlat4.xyz>=float3(0.5, 0.5, 0.5));
                      u_xlat0_d.x = (0<u_xlat0_d.w);
                      u_xlat4.xy = (uint2(u_xlat0_d.xx) & uint2(u_xlat4.xy));
                      u_xlat0_d.x = (0<u_xlat1_d.w);
                      u_xlat0_d.x = (uint(u_xlat0_d.x) & uint(u_xlat4.x));
                      u_xlat5.xyz = (u_xlat0_d.zzz * u_xlat2.xyz);
                      u_xlat2.xyz = (u_xlat0_d.xxx)?(u_xlat5.xyz):(u_xlat2.xyz);
                      u_xlat0_d.x = (u_xlat1_d.w<0);
                      u_xlat1_d.w = (uint(u_xlat0_d.x) & uint(u_xlat4.y));
                      u_xlat4.xyw = (u_xlat0_d.zzz * u_xlat2.xyz);
                      u_xlat2.xyz = (u_xlat1_d.www)?(u_xlat4.xyw):(u_xlat2.xyz);
                      u_xlat0_d.w = (u_xlat0_d.w<0);
                      u_xlat0_d.w = (uint(u_xlat0_d.w) & uint(u_xlat4.z));
                      u_xlat0_d.x = (uint(u_xlat0_d.x) & uint(u_xlat0_d.w));
                      u_xlat4.xyz = (u_xlat0_d.zzz * u_xlat2.xyz);
                      u_xlat3.xyz = (u_xlat0_d.xxx)?(u_xlat4.xyz):(u_xlat2.xyz);
                      out_f.color.w = u_xlat2.w;
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
                          u_xlat0_d.x = (in_f.texcoord1.x>=32);
                          u_xlat0_d.z = (in_f.texcoord1.x==5);
                          u_xlat0_d.x = (uint(u_xlat0_d.z) | uint(u_xlat0_d.x));
                          if((u_xlat0_d.x!=0))
                          {
                              u_xlat2.xy = (u_xlat1_d.zx * float2(0.007812, 0.007812));
                              u_xlat2.w = (((-u_xlat1_d.y) * 0.007812) + 1);
                              u_xlat4 = tex2D(_MainTex, u_xlat2.yw);
                              u_xlat0_d.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
                              u_xlat0_d.xzw = (u_xlat0_d.xxx * u_xlat4.xyz);
                              u_xlat2.y = ((in_f.texcoord3.y * (-0.007812)) + 1);
                              u_xlat2 = tex2D(_MainTex, u_xlat2.xy);
                              u_xlat1_d.w = ((-u_xlat2.w) + 1);
                              u_xlat0_d.xzw = (u_xlat0_d.xzw * u_xlat1_d.www);
                              u_xlat3.xyz = ((u_xlat2.xyz * u_xlat2.www) + u_xlat0_d.xzw);
                              out_f.color.w = u_xlat4.w;
                          }
                          else
                          {
                              u_xlat0_d.x = (in_f.texcoord1.x>=16);
                              if((u_xlat0_d.x!=0))
                              {
                                  u_xlat2.xy = (u_xlat1_d.zx * float2(0.007812, 0.007812));
                                  u_xlat2.w = (((-u_xlat1_d.y) * 0.007812) + 1);
                                  u_xlat1_d = tex2D(_MainTex, u_xlat2.yw);
                                  u_xlat0_d.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
                                  u_xlat0_d.xzw = (u_xlat0_d.xxx * u_xlat1_d.xyz);
                                  u_xlat2.y = ((in_f.texcoord3.y * (-0.007812)) + 1);
                                  u_xlat2 = tex2D(_MainTex, u_xlat2.xy);
                                  u_xlat1_d.xy = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.261719, 0.988281));
                                  u_xlat4 = tex2D(_MainTex, u_xlat1_d.xy).wxyz;
                                  u_xlat1_d.x = (in_f.texcoord1.x - 16);
                                  u_xlat1_d.x = trunc(u_xlat1_d.x);
                                  u_xlat5 = (u_xlat1_d.xxxx * float4(0.5, 0.25, 0.125, 0.0625));
                                  u_xlat5 = frac(u_xlat5);
                                  u_xlat5 = (u_xlat5.x >= float4(0.5, 0.5, 0.5, 0.5).x && u_xlat5.y >= float4(0.5, 0.5, 0.5, 0.5).y && u_xlat5.z >= float4(0.5, 0.5, 0.5, 0.5).z && u_xlat5.w >= float4(0.5, 0.5, 0.5, 0.5).w);
                                  if((u_xlat5.x!=0))
                                  {
                                      u_xlat1_d.xy = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.261719, 0.992188));
                                      u_xlat6 = tex2D(_MainTex, u_xlat1_d.xy);
                                      u_xlat4.x = (u_xlat4.x + u_xlat6.w);
                                  }
                                  if((u_xlat5.y!=0))
                                  {
                                      u_xlat1_d.xy = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.265625, 0.988281));
                                      u_xlat6 = tex2D(_MainTex, u_xlat1_d.xy);
                                      u_xlat4.x = (u_xlat4.x + u_xlat6.w);
                                  }
                                  if((u_xlat5.z!=0))
                                  {
                                      u_xlat1_d.xy = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.261719, 0.984375));
                                      u_xlat6 = tex2D(_MainTex, u_xlat1_d.xy);
                                      u_xlat4.x = (u_xlat4.x + u_xlat6.w);
                                  }
                                  if((u_xlat5.w!=0))
                                  {
                                      u_xlat1_d.xy = ((in_f.texcoord.xy * float2(-0.005524, (-0.005524))) + float2(0.257812, 0.988281));
                                      u_xlat5 = tex2D(_MainTex, u_xlat1_d.xy);
                                      u_xlat4.x = (u_xlat4.x + u_xlat5.w);
                                  }
                                  u_xlat1_d.x = (1<u_xlat4.x);
                                  u_xlat1_d.x = (u_xlat1_d.x)?(1):(u_xlat4.x);
                                  u_xlat1_d.y = ((-u_xlat1_d.x) + 1);
                                  u_xlat0_d.xzw = (u_xlat0_d.xzw * u_xlat1_d.yyy);
                                  u_xlat3.xyz = ((u_xlat2.xyz * u_xlat1_d.xxx) + u_xlat0_d.xzw);
                                  out_f.color.w = u_xlat1_d.w;
                              }
                              else
                              {
                                  u_xlat3.xyz = float3(1, 1, 1);
                                  out_f.color.w = 1;
                              }
                          }
                      }
                  }
              }
          }
          if((u_xlat0_d.y!=0))
          {
              u_xlat0_d.xy = ((in_f.texcoord4.xy * float2(0.007812, (-0.007812))) + float2(0, 1));
              u_xlat0_d = tex2D(_MainTex, u_xlat0_d.xy).yzwx;
              u_xlat1_d.x = (u_xlat0_d.x>=u_xlat0_d.y);
              u_xlat1_d.x = u_xlat1_d.x;
              u_xlat2.xy = u_xlat0_d.yx;
              u_xlat2.zw = float2(-1, 0.666667);
              u_xlat4.xy = (u_xlat0_d.xy - u_xlat2.xy);
              u_xlat4.zw = float2(1, (-1));
              u_xlat1_d = ((u_xlat1_d.xxxx * u_xlat4) + u_xlat2);
              u_xlat2.x = (u_xlat0_d.w>=u_xlat1_d.x);
              u_xlat2.x = u_xlat2.x;
              u_xlat0_d.xyz = u_xlat1_d.xyw;
              u_xlat1_d.xyw = u_xlat0_d.wyx;
              u_xlat1_d = ((-u_xlat0_d) + u_xlat1_d);
              u_xlat0_d = ((u_xlat2.xxxx * u_xlat1_d) + u_xlat0_d);
              u_xlat1_d.x = min(u_xlat0_d.y, u_xlat0_d.w);
              u_xlat0_d.x = (u_xlat0_d.x - u_xlat1_d.x);
              u_xlat0_d.y = ((-u_xlat0_d.y) + u_xlat0_d.w);
              u_xlat0_d.x = ((u_xlat0_d.x * 6) + Epsilon.x);
              u_xlat0_d.x = (u_xlat0_d.y / u_xlat0_d.x);
              u_xlat0_d.x = (u_xlat0_d.z + u_xlat0_d.x);
              u_xlat0_d.y = (in_f.texcoord5.y * _Time.x);
              u_xlat0_d.x = ((abs(u_xlat0_d.x) * 6.283185) + u_xlat0_d.y);
              u_xlat0_d.x = sin((-u_xlat0_d.x));
              u_xlat0_d.x = (u_xlat0_d.x + 1);
              u_xlat0_d.x = (u_xlat0_d.x * 0.5);
              u_xlat0_d.y = (u_xlat0_d.x * u_xlat0_d.x);
              u_xlat0_d.x = (((-u_xlat0_d.x) * u_xlat0_d.y) + 1);
              u_xlat0_d.y = dot(u_xlat3.xyzx, float4(0.3, 0.59, 0.11, 0));
              u_xlat0_d.y = ((-u_xlat0_d.y) + 1);
              u_xlat0_d.z = (u_xlat0_d.y * u_xlat0_d.y);
              u_xlat0_d.y = (((-u_xlat0_d.z) * u_xlat0_d.y) + 1);
              u_xlat0_d.z = ((-u_xlat0_d.x) + 1);
              u_xlat0_d.y = (u_xlat0_d.y * u_xlat0_d.z);
              u_xlat0_d.xzw = (u_xlat3.xyz * u_xlat0_d.xxx);
              out_f.color.xyz = ((u_xlat0_d.yyy * in_f.color.xyz) + u_xlat0_d.xzw);
          }
          else
          {
              u_xlat0_d.x = (in_f.texcoord5.x==5);
              if((u_xlat0_d.x!=0))
              {
                  u_xlat0_d.xy = ((in_f.texcoord4.xy * float2(0.007812, (-0.007812))) + float2(0, 1));
                  u_xlat0_d = tex2D(_MainTex, u_xlat0_d.xy).yzwx;
                  u_xlat1_d.x = (u_xlat0_d.x>=u_xlat0_d.y);
                  u_xlat1_d.x = u_xlat1_d.x;
                  u_xlat2.xy = u_xlat0_d.yx;
                  u_xlat2.zw = float2(-1, 0.666667);
                  u_xlat4.xy = (u_xlat0_d.xy - u_xlat2.xy);
                  u_xlat4.zw = float2(1, (-1));
                  u_xlat1_d = ((u_xlat1_d.xxxx * u_xlat4) + u_xlat2);
                  u_xlat2.x = (u_xlat0_d.w>=u_xlat1_d.x);
                  u_xlat2.x = u_xlat2.x;
                  u_xlat0_d.xyz = u_xlat1_d.xyw;
                  u_xlat1_d.xyw = u_xlat0_d.wyx;
                  u_xlat1_d = ((-u_xlat0_d) + u_xlat1_d);
                  u_xlat0_d = ((u_xlat2.xxxx * u_xlat1_d) + u_xlat0_d);
                  u_xlat1_d.x = min(u_xlat0_d.y, u_xlat0_d.w);
                  u_xlat0_d.x = (u_xlat0_d.x - u_xlat1_d.x);
                  u_xlat0_d.y = ((-u_xlat0_d.y) + u_xlat0_d.w);
                  u_xlat0_d.w = ((u_xlat0_d.x * 6) + Epsilon.x);
                  u_xlat0_d.y = (u_xlat0_d.y / u_xlat0_d.w);
                  u_xlat0_d.y = (u_xlat0_d.z + u_xlat0_d.y);
                  u_xlat0_d.z = (in_f.texcoord5.y * _Time.x);
                  u_xlat0_d.y = ((abs(u_xlat0_d.y) * 6.283185) + u_xlat0_d.z);
                  u_xlat0_d.y = sin((-u_xlat0_d.y));
                  u_xlat0_d.y = (u_xlat0_d.y + 1);
                  u_xlat0_d.y = (u_xlat0_d.y * 0.5);
                  u_xlat0_d.z = (u_xlat0_d.y * u_xlat0_d.y);
                  u_xlat0_d.y = (((-u_xlat0_d.y) * u_xlat0_d.z) + 1);
                  u_xlat0_d.z = dot(u_xlat3.xyzx, float4(0.3, 0.59, 0.11, 0));
                  u_xlat0_d.z = ((-u_xlat0_d.z) + 1);
                  u_xlat0_d.w = (u_xlat0_d.z * u_xlat0_d.z);
                  u_xlat0_d.z = (u_xlat0_d.z * u_xlat0_d.w);
                  u_xlat0_d.w = ((-u_xlat0_d.y) + 1);
                  u_xlat0_d.z = (u_xlat0_d.w * u_xlat0_d.z);
                  u_xlat0_d.x = (u_xlat0_d.x * u_xlat0_d.z);
                  u_xlat0_d.yzw = (u_xlat3.xyz * u_xlat0_d.yyy);
                  out_f.color.xyz = ((u_xlat0_d.xxx * in_f.color.xyz) + u_xlat0_d.yzw);
              }
              else
              {
                  u_xlat0_d.x = (in_f.texcoord5.x==7);
                  u_xlat0_d.y = (u_xlat3.y>=u_xlat3.z);
                  u_xlat0_d.y = u_xlat0_d.y;
                  u_xlat1_d.xy = u_xlat3.zy;
                  u_xlat1_d.zw = float2(-1, 0.666667);
                  u_xlat2.xy = ((-u_xlat1_d.xy) + u_xlat3.yz);
                  u_xlat2.zw = float2(1, (-1));
                  u_xlat1_d = ((u_xlat0_d.yyyy * u_xlat2) + u_xlat1_d);
                  u_xlat0_d.y = (u_xlat3.x>=u_xlat1_d.x);
                  u_xlat0_d.y = u_xlat0_d.y;
                  u_xlat2.xyz = u_xlat1_d.xyw;
                  u_xlat2.w = u_xlat3.x;
                  u_xlat1_d.xyw = u_xlat2.wyx;
                  u_xlat1_d = ((-u_xlat2) + u_xlat1_d);
                  u_xlat1_d = ((u_xlat0_d.yyyy * u_xlat1_d) + u_xlat2);
                  u_xlat0_d.y = min(u_xlat1_d.y, u_xlat1_d.w);
                  u_xlat0_d.y = ((-u_xlat0_d.y) + u_xlat1_d.x);
                  u_xlat0_d.z = ((-u_xlat1_d.y) + u_xlat1_d.w);
                  u_xlat0_d.w = ((u_xlat0_d.y * 6) + Epsilon.x);
                  u_xlat0_d.z = (u_xlat0_d.z / u_xlat0_d.w);
                  u_xlat0_d.z = (u_xlat1_d.z + u_xlat0_d.z);
                  u_xlat1_d.y = (((-u_xlat0_d.y) * 0.5) + u_xlat1_d.x);
                  u_xlat0_d.w = ((u_xlat1_d.y * 2) - 1);
                  u_xlat0_d.w = ((-abs(u_xlat0_d.w)) + Epsilon.x);
                  u_xlat0_d.w = (u_xlat0_d.w + 1);
                  u_xlat1_d.x = (u_xlat0_d.y / u_xlat0_d.w);
                  u_xlat0_d.y = (in_f.texcoord5.y * _Time.x);
                  u_xlat0_d.y = frac(u_xlat0_d.y);
                  u_xlat0_d.y = (u_xlat0_d.y + abs(u_xlat0_d.z));
                  u_xlat0_d.y = frac(u_xlat0_d.y);
                  u_xlat1_d.xy = saturate(u_xlat1_d.xy);
                  u_xlat0_d.yzw = ((u_xlat0_d.yyy * float3(6, 6, 6)) + float3(0, 4, 2));
                  u_xlat0_d.yzw = (u_xlat0_d.yzw * float3(0.166667, 0.166667, 0.166667));
                  u_xlat2.xyz = (u_xlat0_d.yzw>=(-u_xlat0_d.yzw));
                  u_xlat0_d.yzw = frac(u_xlat0_d.yzw);
                  u_xlat0_d.yzw = (u_xlat2.xyz)?(u_xlat0_d.yzw):((-u_xlat0_d.yzw));
                  u_xlat0_d.yzw = ((u_xlat0_d.yzw * float3(6, 6, 6)) + float3(-3, (-3), (-3)));
                  u_xlat0_d.yzw = saturate((abs(u_xlat0_d.yzw) + float3(-1, (-1), (-1))));
                  u_xlat0_d.yzw = (u_xlat0_d.yzw + float3(-0.5, (-0.5), (-0.5)));
                  u_xlat0_d.yzw = (u_xlat0_d.yzw * u_xlat1_d.xxx);
                  u_xlat1_d.x = ((u_xlat1_d.y * 2) - 1);
                  u_xlat1_d.x = ((-abs(u_xlat1_d.x)) + 1);
                  u_xlat0_d.yzw = ((u_xlat0_d.yzw * u_xlat1_d.xxx) + u_xlat1_d.yyy);
                  out_f.color.xyz = (u_xlat0_d.xxx)?(u_xlat0_d.yzw):(u_xlat3.xyz);
              }
          }
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
