Shader "Triniti/Particle/AA_COL_DOBWRC" {
Properties {
 _Color ("Main Color", Color) = (0.5,0.5,0.5,0.5)
 _MainTex ("Particle Texture", 2D) = "white" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "color", Color
   Bind "texcoord", TexCoord
  }
  ZWrite Off
  Cull Off
  Fog {
   Color (0,0,0,0)
  }
  Blend SrcAlpha One

  CGPROGRAM
  #pragma vertex vert
  #pragma fragment frag

  sampler2D _MainTex;
  float4 _Color;

  struct v2f {
    float4 vertex : POSITION;
    float2 texcoord : TEXCOORD;
  };

  struct appdata_t {
    float4 vertex : POSITION;
    float2 texcoord : TEXCOORD;
  };

  v2f vert(appdata_t v) {
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.texcoord = v.texcoord;
    return o;
  }

  float4 frag(v2f i) : SV_TARGET {
    float4 color = tex2D(_MainTex, i.texcoord);
    return float4((((color.x * 0.1) + (color.y * 0.6)) + (color.z * 0.3)).xxx, color.w) * _Color;
  }
  ENDCG
 }
}
}