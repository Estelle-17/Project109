using System.IO;
using UnityEngine;

public static class ImageLoader
{
    /// <summary>
    /// 로컬 경로에 있는 이미지 파일(.png, .jpg)을 읽어들여 런타임에 Sprite로 변환합니다.
    /// </summary>
    /// <param name="absolutePath">이미지 파일의 절대 경로</param>
    /// <returns>생성된 Sprite 객체 (실패 시 null)</returns>
    public static Sprite LoadCustomSprite(string absolutePath)
    {
        if (!File.Exists(absolutePath)) return null;

        // 1. 이미지를 바이트 배열로 읽음
        byte[] fileData = File.ReadAllBytes(absolutePath);
        
        // 2. 임시 텍스처 생성 (크기는 LoadImage 시점에 자동으로 맞춰짐)
        Texture2D texture = new Texture2D(2, 2);
        
        // 3. 바이트 데이터를 이미지 포맷에 맞춰 자동 디코딩
        if (texture.LoadImage(fileData)) 
        {
            // 4. UI에 바로 띄울 수 있도록 Sprite 객체로 포장해서 반환
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        
        return null;
    }
}
