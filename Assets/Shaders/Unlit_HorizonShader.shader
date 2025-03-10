Shader "Unlit/HorizonShader"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "RenderType" = "Opaque"
    }
    LOD 100
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "RenderType" = "Opaque"
      }
      LOD 100
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      uniform float4 _MainTex_ST;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
          float3 vs_TANGENT :TANGENT;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
          float3 vs_TANGENT :TANGENT;
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
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.vs_TANGENT.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float4 u_xlat1_d;
      float4 u_xlat2;
      float4 u_xlat3;
      float4 u_xlat4;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.x = (in_f.vs_TANGENT.y - 4);
          u_xlat0_d.y = (u_xlat0_d.x * (-0.152003));
          u_xlat0_d.y = exp(u_xlat0_d.y);
          u_xlat1_d.xyz = (u_xlat0_d.yyy * float3(0.456, 0.528, 0.156));
          u_xlat0_d.z = (in_f.texcoord.x * 80);
          u_xlat0_d.z = ((in_f.vs_TANGENT.x * 0.025) + u_xlat0_d.z);
          u_xlat0_d.w = (u_xlat0_d.z * 0.2236);
          u_xlat0_d.w = sin(u_xlat0_d.w);
          u_xlat0_d.w = ((u_xlat0_d.w * 3) + u_xlat0_d.z);
          u_xlat2.x = (u_xlat0_d.w * 0.912542);
          u_xlat2.x = sin(u_xlat2.x);
          u_xlat0_d.w = ((u_xlat2.x * 1.2) + u_xlat0_d.w);
          u_xlat2.x = (u_xlat0_d.w * 0.091144);
          u_xlat2.x = sin(u_xlat2.x);
          u_xlat0_d.w = (u_xlat0_d.w + u_xlat2.x);
          u_xlat2 = (u_xlat0_d.wwww * float4(0.4236, 0.74236, 0.174236, 0.154424));
          float _tmp_dvx_0 = sin(u_xlat2);
          u_xlat2 = float4(_tmp_dvx_0, _tmp_dvx_0, _tmp_dvx_0, _tmp_dvx_0);
          u_xlat2.xy = (u_xlat2.yw + u_xlat2.xy);
          u_xlat2.x = (u_xlat2.z + u_xlat2.x);
          u_xlat2.y = (u_xlat2.y + 3);
          u_xlat2.z = (u_xlat0_d.w * 0.923159);
          u_xlat2.z = sin(u_xlat2.z);
          u_xlat2.y = (u_xlat2.z + u_xlat2.y);
          u_xlat2.x = (u_xlat2.y * u_xlat2.x);
          u_xlat2.x = ((u_xlat2.x * 0.1) + 5.5);
          u_xlat2.x = (in_f.vs_TANGENT.y<u_xlat2.x);
          if((u_xlat2.x!=0))
          {
              u_xlat0_d.y = (u_xlat0_d.y * 1.2);
              u_xlat2.x = (u_xlat0_d.y * u_xlat0_d.y);
              u_xlat2.x = (u_xlat2.x * u_xlat2.x);
              u_xlat2.y = (u_xlat0_d.y * u_xlat2.x);
              u_xlat3.xyz = (u_xlat0_d.zzz * float3(0.2, 0.04, 0.18));
              u_xlat3.w = ((in_f.vs_TANGENT.y * 0.3) - 1.2);
              u_xlat4 = tex2D(_MainTex, u_xlat3.xw);
              u_xlat3.y = ((u_xlat0_d.x * 0.3) - u_xlat3.y);
              u_xlat3.x = ((u_xlat3.y * 2) + u_xlat3.z);
              u_xlat3 = tex2D(_MainTex, u_xlat3.xy);
              u_xlat3 = max(u_xlat3, u_xlat4);
              u_xlat0_d.x = (u_xlat0_d.w + in_f.vs_TANGENT.y);
              u_xlat0_d.x = (u_xlat0_d.x * u_xlat0_d.w);
              u_xlat0_d.x = (u_xlat0_d.x * in_f.vs_TANGENT.y);
              u_xlat0_d.x = (u_xlat0_d.x * 5);
              u_xlat0_d.x = sin(u_xlat0_d.x);
              u_xlat0_d.x = ((u_xlat0_d.x * 0.05) + 0.33);
              u_xlat0_d.y = (((-u_xlat0_d.y) * u_xlat2.x) + 1);
              u_xlat1_d.w = 1;
              u_xlat4.x = (u_xlat0_d.x * 0.8);
              u_xlat4.yzw = float3(0.304, 0.088, 0.8);
              u_xlat3 = ((u_xlat3 * float4(0.2, 0.2, 0.2, 0.2)) + u_xlat4);
              u_xlat2 = (u_xlat2.yyyy * u_xlat3);
              out_f.color = ((u_xlat0_d.yyyy * u_xlat1_d) + u_xlat2);
          }
          else
          {
              out_f.color.xyz = u_xlat1_d.xyz;
              out_f.color.w = 1;
          }
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
