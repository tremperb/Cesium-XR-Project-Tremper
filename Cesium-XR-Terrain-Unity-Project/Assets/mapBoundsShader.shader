Shader "Custom/mapBoundsShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _DiscardColor("Discard Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 vertexColor;
        };

        /**
        * Get the vertex color here so it can be inputted to our surf method
        */
        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.vertexColor = v.color; // Save the Vertex Color in the Input for the surf() method
        }

        half _Glossiness;
        half _Metallic;

        fixed4 _Color;
        fixed4 _DiscardColor;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            // Multiply by our vertex color so we can recognize those
            c.rgb *= IN.vertexColor;
            // Discard color, set to black although you could change this depending on the object 
            fixed4 d = _DiscardColor;
            
            // Get the difference
            half3 trans_diff = c.xyz - d.xyz;

            // Get dot of that to compare
            half diff_sq = dot(trans_diff, trans_diff);

            // Just using 0.1 as a threshold
            // so basically if the color is our color discard it
            // Has to be very close
            // Would just change to a random color like red and get rid of it
            // but cant change black soo we are going to go with this method
            if (diff_sq < 0.00001f)
                discard;

            // Return the obj
            o.Albedo = c.rgb * IN.vertexColor;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "VertexLit"
}
