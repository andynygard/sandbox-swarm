Shader "2D/Sprite"
{   
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }

    Category
    {
        ZWrite Off
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off
         
        SubShader
        {
            Pass
            {
                SetTexture[_MainTex]
            }
        }
    }
}