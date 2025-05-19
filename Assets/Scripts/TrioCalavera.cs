using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TrioCalavera : MonoBehaviour
{
    [Header("overlayDAZN GameObject (UI)")]
    public GameObject overlayDAZN;

    [Header("ChatTrioCalavera GameObject (UI)")]
    public GameObject chatTrioCalavera;

    [Header("Trio Calavera prefabs: 0=Lobato, 1=Pedro, 2=Cuquerella")]
    public GameObject[] trioCalaveraPrefabs;

    [Header("Spawn point transform")]
    public Transform spawnPoint;

    [Header("ChatTrioCalavera")]
    public TMP_Text chatPhraseText;

    [Header("Frases TrioCalavera")]
    public string[] phrases = {};

    private void Awake()
    {
        if (spawnPoint == null)
            spawnPoint = transform;

        if (overlayDAZN != null)
            overlayDAZN.SetActive(false);
        if (chatTrioCalavera != null)
            chatTrioCalavera.SetActive(false);
        if (chatPhraseText == null)
        {
            chatPhraseText = FindObjectOfType<TMP_Text>();
        }
        if (chatPhraseText != null)
            chatPhraseText.text = "";
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Historia" || SceneManager.GetActiveScene().name == "Infinito")
        {
            StartCoroutine(TrioCalaveraRoutine());
        }
    }

    private IEnumerator TrioCalaveraRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 10f);
            yield return new WaitForSeconds(waitTime);

            if (overlayDAZN != null)
                overlayDAZN.SetActive(true);

            if (chatTrioCalavera != null)
                chatTrioCalavera.SetActive(true);

            GameObject spawnedIcon = null;
            int idx = -1;
            if (trioCalaveraPrefabs != null && trioCalaveraPrefabs.Length > 0)
            {
                idx = Random.Range(0, trioCalaveraPrefabs.Length);
                GameObject prefab = trioCalaveraPrefabs[idx];
                if (prefab != null)
                {
                    spawnedIcon = Instantiate(
                        prefab,
                        spawnPoint.position,
                        spawnPoint.rotation,
                        spawnPoint
                    );
                }
            }

            // Set random phrase based on prefab index
            if (chatPhraseText != null && phrases != null && phrases.Length > 0 && idx != -1)
            {
                int phraseIdx = 0;
                if (idx == 0 && phrases.Length >= 5)
                {
                    phraseIdx = Random.Range(0, 5); // Lobato: 0-4
                }
                else if (idx == 1 && phrases.Length >= 13)
                {
                    phraseIdx = Random.Range(5, 13); // Pedro: 5-12
                }
                else if (idx == 2 && phrases.Length >= 20)
                {
                    phraseIdx = Random.Range(13, 20); // Cuquerella: 13-19
                }
                else
                {
                    phraseIdx = Random.Range(0, phrases.Length);
                }
                chatPhraseText.text = phrases[phraseIdx];
            }

            yield return new WaitForSeconds(5f);

            if (overlayDAZN != null)
                overlayDAZN.SetActive(false);

            if (chatTrioCalavera != null)
                chatTrioCalavera.SetActive(false);

            if (chatPhraseText != null)
                chatPhraseText.text = "";

            if (spawnedIcon != null)
                spawnedIcon.SetActive(false);
        }
    }
}
