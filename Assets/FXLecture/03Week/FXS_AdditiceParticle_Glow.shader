// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HW/AdditiveParticle_Glow"
{
	Properties
	{
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("CullMode", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)]_Src("Src", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)]_Dst("Dst", Float) = 0
		_Main_Tex("Main_Tex", 2D) = "white" {}
		[HDR]_Main_Color("Main_Color", Color) = (1,1,1,0)
		_Main_Intencity("Main_Intencity", Range( 1 , 50)) = 1
		_Main_Power("Main_Power", Float) = 2.5
		_Dissolve_Texture("Dissolve_Texture", 2D) = "white" {}
		_Dissolve("Dissolve", Range( -1 , 1)) = 1
		[Toggle(USE_CUSTOMDATA_ON)] Use_Customdata("Use_Customdata", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull [_CullMode]
		ZWrite Off
		Blend [_Src] [_Dst]
		
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local USE_CUSTOMDATA_ON
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float4 uv_tex4coord;
		};

		uniform float _Dst;
		uniform float _Src;
		uniform float _CullMode;
		uniform sampler2D _Main_Tex;
		uniform float4 _Main_Tex_ST;
		uniform float _Main_Power;
		uniform float _Main_Intencity;
		uniform float4 _Main_Color;
		uniform sampler2D _Dissolve_Texture;
		uniform float4 _Dissolve_Texture_ST;
		uniform float _Dissolve;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Main_Tex = i.uv_texcoord * _Main_Tex_ST.xy + _Main_Tex_ST.zw;
			float4 tex2DNode3 = tex2D( _Main_Tex, uv_Main_Tex );
			float4 temp_cast_0 = (_Main_Power).xxxx;
			#ifdef USE_CUSTOMDATA_ON
				float staticSwitch29 = i.uv_tex4coord.w;
			#else
				float staticSwitch29 = _Main_Intencity;
			#endif
			o.Emission = ( i.vertexColor * ( ( pow( tex2DNode3 , temp_cast_0 ) * staticSwitch29 ) * _Main_Color ) ).rgb;
			float2 uv_Dissolve_Texture = i.uv_texcoord * _Dissolve_Texture_ST.xy + _Dissolve_Texture_ST.zw;
			#ifdef USE_CUSTOMDATA_ON
				float staticSwitch24 = i.uv_tex4coord.z;
			#else
				float staticSwitch24 = _Dissolve;
			#endif
			o.Alpha = ( tex2DNode3.r * ( i.vertexColor.a * saturate( ( tex2D( _Dissolve_Texture, uv_Dissolve_Texture ).r + staticSwitch24 ) ) ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18712
0;0;1920;1011;2204.74;405.0895;1.316968;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-1392,400;Inherit;False;0;25;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;23;-1856,304;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-1840,-135;Inherit;True;0;3;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-1523,623;Inherit;False;Property;_Dissolve;Dissolve;9;0;Create;True;0;0;0;False;0;False;1;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;25;-1120,384;Inherit;True;Property;_Dissolve_Texture;Dissolve_Texture;8;0;Create;True;0;0;0;False;0;False;-1;145143eb93006454baed654b5669d0c3;145143eb93006454baed654b5669d0c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1600,-160;Inherit;True;Property;_Main_Tex;Main_Tex;3;0;Create;True;0;0;0;False;0;False;-1;94a224cca5971a540bdc318d7c5abac0;b64befe61f1f91a499b5869845cc1697;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-1520,256;Inherit;False;Property;_Main_Intencity;Main_Intencity;6;0;Create;True;0;0;0;False;0;False;1;1;1;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1507,70;Inherit;False;Property;_Main_Power;Main_Power;7;0;Create;True;0;0;0;False;0;False;2.5;0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;24;-1228,790;Inherit;False;Property;Use_Customdata;Use_Customdata;10;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-801.2217,408.2104;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;29;-1280,240;Inherit;False;Property;Use_Customdata;Use_Customdata;10;0;Create;False;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;6;-1293.654,-154.8797;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1029,-152;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;9;-992,128;Inherit;False;Property;_Main_Color;Main_Color;5;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0.5566038,0.5566038,0.5566038,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;27;-432,415;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;10;-816,-368;Inherit;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-784,-160;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-132,399;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;30;831.3606,-93.65936;Inherit;False;162;292;Enum;3;33;32;31;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;63.12048,181.8887;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-512,-176;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;32;855.3607,108.3406;Inherit;False;Property;_Dst;Dst;2;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;855.3607,27.34064;Inherit;False;Property;_Src;Src;1;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;851.3607,-47.65933;Inherit;False;Property;_CullMode;CullMode;0;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;592.8,-86;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;HW/AdditiveParticle_Glow;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;True;33;10;True;32;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;4;-1;-1;-1;0;False;0;0;True;31;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;1;21;0
WireConnection;3;1;12;0
WireConnection;24;1;22;0
WireConnection;24;0;23;3
WireConnection;37;0;25;1
WireConnection;37;1;24;0
WireConnection;29;1;5;0
WireConnection;29;0;23;4
WireConnection;6;0;3;0
WireConnection;6;1;7;0
WireConnection;4;0;6;0
WireConnection;4;1;29;0
WireConnection;27;0;37;0
WireConnection;8;0;4;0
WireConnection;8;1;9;0
WireConnection;28;0;10;4
WireConnection;28;1;27;0
WireConnection;35;0;3;1
WireConnection;35;1;28;0
WireConnection;11;0;10;0
WireConnection;11;1;8;0
WireConnection;0;2;11;0
WireConnection;0;9;35;0
ASEEND*/
//CHKSM=CDB636FA8A2C1AA6B29235439A8466572111F033