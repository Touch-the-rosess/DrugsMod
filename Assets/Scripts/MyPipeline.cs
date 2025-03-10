using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class MyPipeline : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            context.SetupCameraProperties(camera, false);
            if (camera.TryGetCullingParameters(out this.cullingParameters))
            {
                this.cullResults = context.Cull(ref this.cullingParameters);
                this.clearFlags = camera.clearFlags;
                this.buffer.ClearRenderTarget((this.clearFlags & CameraClearFlags.Depth) > (CameraClearFlags)0, (this.clearFlags & CameraClearFlags.Color) > (CameraClearFlags)0, camera.backgroundColor);
                SortingSettings sortingSettings = default(SortingSettings);
                sortingSettings.criteria = SortingCriteria.CommonOpaque;
                DrawingSettings drawingSettings = new DrawingSettings(new ShaderTagId("SRPDefaultUnlit"), sortingSettings);
                FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
                drawingSettings.enableDynamicBatching = true;
                context.DrawRenderers(this.cullResults, ref drawingSettings, ref filteringSettings);
                context.DrawSkybox(camera);
                sortingSettings.criteria = SortingCriteria.CommonTransparent;
                drawingSettings.sortingSettings = sortingSettings;
                filteringSettings.renderQueueRange = RenderQueueRange.transparent;
                context.DrawRenderers(this.cullResults, ref drawingSettings, ref filteringSettings);
                context.ExecuteCommandBuffer(this.buffer);
                this.buffer.Clear();
                context.Submit();
            }
        }
    }

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    private void DrawDefaultPipeline(ScriptableRenderContext context, Camera camera)
    {
        if (this.errorMaterial == null)
        {
            Shader shader = Shader.Find("Hidden/InternalErrorShader");
            this.errorMaterial = new Material(shader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }
        SortingSettings sortingSettings = default(SortingSettings);
        sortingSettings.criteria = SortingCriteria.CommonOpaque;
        DrawingSettings drawingSettings = new DrawingSettings(new ShaderTagId("ForwardBase"), sortingSettings);
        drawingSettings.SetShaderPassName(1, new ShaderTagId("PrepassBase"));
        drawingSettings.SetShaderPassName(2, new ShaderTagId("Always"));
        drawingSettings.SetShaderPassName(3, new ShaderTagId("Vertex"));
        drawingSettings.SetShaderPassName(4, new ShaderTagId("VertexLMRGBM"));
        drawingSettings.SetShaderPassName(5, new ShaderTagId("VertexLM"));
        drawingSettings.overrideMaterial = this.errorMaterial;
        FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
        context.DrawRenderers(this.cullResults, ref drawingSettings, ref filteringSettings);
    }
    
	private Material errorMaterial;

	private ScriptableCullingParameters cullingParameters;

	private CameraClearFlags clearFlags;

	private CommandBuffer buffer = new CommandBuffer
	{
		name = "RenderCamera"
	};

	private CullingResults cullResults;
}
