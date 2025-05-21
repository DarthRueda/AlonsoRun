using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicaBotones : MonoBehaviour
{
    [SerializeField] private Button btnInfinito;
    [SerializeField] private Button btnHistoria;
    [SerializeField] private Button btnExit;

    private GameObject ayudaObject;

    private void Awake()
    {
        if (btnInfinito == null)
            btnInfinito = GameObject.Find("BtnInfinito")?.GetComponent<Button>();
        if (btnHistoria == null)
            btnHistoria = GameObject.Find("BtnHistoria")?.GetComponent<Button>();
        if (btnExit == null)
            btnExit = GameObject.Find("BtnExit")?.GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (btnInfinito != null)
        {
            btnInfinito.onClick.RemoveAllListeners();
            btnInfinito.onClick.AddListener(OnInfinitoClicked);
        }
        if (btnHistoria != null)
        {
            btnHistoria.onClick.RemoveAllListeners();
            btnHistoria.onClick.AddListener(OnHistoriaClicked);
        }
        if (btnExit != null)
        {
            btnExit.onClick.RemoveAllListeners();
            btnExit.onClick.AddListener(OnExitClicked);
        }
    }

    private void Start()
    {
        ayudaObject = GameObject.Find("Ayuda");
        if (ayudaObject != null)
        {
            ayudaObject.SetActive(false);
            // Desactivar el Renderer del objeto Ayuda y sus hijos
            Renderer rend = ayudaObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.enabled = false;
            }
            foreach (Renderer childRend in ayudaObject.GetComponentsInChildren<Renderer>(true))
            {
                childRend.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Hide Ayuda if visible
            if (ayudaObject != null && ayudaObject.activeSelf)
            {
                ayudaObject.SetActive(false);
                Renderer rend = ayudaObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.enabled = false;
                }
                foreach (Renderer childRend in ayudaObject.GetComponentsInChildren<Renderer>(true))
                {
                    childRend.enabled = false;
                }
            }
            // Show all Canvas elements again
            Canvas[] allCanvases = FindObjectsOfType<Canvas>(true); // include inactive
            foreach (Canvas canvas in allCanvases)
            {
                if (!canvas.gameObject.activeSelf && !canvas.gameObject.name.Equals("Ayuda"))
                {
                    canvas.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnInfinitoClicked()
    {
        if (GameManager.Instance != null)
        {
            Time.timeScale = 1f;
            typeof(GameManager).GetField("timer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance, 0f);
            typeof(GameManager).GetField("score", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance, 0);
        }
        SceneManager.LoadScene("Infinito");
    }

    public void OnHistoriaClicked()
    {
        if (GameManager.Instance != null)
        {
            Time.timeScale = 1f;
            typeof(GameManager).GetField("timer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance, 0f);
            typeof(GameManager).GetField("score", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance, 0);
        }
        SceneManager.LoadScene("Inicio");
    }

    public void OnHelpClicked()
    {
        if (ayudaObject == null)
        {
            ayudaObject = GameObject.Find("Ayuda");
        }
        if (ayudaObject != null)
        {
            ayudaObject.SetActive(true);
            Renderer rend = ayudaObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.enabled = true;
            }
            foreach (Renderer childRend in ayudaObject.GetComponentsInChildren<Renderer>(true))
            {
                childRend.enabled = true;
            }
        }
        // Hide all other Canvas elements
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            if (!canvas.gameObject.name.Equals("Ayuda"))
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    public void OnExitClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
