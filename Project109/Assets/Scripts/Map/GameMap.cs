using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    [SerializeField]
    private List<List<Tile>> tileMap;

    public GameObject prefabTile;
    public GameObject moveRangeIndicatorPrefab;
    private MapMoveRangeIndicator moveRangeIndicator;

    public int mapColumn;
    public int mapRow;

    public List<List<Tile>> GetTileMap()
    {
        return tileMap;
    }

    public MapMoveRangeIndicator GetMoveRangeIndicator()
    {
        return moveRangeIndicator;
    }

    public void TileCreateByMapData(MapDataSO mapData)
    {
        float startX = mapData.gridOffset.x;
        float startZ = mapData.gridOffset.z;

        tileMap = new List<List<Tile>>();
        for (int columnIndex = 0; columnIndex < mapData.width; columnIndex++)
        {
            tileMap.Add(new List<Tile>());
            for (int rowIndex = 0; rowIndex < mapData.height; rowIndex++)
            {
                Tile tile = Instantiate(prefabTile, transform).GetComponent<Tile>();
                tile.transform.localPosition = transform.position +
                                               new Vector3(startX + (columnIndex) * mapData.cellSize,
                                                        0.01f,
                                                        startZ + (rowIndex) * mapData.cellSize);
                tile.SetCoord(columnIndex, rowIndex);
                tileMap[columnIndex].Add(tile);
            }
        }

        //맵 크기 저장
        mapColumn = tileMap.Count;
        mapRow = tileMap[0].Count;

        Debug.Log($"MapManager Column: {mapColumn}, Row: {mapRow}");

        ProcessTileSetting(mapData);

        if (moveRangeIndicatorPrefab != null)
        {
            GameObject indicatorObj = Instantiate(moveRangeIndicatorPrefab, transform);
            moveRangeIndicator = indicatorObj.GetComponent<MapMoveRangeIndicator>();
            if (moveRangeIndicator != null)
            {
                moveRangeIndicator.Initialize(mapData.width, mapData.height, mapData.cellSize, mapData.gridOffset, tileMap);
            }
        }
        //맵의 외각선 생성
        //SetMapOutsideLine();
    }

    private void ProcessTileSetting(MapDataSO mapData)
    {
        //생성된 타일에 알맞은 정보 추가
        foreach (CellData cell in mapData.cells)
        {
            // eventID가 Trap이거나 objectID가 Trap으로 시작하는 경우 이동 가능(Trap)
            if (cell.eventID == "Trap" || cell.objectID.StartsWith("Trap"))
            {
                tileMap[cell.position.x][cell.position.y].tileState = TileState.Trap;
            }
            // 그 외에 objectID가 Empty가 아니거나, terrainID가 Empty이거나, eventID가 Block인 경우 이동 불가(Full)
            else if (cell.objectID != "Empty" || cell.terrainID == "Empty" || cell.eventID == "Block")
            {
                tileMap[cell.position.x][cell.position.y].tileState = TileState.Full;
            }
            // 그 외는 일반 이동 가능 타일
            else
            {
                tileMap[cell.position.x][cell.position.y].tileState = TileState.Empty;
            }
        }
    }

    // //이동할 수 있는 타일들의 외각을 표시해주는 함수
    // public void SetMapOutsideLine()
    // {
    //     //현재 외각선 초기화
    //     for (int columnIndex = 0; columnIndex < mapColumn; columnIndex++)
    //     {
    //         for (int rowIndex = 0; rowIndex < mapRow; rowIndex++)
    //         {
    //             foreach (GameObject obj in tileMap[columnIndex][rowIndex].tileBaseTextureObjects)
    //             {
    //                 obj.SetActive(false);
    //             }
    //         }
    //     }

    //     //이후 장애물과 맵의 끝 부분을 탐색하여 외각선 생성
    //     for (int columnIndex = 0; columnIndex < mapColumn; columnIndex++)
    //     {
    //         for (int rowIndex = 0; rowIndex < mapRow; rowIndex++)
    //         {
    //             //현재 위치가 비어있을 경우
    //             if (tileMap[columnIndex][rowIndex].tileState == TileState.Empty || tileMap[columnIndex][rowIndex].tileState == TileState.Trap)
    //             {
    //                 //상,하,좌,우 순으로 탐색
    //                 int[] dirX = { 0, 0, 1, -1 };
    //                 int[] dirY = { -1, 1, 0, 0 };

    //                 for (int i = 0; i < 4; i++)
    //                 {
    //                     int x = columnIndex + dirX[i];
    //                     int y = rowIndex + dirY[i];

    //                     //맵의 범위 내에 있는 경우
    //                     if (x < mapColumn && x >= 0 && y < mapRow && y >= 0)
    //                     {
    //                         //탐색된 위치가 이동 불가능한 위치일 때
    //                         if (tileMap[x][y].tileState == TileState.Full || tileMap[x][y].tileState == TileState.Obstacle)
    //                         {
    //                             tileMap[columnIndex][rowIndex].tileBaseTextureObjects[i].SetActive(true);
    //                         }
    //                     }
    //                     else
    //                     {
    //                         tileMap[columnIndex][rowIndex].tileBaseTextureObjects[i].SetActive(true);
    //                     }
    //                 }
    //             }
    //         }
    //     }
    // }
}
