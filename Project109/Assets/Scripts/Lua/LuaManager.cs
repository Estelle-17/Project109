using UnityEngine;
using XLua;
using System.IO;
using System.Collections.Generic;

public class LuaManager
{
    // 순수 C# 싱글톤 인스턴스 (Lazy Initialization)
    private static LuaManager _instance;
    public static LuaManager Instance 
    { 
        get 
        { 
            if (_instance == null)
            {
                _instance = new LuaManager();
            }
            return _instance; 
        } 
    }
    
    public LuaEnv luaEnv { get; private set; }
    
    // 외부(ModLoader 등)에서 동적으로 추가해줄 루아 스크립트 검색 경로들
    private List<string> searchPaths = new List<string>();

    // 생성자 (접근 제어자를 private으로 막아 외부 생성을 방지)
    private LuaManager()
    {
        InitLuaEnv();

        // 순수 C# 객체는 Unity의 생명주기를 따르지 않으므로, 앱 종료 이벤트를 직접 구독하여 해제
        Application.quitting += Dispose;
    }

    private void InitLuaEnv()
    {
        luaEnv = new LuaEnv();
        
        // XLua 커스텀 로더 등록
        luaEnv.AddLoader(CustomModLoader);
    }

    /// <summary>
    /// 모드 로딩 시 루아 스크립트가 존재하는 폴더 경로를 등록합니다.
    /// </summary>
    public void AddSearchPath(string path)
    {
        if (!searchPaths.Contains(path))
        {
            searchPaths.Add(path);
        }
    }

    private byte[] CustomModLoader(ref string filepath)
    {
        string fileName = filepath;
        if (!fileName.EndsWith(".lua"))
        {
            fileName += ".lua";
        }

        // 등록된 모든 검색 경로를 순회하며 파일을 찾음
        foreach (string searchPath in searchPaths)
        {
            if (Directory.Exists(searchPath))
            {
                // 해당 모드 경로 내 모든 하위 디렉토리에서 검색
                string[] files = Directory.GetFiles(searchPath, fileName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    return File.ReadAllBytes(files[0]);
                }
            }
        }

        return null;
    }

    /// <summary>
    /// XLua 내부의 가비지 컬렉션을 수행합니다.
    /// 어딘가(예: RunManager의 Update)에서 주기적으로 호출해주는 것이 좋습니다.
    /// </summary>
    public void Tick()
    {
        luaEnv?.Tick();
    }

    /// <summary>
    /// 게임 종료 시 호출되어 XLua 환경을 안전하게 파괴합니다.
    /// </summary>
    private void Dispose()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
            luaEnv = null;
        }
    }
}
