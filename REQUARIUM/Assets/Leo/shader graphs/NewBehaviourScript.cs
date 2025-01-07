using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class FullscreenPassHDRP : CustomPass
{
    public Material fullscreenMaterial;

    protected override void Execute(CustomPassContext ctx)
    {
        if (fullscreenMaterial == null) return;

        // Get the current camera color buffer
        var source = ctx.cameraColorBuffer;

        // Perform the fullscreen pass
        CoreUtils.SetRenderTarget(ctx.cmd, source);
        ctx.cmd.DrawProcedural(Matrix4x4.identity, fullscreenMaterial, 0, MeshTopology.Triangles, 3, 1);
    }
}
