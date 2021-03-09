using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameControl : MonoBehaviour
{

    public GameObject[] _puzzle; // оригинальный массив

    // стартовая позиция для первого элемента
    public float startPosX = -6f;
    public float startPosY = 6f;

    // отступ по Х и Y, рассчитывается в зависимости от размера объекта
    public float outX = 1.1f;
    public float outY = 1.1f;

    public Text _text; // вывод текстовой информации

    public static int click;
    public static GameObject[,] grid;
    public static Vector3[,] position;
    private GameObject[] puzzleRandom;
    public static bool win;

    [SerializeField] Fungus.Flowchart flowchart;
    [SerializeField] GameObject buttonHint;

    void Start()
    {
        puzzleRandom = new GameObject[_puzzle.Length];

        // заполнение массива позиций клеток
        float posXreset = startPosX;
        position = new Vector3[3, 3];
        for (int y = 0; y < 3; y++)
        {
            startPosY -= outY;
            for (int x = 0; x < 3; x++)
            {
                startPosX += outX;
                position[x, y] = new Vector3(startPosX, startPosY, 0);
            }
            startPosX = posXreset;
        }

        if (!PlayerPrefs.HasKey("Puzzle")) StartNewGame(); else Load();
    }

    public void StartNewGame()
    {
        win = false;
        click = 0;
        RandomPuzzle();
        Debug.Log("New Game");
    }

    public void ExitGame()
    {
        Save();
        flowchart.ExecuteBlock("Scene_5");

        //Application.Quit();
    }

    void Save()
    {
        string content = string.Empty;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (content.Length > 0) content += "|";
                if (grid[x, y]) content += grid[x, y].GetComponent<Puzzle>().ID.ToString(); else content += "null";
            }
        }
        PlayerPrefs.SetString("Puzzle", content);
        PlayerPrefs.SetString("PuzzleInfo", click.ToString());
        //PlayerPrefs.Save(); // записать на диск сейчас, если в приложении не используется функция выхода
        Debug.Log(this + " сохранение 8 Puzzle.");
    }

    void Load()
    {
        string[] content = PlayerPrefs.GetString("Puzzle").Split(new char[] { '|' });

        if (content.Length == 0 || content.Length != 9) return;

        if (PlayerPrefs.HasKey("PuzzleInfo")) click = Parse(PlayerPrefs.GetString("PuzzleInfo"));

        grid = new GameObject[3, 3];
        int i = 0;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int j = FindPuzzle(Parse(content[i]));

                if (j >= 0)
                {
                    grid[x, y] = Instantiate(_puzzle[j], position[x, y], Quaternion.identity) as GameObject;
                    grid[x, y].name = "ID-" + i;
                    grid[x, y].transform.parent = transform;

                }
                i++;
            }
        }
    }

    int FindPuzzle(int index)
    {
        int j = 0;
        foreach (GameObject e in _puzzle)
        {
            if (e.GetComponent<Puzzle>().ID == index) return j;
            j++;
        }
        return -1;
    }

    int Parse(string text)
    {
        int value;
        if (int.TryParse(text, out value)) return value;
        return -1;
    }

    void CreatePuzzle()
    {
        if (transform.childCount > 0)
        {
            // удаление старых объектов, если они есть
            for (int j = 0; j < transform.childCount; j++)
            {
                Destroy(transform.GetChild(j).gameObject);
            }
        }
        int i = 0;
        grid = new GameObject[3, 3];
        int h = Random.Range(0, 2);
        int v = Random.Range(0, 2);
        GameObject clone = new GameObject();
        grid[h, v] = clone; // размещаем пустой объект в случайную клетку
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                // создание дубликатов на основе временного массива
                if (grid[x, y] == null)
                {
                    grid[x, y] = Instantiate(puzzleRandom[i], position[x, y], Quaternion.identity) as GameObject;
                    grid[x, y].name = "ID-" + i;
                    grid[x, y].transform.parent = transform;
                    i++;
                }
            }
        }
        Destroy(clone); // очистка случайной клетки
        for (int q = 0; q < _puzzle.Length; q++)
        {
            Destroy(puzzleRandom[q]); // очистка временного массива
        }
    }

    // проверка на выигрыш
    static public void GameFinish()
    {
        int i = 1;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
               
                if (grid[x, y])
                {
                    if (grid[x, y].GetComponent<Puzzle>().ID == i)
                        i++;
                }
                else 
                {
                    if ((x == 1 && y == 0) || (x == 1 && y == 2))
                    {
                        i++;
                    }
                    else
                    {
                        i--;
                    }
                }
                //Debug.Log(i);
            }
        }
        if (i == 9)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (grid[x, y]) Destroy(grid[x, y].GetComponent<Puzzle>());
                }
            }
            win = true;
            Debug.Log("Finish!");
        }
    }

    // создание временного массива, с случайно перемешанными элементами
    void RandomPuzzle()
    {
        int[] tmp = new int[_puzzle.Length];
        for (int i = 0; i < _puzzle.Length; i++)
        {
            tmp[i] = 1;
        }
        int c = 0;
        while (c < _puzzle.Length)
        {
            int r = Random.Range(0, _puzzle.Length);
            if (tmp[r] == 1)
            {
                puzzleRandom[c] = Instantiate(_puzzle[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
                tmp[r] = 0;
                c++;
            }
        }
        CreatePuzzle();
    }

    void LateUpdate()
    {
        if (!win)
        {
            Debug.Log(click);
            if (click >= 10)
            {
                if (!buttonHint.activeSelf)
                {
                    buttonHint.SetActive(true);
                }
            }
            _text.text = "Ходов:\n" + click;
        }
        else
        {
            click = 0;
            //_text.text = "Игра\nЗавершена!";
            //Debug.Log(win);
            Invoke("NextScene", 0.5f);
        }
    }

    void NextScene()
    {
        flowchart.ExecuteBlock("Shkotulka");
    }

    public void SetWinPosition()
    {
        if (transform.childCount > 0)
        {
            // удаление старых объектов, если они есть
            for (int j = 0; j < transform.childCount; j++)
            {
                Destroy(transform.GetChild(j).gameObject);
            }
        }
        int i = 0;
        grid = new GameObject[3, 3];
        //int h = Random.Range(0, 2);
        //int v = Random.Range(0, 2);
        GameObject clone = new GameObject();
        grid[1, 0] = clone; // размещаем пустой объект в случайную клетку
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (x == 1 && y == 0)
                {
                    continue;
                } else

                // создание дубликатов на основе временного массива
                if (grid[x, y] == null)
                {
                    grid[x, y] = Instantiate(_puzzle[i], position[x, y], Quaternion.identity) as GameObject;
                    grid[x, y].name = "ID-" + i;
                    grid[x, y].transform.parent = transform;
                    i++;
                }
            }
        }
        Destroy(clone); // очистка случайной клетки
        for (int q = 0; q < _puzzle.Length; q++)
        {
            Destroy(puzzleRandom[q]); // очистка временного массива
        }
    }
}
