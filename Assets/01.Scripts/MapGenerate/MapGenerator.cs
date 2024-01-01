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

    [SerializeField] private Vector2Int mapSize; //���ϴ� ���� ũ��
    [SerializeField] private float minimumDevideRate; //������ �������� �ּ� ����
    [SerializeField] private float maximumDivideRate; //������ �������� �ִ� ����
    [SerializeField] private int maximumDepth; //Ʈ���� ����, ���� ���� ���� �� �ڼ��� ������ ��
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
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //��ü �� ũ���� ��Ʈ��带 ����
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
            if (tree.nodeRect.width >= tree.nodeRect.height) //���ΰ� �� ����� ��쿡�� �� ��� ������ �� ���̸�, �� ��쿡�� ���� ���̴� ������ �ʴ´�.
            {
                tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
                tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
            }
            else
            {
                tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
                tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
            }
            tree.leftNode.parNode = tree; //�ڽĳ����� �θ��带 �������� ���� ����
            tree.rightNode.parNode = tree;
            await Divide(tree.leftNode, n + 1); //����, ������ �ڽ� ���鵵 �����ش�.
            await Divide(tree.rightNode, n + 1);//����, ������ �ڽ� ���鵵 �����ش�.
        }
    }
    private async Task<RectInt> GenerateRoom(Node tree, int n)
    {
        RectInt rect;
        if (n == maximumDepth) //�ش� ��尡 ��������� ���� ����� �� ���̴�.
        {
            rect = tree.nodeRect;
            int width = Random.Range(rect.width / 2, rect.width - 1);
            //���� ���� �ּ� ũ��� ����� ���α����� ����, �ִ� ũ��� ���α��̺��� 1 �۰� ������ �� �� ���� ���� ������ ���� �����ش�.
            int height = Random.Range(rect.height / 2, rect.height - 1);
            //���̵� ���� ����.
            int x = rect.x + Random.Range(1, rect.width - width);
            //���� x��ǥ�̴�. ���� 0�� �ȴٸ� �پ� �ִ� ��� �������� ������,�ּڰ��� 1�� ���ְ�, �ִ��� ���� ����� ���ο��� ���� ���α��̸� �� �� ���̴�.
            int y = rect.y + Random.Range(1, rect.height - height);
            //y��ǥ�� ���� ����.
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