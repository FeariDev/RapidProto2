using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [Header("Parallax Background")]
    [SerializeField] float parallaxIntensity;
    [SerializeField] float parallaxSmoothSpeed;
    [SerializeField] float parallaxDecay;
    [SerializeField] int parallaxStartLayerOffset;
    [SerializeField] bool parallaxBgIterateInverted;
    [SerializeField] Transform[] parallaxBgLayers;
    public Vector3[] parallaxLayerStartPos;
    public Vector3 parallaxBgTargetPos;
    bool isParallaxBgInitialized;



    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }



    void InitializeParallaxBackground()
    {
        if (parallaxBgLayers.Length < 1)
        {
            Debug.Log("Failed to initialize main menu parallax background because no parallax background layers have been assigned!");
            return;
        }

        parallaxLayerStartPos = new Vector3[parallaxBgLayers.Length];
        for (int i = 0; i < parallaxBgLayers.Length; i++)
        {
            parallaxLayerStartPos[i] = parallaxBgLayers[i].position;
        }

        isParallaxBgInitialized = true;
    }

    void UpdateParallaxBackground()
    {
        if (!isParallaxBgInitialized) return;

        float normalizedMouseX = Input.mousePosition.x / Screen.width * 2 - 1;
        float normalizedMouseY = Input.mousePosition.y / Screen.height * 2 - 1;

        Vector3 mouseDelta = new Vector3(normalizedMouseX, normalizedMouseY, 0);

        for (int i = 0; i < parallaxBgLayers.Length; i++)
        {
            Vector3 newBgLayerPosition;

            float layerIndex = parallaxBgIterateInverted ? parallaxBgLayers.Length - 1 - i : i;

            //parallaxBgTargetPos = parallaxLayerStartPos[i] + mouseDelta * ((layerIndex + parallaxStartLayerOffset) * parallaxDecay) * parallaxIntensity;
            parallaxBgTargetPos = parallaxLayerStartPos[i] + mouseDelta * parallaxIntensity * (1 + (parallaxDecay * (layerIndex + parallaxStartLayerOffset)));
            newBgLayerPosition = Vector3.Lerp(parallaxBgLayers[i].localPosition, parallaxBgTargetPos, parallaxSmoothSpeed * Time.deltaTime);

            parallaxBgLayers[i].localPosition = newBgLayerPosition;
        }
    }



    #region Unity lifecycle

    void Start()
    {
        InitializeParallaxBackground();
    }

    void Update()
    {
        UpdateParallaxBackground();
    }

    #endregion
}
