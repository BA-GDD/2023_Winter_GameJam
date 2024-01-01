using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MapType
{
    FLOOR = 0,
    WALL = 1,
    EMPTY = 2,
}
public class MapGenerator : MonoBehaviour
{
    public MapType[,] gridHandler;
    public float fillPercentage = 0.7f;
    public float waitWalkerTime = 0.05f;
    [SerializeField] private int _maxWalker;

    public float magnification;

    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Tile _floorTile;
    [SerializeField] private Tile _wallTile;

    [SerializeField] private Vector2Int mapSize; //원하는 맵의 크기
    [SerializeField] private float minimumDevideRate; //공간이 나눠지는 최소 비율
    [SerializeField] private float maximumDivideRate; //공간이 나눠지는 최대 비율
    [SerializeField] private int maximumDepth; //트리의 높이, 높을 수록 방을 더 자세히 나누게 됨
    private async void Start()
    {
        gridHandler = new MapType[mapSize.x, mapSize.y];
        for (int x = 0; x < gridHandler.GetLength(0) - 1; ++x)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; ++y)
            {
                gridHandler[x, y] = MapType.EMPTY;
            }
        }
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //전체 맵 크기의 루트노드를 만듬
        await Divide(root, 0);
        await GenerateRoom(root, 0);
        DrawTile();
    }

    private void DrawTile()
    {
        for (int x = 0; x < gridHandler.GetLength(0) - 1; ++x)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; ++y)
            {
                Vector3Int pos = new Vector3Int(x, y);
                switch (gridHandler[x, y])
                {
                    case MapType.FLOOR:
                        _tileMap.SetTile(pos, _floorTile);
                        break;
                    case MapType.WALL:
                        _tileMap.SetTile(pos, _wallTile);
                        break;
                    case MapType.EMPTY:
                        break;
                }

            }
        }
    }

    private async Task Divide(Node tree, int n)
    {
        if (n < maximumDepth)
        {
            int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
            int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
            if (tree.nodeRect.width >= tree.nodeRect.height) //가로가 더 길었던 경우에는 좌 우로 나누게 될 것이며, 이 경우에는 세로 길이는 변하지 않는다.
            {
                tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
                tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
            }
            else
            {
                tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
                tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
            }
            tree.leftNode.parNode = tree; //자식노드들의 부모노드를 나누기전 노드로 설정
            tree.rightNode.parNode = tree;
            await Divide(tree.leftNode, n + 1); //왼쪽, 오른쪽 자식 노드들도 나눠준다.
            await Divide(tree.rightNode, n + 1);//왼쪽, 오른쪽 자식 노드들도 나눠준다.
        }
    }
    private async Task<RectInt> GenerateRoom(Node tree, int n)
    {
        RectInt rect;
        if (n == maximumDepth) //해당 노드가 리프노드라면 방을 만들어 줄 것이다.
        {
            rect = tree.nodeRect;
            int width = Random.Range(rect.width / 2, rect.width - 1);
            //방의 가로 최소 크기는 노드의 가로길이의 절반, 최대 크기는 가로길이보다 1 작게 설정한 후 그 사이 값중 랜덤한 값을 구해준다.
            int height = Random.Range(rect.height / 2, rect.height - 1);
            //높이도 위와 같다.
            int x = rect.x + Random.Range(1, rect.width - width);
            //방의 x좌표이다. 만약 0이 된다면 붙어 있는 방과 합쳐지기 때문에,최솟값은 1로 해주고, 최댓값은 기존 노드의 가로에서 방의 가로길이를 빼 준 값이다.
            int y = rect.y + Random.Range(1, rect.height - height);
            //y좌표도 위와 같다.
            rect = new RectInt(x, y, width, height);
            WalkerGenerator generator = new WalkerGenerator(this, rect, _maxWalker);
            await generator.Generate();
            //PerlinNoiseMapGenerator generator = new PerlinNoiseMapGenerator(this, tree.nodeRect);
            //await generator.Generate();
        }
        else
        {
            tree.leftNode.roomRect = await GenerateRoom(tree.leftNode, n + 1);
            tree.rightNode.roomRect = await GenerateRoom(tree.rightNode, n + 1);
            rect = tree.leftNode.roomRect;
        }
        return rect;
    }
}