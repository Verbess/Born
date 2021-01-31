// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/* 语言:
    1. CG
        即 Nvdia 的 C for Graphic
        真正的跨平台
    2. HLSL
        即 High Level Shading Language
        由微软搞的
    3. GLSL
        即 OpenGL Shading Language
        虽跨平台，但缺少一些显卡编译
*/

/* 术语:
    NDC = Normalize Device Coordinates
*/

/* Shader 种类:
    1. Fixed Function Shader
        选项式，不可用程式码
        完全使用 ShaderLab 命令
    2. Surface Shader
        可用程式码
        写在CG区块间
        自动编译成多个Pass
    3. Vertex and Fragment Shader
        可用程式码(CG Program)
        最灵活，但语法复杂
        可实现特别效果
*/


// ---- Test Shader ----
/*
Shader "Custom/TestShader" {

    Properties {
        
        // Numbers and Sliders
        _Int ("int", Int) = 3
        _Float ("float", Float) = 1.5
        _Range ("Range", Range(0.0, 5.0)) = 3.0

        // Color & Vector
        _Color ("Color", Color) = (0.5, 0.2, 0.8, 1)
        _Vector ("Vector", Vector) = (2, 0, 8, 4)

        // Texture
        _2D ("2D", 2D) = "" {}
        _Cube ("Cube", Cube) = "White" {}
        _3D ("3D", 3D) = "Black" {}

    }

    SubShader {

        // Tags
        Tags {
            // Queue
            //     渲染顺序，值越小越优先: 
            //     "Background" == 1000, 用于 Skybox 或背景
            //     "Geometry" == 2000,  （默认值）用于绝大多数物体
            //     "AlphaTest" == 2450,  渲染经过AlphaTest的像素
            //     "Transparent" = 3000,  按Z轴从后往前渲染透明物体
            //     "Overlay" == 4000 渲染叠加效果，例如镜头光晕
            "Queue" = "Geometry"
            // "Queue" = "Geometry + 20"

            // RenderType
            //   Camera.RenderWithShader 和 Camera.SetReplacementShader 都是在Tag里寻找RenderType，如果有则取代
            //
            "RenderType" = "Opaque"
        }

        // LOD = Level of Detail 细节数
        LOD 100

        // RenderSetup
        Cull Back // Front / Back / Off
        ZTest Less // Less / Greater / LEqual / GEqual / Equal / NotEqual / Always
        ZWrite On // On / Off
        // Blend DstFactor // SrcFactor / DstFactor

        // UsePass 取其它的UnityShader文件里的Pass来用
        UsePass "Custom/TESTSHADER" // 必须大写

        // GrabPass 把当前渲染结果存到 Texture 里再处理
        GrabPass {
            "Texture Name"
        }

        // Pass
        Pass {

            // 里面依然可以写 Tags / Names / RenderSetup
            Name "PASS1"

        }
        
    }

    // 假如上述的不生效，执行最低级的Shader
    // 同时它也可以作为阴影投射
    FallBack "Diffuse"
}
*/

Shader "Custom/SampleMyOutline" {

    Properties {

        _Texture ("Texture2D", 2D) = "White" {}
        _Outline ("Outline", Range(0, 0.025)) = 0.0125
        _OutlineColor ("OutlineColor", Color) = (0, 0, 0.5, 1)
 
    }

    SubShader {

        Tags {
            "Queue" = "Geometry"
            "RenderType" = "Transparent" // 不透明体
        }

        LOD 500

        // 染色
        Pass {

            Cull Front

            CGPROGRAM
            // vertex 是顶点坐标
            // fragment 是颜色块
            #pragma vertex vert
            #pragma fragment frag

            fixed _Outline;
            fixed4 _OutlineColor;

            struct appData {
                fixed4 vertex : POSITION;
                fixed4 normal : NORMAL;
            };

            struct v2f {
                fixed4 vertex : SV_POSITION;
            }; 

            v2f vert(appData d) {
                v2f o;
                d.vertex.xyz += d.normal * _Outline;
                o.vertex = UnityObjectToClipPos(d.vertex);
                return o;
            }

            fixed4 frag(v2f v) : SV_TARGET {
                return _OutlineColor;
            }

            ENDCG
 
        }

        // 显示未染色的模型
        Pass {

            Tags {
                "Queue" = "Geometry"
                "RenderType" = "Opaque" // 不透明体
            }

            // Cull Front

            CGPROGRAM

            // 定义
            // vertex 变量一定得叫 vert
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _Texture;

            // Unity输入的数据类型
            struct appData {
                fixed4 vertex : POSITION; // 语义: 要一个顶点位置, Unity会传入到vertex
                fixed2 uv : TEXCOORD; // 贴图位置
            };

            // 输出的数据类型
            struct v2f {
                fixed4 vertex : SV_POSITION; // 一定得这样写 SV System Value
                fixed2 uv : TEXCOORD;
            };

            v2f vert(appData i) {
                v2f o;
                o.uv = i.uv;
                o.vertex = UnityObjectToClipPos(i.vertex); // 坐标系转换, 矩阵相乘
                return o;
            }

            fixed4 frag(v2f v) : SV_TARGET { // SV_TARGET 渲染目标显示在画面上
                return tex2D(_Texture, v.uv); // 贴图根据位置打到模型上， * _Color 即 颜色混合
            }

            ENDCG
        }

    }

}