Shader "Custom/Simple Additive" {
    Properties {
        _MainTex ("Base texture", 2D) = "white" {}
        _Paint ("Decal", 2D) = "black" {}
    }
    SubShader {
//        Tags { "Queue" = "Transparent" }
        Pass {
//        	zWrite Off
        	
//            Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex] { combine texture }
            
            SetTexture [_Paint] {
            	combine texture lerp (texture) previous
            }
            
        }
    }
}