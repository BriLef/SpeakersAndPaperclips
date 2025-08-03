using UnityEngine;

public static class SpriteUtil
{
    public static Sprite FromTexture(Texture2D tex, float pixelsPerUnit = 100f, Vector2? pivot = null)
    {
        if (tex == null) return null;
        var p = pivot ?? new Vector2(0.5f, 0.5f);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), p, pixelsPerUnit);
    }
}
