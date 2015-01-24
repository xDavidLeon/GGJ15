Shader "Lince/Detail" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DetailTex ("Detail", 2D) = "black" {}
		_DetailColor ("Detail Color", Color) = (1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _DetailTex;
		float4 _DetailColor;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 d = tex2D (_DetailTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb;
			if (d.a > 0) o.Albedo = d.rgb * _DetailColor.rgb;
			o.Alpha = c.a;
			if (o.Alpha < 0.5) discard;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
