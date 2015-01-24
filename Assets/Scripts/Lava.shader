Shader "Lince/Lava" {
	Properties {
		_Color ("Color", Color) = (1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DetailTex ("Detail", 2D) = "black" {}
		_DetailColor ("Detail Color", Color) = (1,1,1)
		_Speed ("Speed", Range(0.0,1)) = 0.025
		_Turbulence ("Turbulence", Range(0.0,10)) = 0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert noambient noforwardadd

		sampler2D _MainTex;
		sampler2D _DetailTex;
		float4 _DetailColor;
		float4 _Color;
		fixed _Speed;
		fixed _Turbulence;

		struct Input {
			float2 uv_MainTex;
		};

		void vert (inout appdata_full v) {
        //v.vertex.xyz += v.normal;
		v.vertex.x += (_SinTime.z * sin(v.vertex.z))*_Speed;
		v.vertex.z += (_SinTime.z * sin(v.vertex.x))*_Speed;
		v.vertex.y += (sin(_Time.y *_Turbulence * v.vertex.z/1000) * 0.1f);

    }

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			half4 d = tex2D (_DetailTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb ;
			if (d.a > 0) o.Albedo = d.rgb * _DetailColor.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
