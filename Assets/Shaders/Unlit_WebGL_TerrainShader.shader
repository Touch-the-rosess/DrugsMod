Shader "Unlit/WebGL/TerrainShader"
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
      uniform float Epsilon;
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
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float2 texcoord4 :TEXCOORD4;
          float2 texcoord5 :TEXCOORD5;
          float4 color :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float2 texcoord2 :TEXCOORD2;
          float2 texcoord3 :TEXCOORD3;
          float2 texcoord4 :TEXCOORD4;
          float2 texcoord5 :TEXCOORD5;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
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
      float4 u_xlat16_0;
      int u_xlati0;
      float2 u_xlatb0;
      float4 u_xlat1_d;
      float4 u_xlat2;
      int u_xlatu2;
      int u_xlatb2;
      float4 u_xlat3;
      int u_xlatb3;
      float4 u_xlat4;
      float4 u_xlatb4;
      float3 u_xlat5;
      float3 u_xlatb5;
      float3 u_xlat16_6;
      float3 u_xlat9;
      int u_xlatb9;
      float2 u_xlat10;
      float3 u_xlat16_13;
      int u_xlati14;
      float2 u_xlat16;
      float u_xlat17;
      int u_xlati21;
      int u_xlatu21;
      float u_xlat22;
      int u_xlatu22;
      float u_xlat23;
      int u_xlatb23;
      float u_xlat24;
      float u_xlat16_24;
      float4 null;
      const int BITWISE_BIT_COUNT = 32;
      int op_modi(int x, int y)
      {
          return (x - (y * (x / y)));
      }
      
      int2 op_modi(int2 a, int2 b)
      {
          a.x = op_modi(a.x, b.x);
          a.y = op_modi(a.y, b.y);
          return a;
      }
      
      int3 op_modi(int3 a, int3 b)
      {
          a.x = op_modi(a.x, b.x);
          a.y = op_modi(a.y, b.y);
          a.z = op_modi(a.z, b.z);
          return a;
      }
      
      int4 op_modi(int4 a, int4 b)
      {
          a.x = op_modi(a.x, b.x);
          a.y = op_modi(a.y, b.y);
          a.z = op_modi(a.z, b.z);
          a.w = op_modi(a.w, b.w);
          return a;
      }
      
      int op_and(int a, int b)
      {
          int result = 0;
          int n = 1;
          int i = 0;
          while((i<BITWISE_BIT_COUNT))
          {
              if(((op_modi(a, 2)!=0) && (op_modi(b, 2)!=0)))
              {
                  result = (result + n);
              }
              a = (a / 2);
              b = (b / 2);
              n = (n * 2);
              if(!((a>0) && (b>0)))
              {
                  break;
              }
              i = (i + 1);
          }
          return result;
      }
      
      int2 op_and(int2 a, int2 b)
      {
          a.x = op_and(a.x, b.x);
          a.y = op_and(a.y, b.y);
          return a;
      }
      
      int3 op_and(int3 a, int3 b)
      {
          a.x = op_and(a.x, b.x);
          a.y = op_and(a.y, b.y);
          a.z = op_and(a.z, b.z);
          return a;
      }
      
      int4 op_and(int4 a, int4 b)
      {
          a.x = op_and(a.x, b.x);
          a.y = op_and(a.y, b.y);
          a.z = op_and(a.z, b.z);
          a.w = op_and(a.w, b.w);
          return a;
      }
      
      int op_xor(int a, int b)
      {
          return ((a + b) - (2 * op_and(a, b)));
      }
      
      int2 op_xor(int2 a, int2 b)
      {
          a.x = op_xor(a.x, b.x);
          a.y = op_xor(a.y, b.y);
          return a;
      }
      
      int3 op_xor(int3 a, int3 b)
      {
          a.x = op_xor(a.x, b.x);
          a.y = op_xor(a.y, b.y);
          a.z = op_xor(a.z, b.z);
          return a;
      }
      
      int4 op_xor(int4 a, int4 b)
      {
          a.x = op_xor(a.x, b.x);
          a.y = op_xor(a.y, b.y);
          a.z = op_xor(a.z, b.z);
          a.w = op_xor(a.w, b.w);
          return a;
      }
      
      float trunc(float x)
      {
          return (sign(x) * floor(abs(x)));
      }
      
      float2 trunc(float2 x)
      {
          return (sign(x) * floor(abs(x)));
      }
      
      float3 trunc(float3 x)
      {
          return (sign(x) * floor(abs(x)));
      }
      
      float4 trunc(float4 x)
      {
          return (sign(x) * floor(abs(x)));
      }
      
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlatb0.xy = bool4(in_f.texcoord5.xxxx == float4(3, 2, 0, 0)).xy;
          if(u_xlatb0.x)
          {
              u_xlat0_d.x = (in_f.texcoord5.y * _Time.x);
              u_xlat0_d.x = (u_xlat0_d.x * 400.200012);
              u_xlati0 = int(u_xlat0_d.x);
              u_xlati14 = op_xor(u_xlati0, 6);
              u_xlatu21 = int(max(float(u_xlati0), (-float(u_xlati0))));
              u_xlat16_0.w = float((int(u_xlatu21) / 6));
              u_xlat16_0.w = float(op_modi(int(null.w), int(u_xlatu21)));
              u_xlat1_d.x = float((0 - int(u_xlat16_0.w)));
              u_xlati14 = op_and(u_xlati14, (-2147483648));
              u_xlat16_0.z = ((u_xlati14!=0))?(u_xlat1_d.x):(u_xlat16_0.w);
              u_xlati0 = (((-6) * int(u_xlat16_0.z)) + u_xlati0);
              u_xlat0_d.x = float(u_xlati0);
              u_xlat1_d.z = (u_xlat0_d.x + in_f.texcoord3.x);
              u_xlat1_d.xy = in_f.texcoord2.xy;
          }
          else
          {
              u_xlatb0.x = (in_f.texcoord5.x==4);
              if(u_xlatb0.x)
              {
                  u_xlat0_d.x = (in_f.texcoord5.y * _Time.x);
                  u_xlat0_d.x = (u_xlat0_d.x * 400.200012);
                  u_xlati0 = int(u_xlat0_d.x);
                  u_xlati14 = op_xor(u_xlati0, 6);
                  u_xlatu21 = int(max(float(u_xlati0), (-float(u_xlati0))));
                  u_xlat16_0.w = float((int(u_xlatu21) / 6));
                  u_xlat16_0.w = float(op_modi(int(null.w), int(u_xlatu21)));
                  u_xlat22 = float((0 - int(u_xlat16_0.w)));
                  u_xlati14 = op_and(u_xlati14, (-2147483648));
                  u_xlat16_0.z = ((u_xlati14!=0))?(u_xlat22):(u_xlat16_0.w);
                  u_xlati0 = (((-6) * int(u_xlat16_0.z)) + u_xlati0);
                  u_xlat0_d.x = float(u_xlati0);
                  u_xlat1_d.z = ((u_xlat0_d.x * 2) + in_f.texcoord3.x);
                  u_xlat1_d.xy = in_f.texcoord2.xy;
              }
              else
              {
                  u_xlat0_d.x = (in_f.texcoord5.y * _Time.x);
                  u_xlat0_d.x = (u_xlat0_d.x * 400.200012);
                  u_xlati0 = int(u_xlat0_d.x);
                  u_xlati14 = int(in_f.texcoord4.y);
                  u_xlati21 = op_xor(u_xlati14, u_xlati0);
                  u_xlatu22 = int(max(float(u_xlati0), (-float(u_xlati0))));
                  u_xlatu2 = int(max(float(u_xlati14), (-float(u_xlati14))));
                  u_xlatu22 = int(op_modi(int(null.x), int(u_xlatu22)));
                  u_xlatu22 = (int(u_xlatu22) / int(u_xlatu2));
                  u_xlatu2 = int((0 - int(u_xlatu22)));
                  u_xlati21 = op_and(u_xlati21, (-2147483648));
                  u_xlatu21 = ((u_xlati21!=0))?(int(u_xlatu2)):(int(u_xlatu22));
                  u_xlati0 = (((-u_xlati14) * int(u_xlatu21)) + u_xlati0);
                  u_xlat0_d.x = float(u_xlati0);
                  u_xlat16_0.zw = float2((int2(bool4(in_f.texcoord5.xxxx == float4(0, 0, 6, 8)).zw) * (-1)));
                  u_xlat2.xy = ((in_f.texcoord4.xx * u_xlat0_d.xx) + in_f.texcoord2.yx);
                  u_xlat3.x = ((int(u_xlat16_0.w)!=0))?(u_xlat2.y):(in_f.texcoord2.x);
                  u_xlat2.z = in_f.texcoord2.x;
                  u_xlat3.y = in_f.texcoord2.y;
                  u_xlat1_d.xy = ((int(u_xlat16_0.z)!=0))?(u_xlat2.zx):(u_xlat3.xy);
                  u_xlat1_d.z = in_f.texcoord3.x;
              }
          }
          u_xlat16_0.x = float(((in_f.texcoord1.x==0))?((-1)):(0));
          if((int(u_xlat16_0.x)!=0))
          {
              u_xlat0_d.x = (u_xlat1_d.x * 0.0078125);
              u_xlat0_d.z = (((-u_xlat1_d.y) * 0.0078125) + 1);
              u_xlat2 = tex2D(_MainTex, u_xlat0_d.xz);
              u_xlat0_d.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
              u_xlat0_d.xzw = (u_xlat0_d.xxx * u_xlat2.xyz);
              u_xlat16_0.xzw = u_xlat0_d.xzw;
              out_f.color.w = u_xlat2.w;
          }
          else
          {
              u_xlatb2 = (in_f.texcoord1.x==4);
              if(u_xlatb2)
              {
                  u_xlat2.x = (u_xlat1_d.x * 0.0078125);
                  u_xlat2.z = (((-u_xlat1_d.y) * 0.0078125) + 1);
                  u_xlat2 = tex2D(_MainTex, u_xlat2.xz);
                  u_xlat16_0.xzw = u_xlat2.xyz;
                  out_f.color.w = u_xlat2.w;
              }
              else
              {
                  u_xlatb2 = (in_f.texcoord1.x==1);
                  if(u_xlatb2)
                  {
                      u_xlat2.x = (u_xlat1_d.x * 0.0078125);
                      u_xlat2.z = (((-u_xlat1_d.y) * 0.0078125) + 1);
                      u_xlat2 = tex2D(_MainTex, u_xlat2.xz);
                      u_xlat3.x = ((-in_f.texcoord1.y) + 15);
                      u_xlat10.xy = (in_f.texcoord.xy * in_f.texcoord.xy);
                      u_xlat10.x = max(u_xlat10.y, u_xlat10.x);
                      u_xlat10.x = ((-u_xlat10.x) + 1);
                      u_xlat17 = (u_xlat10.x * u_xlat10.x);
                      u_xlat10.x = (u_xlat17 * u_xlat10.x);
                      u_xlat17 = ((-in_f.texcoord.y) + in_f.texcoord.x);
                      u_xlat24 = (in_f.texcoord.y + in_f.texcoord.x);
                      u_xlat3.x = trunc(u_xlat3.x);
                      u_xlat4.xyz = (u_xlat3.xxx * float3(0.25, 0.125, 0.0625));
                      u_xlat4.xyz = frac(u_xlat4.xyz);
                      u_xlatb4.xyz = bool4(u_xlat4.xyzx >= float4(0.5, 0.5, 0.5, 0)).xyz;
                      u_xlatb3 = (0<u_xlat17);
                      u_xlatb4.x = (int(u_xlatb3) && u_xlatb4.x);
                      u_xlatb4.y = (int(u_xlatb3) && u_xlatb4.y);
                      u_xlatb3 = (0<u_xlat24);
                      u_xlatb3 = (u_xlatb3 && u_xlatb4.x);
                      u_xlat5.xyz = (u_xlat2.xyz * u_xlat10.xxx);
                      u_xlat16_6.xyz = (int(u_xlatb3))?(u_xlat5.xyz):(u_xlat2.xyz);
                      u_xlatb2 = (u_xlat24<0);
                      u_xlatb9 = (u_xlatb2 && u_xlatb4.y);
                      u_xlat4.xyw = (u_xlat10.xxx * u_xlat16_6.xyz);
                      u_xlat16_6.xyz = (int(u_xlatb9))?(u_xlat4.xyw):(u_xlat16_6.xyz);
                      u_xlatb9 = (u_xlat17<0);
                      u_xlatb9 = (u_xlatb9 && u_xlatb4.z);
                      u_xlatb2 = (u_xlatb2 && u_xlatb9);
                      u_xlat3.xyz = (u_xlat10.xxx * u_xlat16_6.xyz);
                      u_xlat16_0.xzw = (int(u_xlatb2))?(u_xlat3.xyz):(u_xlat16_6.xyz);
                      out_f.color.w = u_xlat2.w;
                  }
                  else
                  {
                      u_xlatb2 = (in_f.texcoord1.x==3);
                      if(u_xlatb2)
                      {
                          out_f.color.w = 0;
                      }
                      else
                      {
                          u_xlatb2 = (in_f.texcoord1.x>=32);
                          u_xlatb9 = (in_f.texcoord1.x==5);
                          u_xlatb2 = (u_xlatb9 || u_xlatb2);
                          if(u_xlatb2)
                          {
                              u_xlat2.xy = (u_xlat1_d.zx * float2(0.0078125, 0.0078125));
                              u_xlat2.w = (((-u_xlat1_d.y) * 0.0078125) + 1);
                              u_xlat3 = tex2D(_MainTex, u_xlat2.yw);
                              u_xlat16.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
                              u_xlat3.xyz = (u_xlat16.xxx * u_xlat3.xyz);
                              u_xlat2.y = ((in_f.texcoord3.y * (-0.0078125)) + 1);
                              u_xlat2 = tex2D(_MainTex, u_xlat2.xy);
                              u_xlat16_6.x = ((-u_xlat2.w) + 1);
                              u_xlat16_6.xyz = (u_xlat3.xyz * u_xlat16_6.xxx);
                              u_xlat16_0.xzw = ((u_xlat2.xyz * u_xlat2.www) + u_xlat16_6.xyz);
                              out_f.color.w = u_xlat3.w;
                          }
                          else
                          {
                              u_xlatb2 = (in_f.texcoord1.x>=16);
                              if(u_xlatb2)
                              {
                                  u_xlat2.xy = (u_xlat1_d.zx * float2(0.0078125, 0.0078125));
                                  u_xlat2.w = (((-u_xlat1_d.y) * 0.0078125) + 1);
                                  u_xlat1_d = tex2D(_MainTex, u_xlat2.yw);
                                  u_xlat16.x = (((-in_f.texcoord1.y) * in_f.texcoord1.y) + 1);
                                  u_xlat3.xyz = (u_xlat1_d.xyz * u_xlat16.xxx);
                                  u_xlat2.y = ((in_f.texcoord3.y * (-0.0078125)) + 1);
                                  u_xlat2.xyz = tex2D(_MainTex, u_xlat2.xy).xyz;
                                  u_xlat4.xy = ((in_f.texcoord.xy * float2(-0.00552427163, (-0.00552427163))) + float2(0.26171875, 0.98828125));
                                  u_xlat23 = tex2D(_MainTex, u_xlat4.xy).w;
                                  u_xlat24 = (in_f.texcoord1.x + (-16));
                                  u_xlat24 = trunc(u_xlat24);
                                  u_xlat4 = (float4(u_xlat24, u_xlat24, u_xlat24, u_xlat24) * float4(0.5, 0.25, 0.125, 0.0625));
                                  u_xlat4 = frac(u_xlat4);
                                  u_xlatb4 = bool4(u_xlat4 >= float4(0.5, 0.5, 0.5, 0.5));
                                  if(u_xlatb4.x)
                                  {
                                      u_xlat5.xy = ((in_f.texcoord.xy * float2(-0.00552427163, (-0.00552427163))) + float2(0.26171875, 0.9921875));
                                      u_xlat24 = tex2D(_MainTex, u_xlat5.xy).w;
                                      u_xlat24 = (u_xlat23 + u_xlat24);
                                      u_xlat16_24 = u_xlat24;
                                  }
                                  else
                                  {
                                      u_xlat16_24 = u_xlat23;
                                  }
                                  if(u_xlatb4.y)
                                  {
                                      u_xlat4.xy = ((in_f.texcoord.xy * float2(-0.00552427163, (-0.00552427163))) + float2(0.265625, 0.98828125));
                                      u_xlat23 = tex2D(_MainTex, u_xlat4.xy).w;
                                      u_xlat24 = (u_xlat23 + u_xlat16_24);
                                      u_xlat16_24 = u_xlat24;
                                  }
                                  if(u_xlatb4.z)
                                  {
                                      u_xlat4.xy = ((in_f.texcoord.xy * float2(-0.00552427163, (-0.00552427163))) + float2(0.26171875, 0.984375));
                                      u_xlat23 = tex2D(_MainTex, u_xlat4.xy).w;
                                      u_xlat24 = (u_xlat23 + u_xlat16_24);
                                      u_xlat16_24 = u_xlat24;
                                  }
                                  if(u_xlatb4.w)
                                  {
                                      u_xlat4.xy = ((in_f.texcoord.xy * float2(-0.00552427163, (-0.00552427163))) + float2(0.2578125, 0.98828125));
                                      u_xlat23 = tex2D(_MainTex, u_xlat4.xy).w;
                                      u_xlat24 = (u_xlat23 + u_xlat16_24);
                                      u_xlat16_24 = u_xlat24;
                                  }
                                  u_xlatb23 = (1<u_xlat16_24);
                                  u_xlat16_6.x = (u_xlatb23)?(1):(u_xlat16_24);
                                  u_xlat16_13.x = ((-u_xlat16_6.x) + 1);
                                  u_xlat16_13.xyz = (u_xlat3.xyz * u_xlat16_13.xxx);
                                  u_xlat16_0.xzw = ((u_xlat2.xyz * u_xlat16_6.xxx) + u_xlat16_13.xyz);
                                  out_f.color.w = u_xlat1_d.w;
                              }
                              else
                              {
                                  u_xlat16_0.x = float(1);
                                  u_xlat16_0.z = float(1);
                                  u_xlat16_0.w = float(1);
                                  out_f.color.w = 1;
                              }
                          }
                      }
                  }
              }
          }
          if(u_xlatb0.y)
          {
              u_xlat2.xy = ((in_f.texcoord4.xy * float2(0.0078125, (-0.0078125))) + float2(0, 1));
              u_xlat1_d.xyw = tex2D(_MainTex, u_xlat2.xy).yzx;
              u_xlatb2 = (u_xlat1_d.x>=u_xlat1_d.y);
              u_xlat2.x = (u_xlatb2)?(1):(float(0));
              u_xlat3.xy = u_xlat1_d.yx;
              u_xlat3.z = float(-1);
              u_xlat3.w = float(0.666666687);
              u_xlat4.xy = (u_xlat1_d.xy + (-u_xlat3.xy));
              u_xlat4.z = float(1);
              u_xlat4.w = float(-1);
              u_xlat2 = ((u_xlat2.xxxx * u_xlat4) + u_xlat3);
              u_xlatb4.x = (u_xlat1_d.w>=u_xlat2.x);
              u_xlat4.x = (u_xlatb4.x)?(1):(float(0));
              u_xlat1_d.xyz = u_xlat2.xyw;
              u_xlat2.xyw = u_xlat1_d.wyx;
              u_xlat2 = ((-u_xlat1_d) + u_xlat2);
              u_xlat1_d = ((u_xlat4.xxxx * u_xlat2) + u_xlat1_d);
              u_xlat2.x = min(u_xlat1_d.y, u_xlat1_d.w);
              u_xlat2.x = (u_xlat1_d.x + (-u_xlat2.x));
              u_xlat9.x = ((-u_xlat1_d.y) + u_xlat1_d.w);
              u_xlat2.x = ((u_xlat2.x * 6) + Epsilon);
              u_xlat2.x = (u_xlat9.x / u_xlat2.x);
              u_xlat2.x = (u_xlat1_d.z + u_xlat2.x);
              u_xlat9.x = (in_f.texcoord5.y * _Time.x);
              u_xlat2.x = ((abs(u_xlat2.x) * 6.28318548) + u_xlat9.x);
              u_xlat2.x = sin((-u_xlat2.x));
              u_xlat2.x = (u_xlat2.x + 1);
              u_xlat2.x = (u_xlat2.x * 0.5);
              u_xlat9.x = (u_xlat2.x * u_xlat2.x);
              u_xlat2.x = (((-u_xlat2.x) * u_xlat9.x) + 1);
              u_xlat9.x = dot(u_xlat16_0.xzw, float3(0.300000012, 0.589999974, 0.109999999));
              u_xlat9.x = ((-u_xlat9.x) + 1);
              u_xlat16.x = (u_xlat9.x * u_xlat9.x);
              u_xlat9.x = (((-u_xlat16.x) * u_xlat9.x) + 1);
              u_xlat16.x = ((-u_xlat2.x) + 1);
              u_xlat9.x = (u_xlat9.x * u_xlat16.x);
              u_xlat2.xzw = (u_xlat16_0.xzw * u_xlat2.xxx);
              u_xlat2.xyz = ((u_xlat9.xxx * in_f.color.xyz) + u_xlat2.xzw);
              out_f.color.xyz = u_xlat2.xyz;
          }
          else
          {
              u_xlatb2 = (in_f.texcoord5.x==5);
              if(u_xlatb2)
              {
                  u_xlat2.xy = ((in_f.texcoord4.xy * float2(0.0078125, (-0.0078125))) + float2(0, 1));
                  u_xlat1_d.xyw = tex2D(_MainTex, u_xlat2.xy).yzx;
                  u_xlatb2 = (u_xlat1_d.x>=u_xlat1_d.y);
                  u_xlat2.x = (u_xlatb2)?(1):(float(0));
                  u_xlat3.xy = u_xlat1_d.yx;
                  u_xlat3.z = float(-1);
                  u_xlat3.w = float(0.666666687);
                  u_xlat4.xy = (u_xlat1_d.xy + (-u_xlat3.xy));
                  u_xlat4.z = float(1);
                  u_xlat4.w = float(-1);
                  u_xlat2 = ((u_xlat2.xxxx * u_xlat4) + u_xlat3);
                  u_xlatb4.x = (u_xlat1_d.w>=u_xlat2.x);
                  u_xlat4.x = (u_xlatb4.x)?(1):(float(0));
                  u_xlat1_d.xyz = u_xlat2.xyw;
                  u_xlat2.xyw = u_xlat1_d.wyx;
                  u_xlat2 = ((-u_xlat1_d) + u_xlat2);
                  u_xlat1_d = ((u_xlat4.xxxx * u_xlat2) + u_xlat1_d);
                  u_xlat2.x = min(u_xlat1_d.y, u_xlat1_d.w);
                  u_xlat2.x = (u_xlat1_d.x + (-u_xlat2.x));
                  u_xlat9.x = ((-u_xlat1_d.y) + u_xlat1_d.w);
                  u_xlat16.x = ((u_xlat2.x * 6) + Epsilon);
                  u_xlat9.x = (u_xlat9.x / u_xlat16.x);
                  u_xlat9.x = (u_xlat1_d.z + u_xlat9.x);
                  u_xlat16.x = (in_f.texcoord5.y * _Time.x);
                  u_xlat9.x = ((abs(u_xlat9.x) * 6.28318548) + u_xlat16.x);
                  u_xlat9.x = sin((-u_xlat9.x));
                  u_xlat9.x = (u_xlat9.x + 1);
                  u_xlat9.x = (u_xlat9.x * 0.5);
                  u_xlat16.x = (u_xlat9.x * u_xlat9.x);
                  u_xlat9.x = (((-u_xlat9.x) * u_xlat16.x) + 1);
                  u_xlat16.x = dot(u_xlat16_0.xzw, float3(0.300000012, 0.589999974, 0.109999999));
                  u_xlat16.x = ((-u_xlat16.x) + 1);
                  u_xlat23 = (u_xlat16.x * u_xlat16.x);
                  u_xlat16.x = (u_xlat16.x * u_xlat23);
                  u_xlat23 = ((-u_xlat9.x) + 1);
                  u_xlat16.x = (u_xlat23 * u_xlat16.x);
                  u_xlat2.x = (u_xlat2.x * u_xlat16.x);
                  u_xlat9.xyz = (u_xlat16_0.xzw * u_xlat9.xxx);
                  u_xlat2.xyz = ((u_xlat2.xxx * in_f.color.xyz) + u_xlat9.xyz);
                  out_f.color.xyz = u_xlat2.xyz;
              }
              else
              {
                  u_xlatb2 = (in_f.texcoord5.x==7);
                  u_xlatb9 = (u_xlat16_0.z>=u_xlat16_0.w);
                  u_xlat9.x = (u_xlatb9)?(1):(float(0));
                  u_xlat16.xy = ((-u_xlat16_0.wz) + u_xlat16_0.zw);
                  u_xlat4.x = float(1);
                  u_xlat4.y = float(-1);
                  u_xlat1_d.xy = ((u_xlat9.xx * u_xlat16.xy) + u_xlat16_0.wz);
                  u_xlat1_d.zw = ((u_xlat9.xx * u_xlat4.xy) + float2(-1, 0.666666687));
                  u_xlatb9 = (u_xlat16_0.x>=u_xlat1_d.x);
                  u_xlat9.x = (u_xlatb9)?(1):(float(0));
                  u_xlat3.xyz = (-u_xlat1_d.xyw);
                  u_xlat3.w = (-u_xlat16_0.x);
                  u_xlat4.x = (u_xlat16_0.x + u_xlat3.x);
                  u_xlat4.yzw = (u_xlat1_d.yzx + u_xlat3.yzw);
                  u_xlat4.xyz = ((u_xlat9.xxx * u_xlat4.xyz) + u_xlat1_d.xyw);
                  u_xlat9.x = ((u_xlat9.x * u_xlat4.w) + u_xlat16_0.x);
                  u_xlat16.x = min(u_xlat4.y, u_xlat9.x);
                  u_xlat16.x = ((-u_xlat16.x) + u_xlat4.x);
                  u_xlat9.x = ((-u_xlat4.y) + u_xlat9.x);
                  u_xlat23 = ((u_xlat16.x * 6) + Epsilon);
                  u_xlat9.x = (u_xlat9.x / u_xlat23);
                  u_xlat9.x = (u_xlat4.z + u_xlat9.x);
                  u_xlat4.y = (((-u_xlat16.x) * 0.5) + u_xlat4.x);
                  u_xlat23 = ((u_xlat4.y * 2) + (-1));
                  u_xlat23 = ((-abs(u_xlat23)) + Epsilon);
                  u_xlat23 = (u_xlat23 + 1);
                  u_xlat4.x = (u_xlat16.x / u_xlat23);
                  u_xlat16.x = (in_f.texcoord5.y * _Time.x);
                  u_xlat16.x = frac(u_xlat16.x);
                  u_xlat9.x = (u_xlat16.x + abs(u_xlat9.x));
                  u_xlat9.x = frac(u_xlat9.x);
                  u_xlat4.xy = u_xlat4.xy;
                  u_xlat4.xy = clamp(u_xlat4.xy, 0, 1);
                  u_xlat9.xyz = ((u_xlat9.xxx * float3(6, 6, 6)) + float3(0, 4, 2));
                  u_xlat9.xyz = (u_xlat9.xyz * float3(0.166666672, 0.166666672, 0.166666672));
                  u_xlatb5.xyz = bool4(u_xlat9.xyzx >= (-u_xlat9.xyzx)).xyz;
                  u_xlat9.xyz = frac(u_xlat9.xyz);
                  float3 hlslcc_movcTemp = u_xlat9;
                  hlslcc_movcTemp.x = (u_xlatb5.x)?(u_xlat9.x):((-u_xlat9.x));
                  hlslcc_movcTemp.y = (u_xlatb5.y)?(u_xlat9.y):((-u_xlat9.y));
                  hlslcc_movcTemp.z = (u_xlatb5.z)?(u_xlat9.z):((-u_xlat9.z));
                  u_xlat9 = hlslcc_movcTemp;
                  u_xlat9.xyz = ((u_xlat9.xyz * float3(6, 6, 6)) + float3(-3, (-3), (-3)));
                  u_xlat9.xyz = (abs(u_xlat9.xyz) + float3(-1, (-1), (-1)));
                  u_xlat9.xyz = clamp(u_xlat9.xyz, 0, 1);
                  u_xlat9.xyz = (u_xlat9.xyz + float3(-0.5, (-0.5), (-0.5)));
                  u_xlat9.xyz = (u_xlat9.xyz * u_xlat4.xxx);
                  u_xlat4.x = ((u_xlat4.y * 2) + (-1));
                  u_xlat4.x = ((-abs(u_xlat4.x)) + 1);
                  u_xlat9.xyz = ((u_xlat9.xyz * u_xlat4.xxx) + u_xlat4.yyy);
                  out_f.color.xyz = (int(u_xlatb2))?(u_xlat9.xyz):(u_xlat16_0.xzw);
              }
              return out_f;
          }
		   return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
