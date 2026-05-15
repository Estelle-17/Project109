using UnityEngine;
using UnityEditor;

public class MapPainterEditor : EditorWindow
{
    private MapDataSO targetMapData;    //현재 수정 중인 맵 데이터
    private CellType currentBrushType = CellType.Floor; //현재 선택된 브러시 타입
    private bool isEditing = false; //편집 모드 활성화 여부

    private string currentFixedID = "";

    [MenuItem("Tools/Map Painter")]
    public static void ShowWindow() => GetWindow<MapPainterEditor>("Map Painter");

    private void OnGUI()
    {
        GUILayout.Label("1. 맵 데이터 연결", EditorStyles.boldLabel);
        // 에셋 변경 감지 시작
        EditorGUI.BeginChangeCheck();

        targetMapData = (MapDataSO)EditorGUILayout.ObjectField("Map Data SO", targetMapData, typeof(MapDataSO), false);

        // 에셋 슬롯에 새로운 파일이 들어오거나 변경되었다면
        if (EditorGUI.EndChangeCheck())
        {
            if (targetMapData != null)
            {
                // 씬 뷰를 즉시 다시 그리도록 명령
                SceneView.RepaintAll();
                Debug.Log($"{targetMapData.name} 데이터를 불러왔습니다. 셀 개수: {targetMapData.cells.Count}");
            }
        }

        if (targetMapData == null)
        {
            EditorGUILayout.HelpBox("프로젝트 창에서 MapDataSO를 생성한 뒤 여기에 끌어다 놓으세요.", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("11x11 빈 그리드로 초기화"))
        {
            targetMapData.InitializeGrid();
            EditorUtility.SetDirty(targetMapData); // 변경사항 유니티에 알림
        }

        GUILayout.Space(15);
        GUILayout.Label("2. 그리드 설정", EditorStyles.boldLabel);
        targetMapData.cellSize = EditorGUILayout.FloatField("셀 크기(Size)", targetMapData.cellSize);
        targetMapData.gridOffset = EditorGUILayout.Vector3Field("그리드 오프셋(Offset)", targetMapData.gridOffset);

        GUILayout.Space(15);
        GUILayout.Label("3. 페인팅 도구", EditorStyles.boldLabel);

        // 칠하기 모드 토글
        GUI.backgroundColor = isEditing ? Color.green : Color.white;
        if (GUILayout.Button(isEditing ? "페인팅 모드 ON" : "페인팅 모드 OFF", GUILayout.Height(30)))
        {
            isEditing = !isEditing;
        }
        GUI.backgroundColor = Color.white;

        // 브러시 종류 선택
        currentBrushType = (CellType)EditorGUILayout.EnumPopup("현재 브러시", currentBrushType);

        // 특정 브러시를 선택했을 때 하단에 입력창을 띄워줍니다.
        if (currentBrushType == CellType.FixedTrap || currentBrushType == CellType.FixedObstacle)
        {
            EditorGUI.indentLevel++; // UI 들여쓰기

            currentFixedID = EditorGUILayout.TextField("Addressable Key (ID)", currentFixedID);
            
            EditorGUILayout.HelpBox("고정 스폰할 오브젝트의 Addressable Key나 데이터 ID를 입력하세요.\n이름이 비어있으면 랜덤으로 스폰될 수 있습니다.", MessageType.Info);
            
            EditorGUI.indentLevel--;
        }
    }

    // 에디터 창이 열릴 때 씬 뷰 이벤트 구독
    private void OnEnable() => SceneView.duringSceneGui += OnSceneGUI;
    // 에디터 창이 닫힐 때 구독 해제
    private void OnDisable() => SceneView.duringSceneGui -= OnSceneGUI;

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!isEditing || targetMapData == null) return;

        // 가상의 타일들을 씬 뷰에 그려줌
        DrawMapDataInScene();

        // 마우스 입력 처리 (레이캐스트)
        Event e = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, targetMapData.gridOffset);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            // 셀 크기와 오프셋을 반영하여 마우스 좌표를 인덱스로 변환
            int gridX = Mathf.FloorToInt((hitPoint.x - targetMapData.gridOffset.x) / targetMapData.cellSize + 0.5f);
            int gridY = Mathf.FloorToInt((hitPoint.z - targetMapData.gridOffset.z) / targetMapData.cellSize + 0.5f);

            // 하이라이트 박스 크기 cellSize에 맞게 조절
            Vector3 snappedPos = new Vector3(
                gridX * targetMapData.cellSize + targetMapData.gridOffset.x,
                targetMapData.gridOffset.y,
                gridY * targetMapData.cellSize + targetMapData.gridOffset.z
            );

            // 마우스 커서 위치에 하이라이트 박스 그리기
            Handles.color = new Color(1, 1, 0, 0.5f); // 반투명 노란색
            Handles.DrawWireCube(snappedPos, Vector3.one * targetMapData.cellSize);

            // 마우스 드래그나 클릭 시 색칠하기 (왼쪽 클릭 전용)
            if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && e.button == 0)
            {
                PaintCell(gridX, gridY);
                e.Use(); // 유니티의 다른 클릭 이벤트(오브젝트 선택 등)를 무시하도록 처리
            }
        }

        // 에디터 화면 강제 갱신
        sceneView.Repaint();
    }

    private void PaintCell(int x, int y)
    {
        if (x < 0 || x >= targetMapData.width || y < 0 || y >= targetMapData.height) return;

        CellData cell = targetMapData.cells.Find(c => c.position.x == x && c.position.y == y);

        // 타입이 다르거나, 타입은 같은데 고정 ID가 달라진 경우 덮어씌우기
        if (cell != null && (cell.cellType != currentBrushType || cell.fixedId != currentFixedID))
        {
            Undo.RecordObject(targetMapData, "Paint Map Cell");

            cell.cellType = currentBrushType;

            // 고정 타입일 때만 ID를 기록하고, 그 외의 타일은 비워주기
            if (currentBrushType == CellType.FixedTrap || currentBrushType == CellType.FixedObstacle)
            {
                cell.fixedId = currentFixedID;
            }
            else
            {
                cell.fixedId = "";
            }

            EditorUtility.SetDirty(targetMapData);
        }
    }

    private void DrawMapDataInScene()
    {
        foreach (var cell in targetMapData.cells)
        {
            //데이터 좌표를 월드 좌표로 변환 (셀 크기와 오프셋 반영)
            Vector3 pos = new Vector3(
            cell.position.x * targetMapData.cellSize + targetMapData.gridOffset.x,
            targetMapData.gridOffset.y,
            cell.position.y * targetMapData.cellSize + targetMapData.gridOffset.z
        );

            // CellType에 따라 보여줄 색상 지정
            switch (cell.cellType)
            {
                case CellType.Wall: Handles.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); break;
                case CellType.PlayerSpawn: Handles.color = new Color(0, 0, 1, 0.8f); break; // 파랑
                case CellType.EnemySpawn: Handles.color = new Color(1, 0, 0, 0.8f); break;  // 빨강
                //case CellType.Trap: Handles.color = new Color(1, 0.5f, 0, 0.8f); break;     // 주황
                //case CellType.Obstacle: Handles.color = new Color(0, 0.8f, 0, 0.8f); break; // 녹색
                case CellType.RandomTrapMarker: Handles.color = new Color(1, 0.5f, 0, 0.5f); break;
                case CellType.RandomObstacleMarker: Handles.color = new Color(0.5f, 0.3f, 0, 0.5f); break;

                // 고정 마커들은 진하고 뚜렷하게
                case CellType.FixedTrap: Handles.color = new Color(1, 0.5f, 0, 0.9f); break;
                case CellType.FixedObstacle: Handles.color = new Color(0.6f, 0.4f, 0.2f, 0.9f); break;
                case CellType.Floor:
                default:
                    Handles.color = new Color(1, 1, 1, 0.1f); // 바닥은 옅은 흰색 테두리만
                    Handles.DrawWireCube(pos, Vector3.one * targetMapData.cellSize);
                    continue;
            }

            // 바닥이 아닌 데이터들은 꽉 찬 반투명 사각형으로 표시
            Handles.CubeHandleCap(0, pos, Quaternion.identity, targetMapData.cellSize, EventType.Repaint);

            // 만약 고정 마커라면, 그 위에 어떤 ID인지 텍스트 라벨을 띄우기
            if (cell.cellType == CellType.FixedTrap || cell.cellType == CellType.FixedObstacle)
            {
                // 라벨 텍스트 스타일 지정 (가독성을 위해)
                GUIStyle labelStyle = new GUIStyle();
                labelStyle.normal.textColor = Color.white;
                labelStyle.alignment = TextAnchor.MiddleCenter;

                // 타일 살짝 위쪽에 텍스트 렌더링
                Handles.Label(pos + Vector3.up * targetMapData.cellSize, cell.fixedId, labelStyle);
            }
        }
    }
}
