Shader "Custom/FlashAppear" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _Color ("Color", Color) = (1,1,1,1)
 _FlashColor ("FlashColor", Color) = (1,1,1,1)
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Transparent+1" }
 Pass {
  Name "LIGHT"
  Tags { "QUEUE"="Transparent+1" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha

  CGPROGRAM

  #pragma vertex vert
  #pragma fragment frag

  sampler2D _MainTex;

  float4 _Color, _FlashColor;

  struct appdata_t {
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
	float4 vertex : POSITION;
	float3 normal : NORMAL;
  };
  

  struct v2f {
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
	float4 vertex : POSITION;
  };

	float4x4 CalculateInverseTranspose(float4x4 mat)
	{
	    float3x3 upperLeft3x3 = mat;
	    float3x3 inverseTranspose3x3 = transpose(upperLeft3x3);
	    return float4x4(inverseTranspose3x3, 0, 0, 0, 0, 0, 0, 1);
	}

  v2f vert(appdata_t v) {
	v2f o;
  	o.vertex = UnityObjectToClipPos(v.vertex);
    o.color = _FlashColor;
    float4 worldnormal = mul(CalculateInverseTranspose(unity_ObjectToWorld), float4( v.normal, 0.0));
	o.texcoord = v.texcoord;
	return o;
  }

  float4 frag(v2f i) : SV_TARGET {
	float4 tmpvar_2 = tex2D(_MainTex, i.texcoord);
	float4 color_1;
  	color_1.xyz = tmpvar_2.xyz;
  	color_1.w = clamp((1.1 - i.color.w), 0.0, 1.0);
  	return color_1;
  }

  ENDCG
 }
}
}