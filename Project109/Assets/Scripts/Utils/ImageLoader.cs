using System.IO;
using UnityEngine;

public static class ImageLoader
{
    /// <summary>
    /// 로컬 경로에 있는 이미지 파일(.png, .jpg)을 읽어들여 런타임에 Sprite로 변환합니다.
    /// 최대 크기를 지정하면, 초과 시 지정된 크기로 리사이징합니다.
    /// </summary>
    /// <param name="absolutePath">이미지 파일의 절대 경로</param>
    /// <param name="maxSize">허용되는 최대 가로/세로 픽셀 크기 (0이면 원본 크기 유지)</param>
    /// <returns>생성된 Sprite 객체 (실패 시 null)</returns>
    public static Sprite LoadCustomSprite(string absolutePath, int maxSize = 0)
    {
        if (!File.Exists(absolutePath)) return null;

        // 1. 이미지를 바이트 배열로 읽음
        byte[] fileData = File.ReadAllBytes(absolutePath);
        
        // 2. 임시 텍스처 생성 (크기는 LoadImage 시점에 자동으로 맞춰짐)
        Texture2D texture = new Texture2D(2, 2);
        
        // 3. 바이트 데이터를 이미지 포맷에 맞춰 자동 디코딩
        if (texture.LoadImage(fileData)) 
        {
            // 최대 크기 제한이 있고 텍스처가 그보다 큰 경우 리사이징 수행
            if (maxSize > 0 && (texture.width > maxSize || texture.height > maxSize))
            {
                texture = ResizeTexture(texture, maxSize);
            }

            // 4. UI에 바로 띄울 수 있도록 Sprite 객체로 포장해서 반환
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        
        return null;
    }

    private static Texture2D ResizeTexture(Texture2D source, int maxSize)
    {
        // 원본 비율 유지하면서 새로운 해상도 계산
        float ratio = Mathf.Min((float)maxSize / source.width, (float)maxSize / source.height);
        int newWidth = Mathf.RoundToInt(source.width * ratio);
        int newHeight = Mathf.RoundToInt(source.height * ratio);

        source.filterMode = FilterMode.Bilinear;
        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        rt.filterMode = FilterMode.Bilinear;
        RenderTexture.active = rt;
        
        Graphics.Blit(source, rt);
        
        Texture2D nTex = new Texture2D(newWidth, newHeight);
        nTex.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        nTex.Apply();
        
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        
        return nTex;
    }
}
