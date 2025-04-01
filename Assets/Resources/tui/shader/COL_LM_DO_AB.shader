Shader "Triniti/Scene/COL_LM_DO_AB" {
Properties {
 _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
 _MainTex ("MainTex(RGB)", 2D) = "" {}
 _LightMap ("Lightmap (RGB)", 2D) = "white" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord0
   Bind "texcoord1", TexCoord1
  }
  Color [_Color]
  Fog { Mode Off }
  AlphaTest Greater 0.5
  SetTexture [_MainTex] { combine texture * primary double }
  SetTexture [_LightMap] { combine texture * previous }
 }
}
}