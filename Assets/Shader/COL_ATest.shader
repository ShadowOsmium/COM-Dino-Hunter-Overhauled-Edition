Shader "Triniti/Scene/COL_ATest" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("MainTex(RGB)", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  Cull Off
  AlphaTest Greater 0.5
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * constant }
 }
}
}