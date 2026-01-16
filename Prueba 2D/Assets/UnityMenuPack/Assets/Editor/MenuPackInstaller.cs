#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class MenuPackInstaller
{
    static Font BuiltinFont()
    {
        // Unity 6+ deprecates Arial.ttf as built-in. Use LegacyRuntime.ttf.
        return Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    }

    [MenuItem("Tools/Setup/Menu Pack (Scenes+UI)")]
    public static void CreateScenesAndUI()
    {
        string scenesPath = "Assets/Scenes";
        if (!AssetDatabase.IsValidFolder(scenesPath))
        {
            AssetDatabase.CreateFolder("Assets", "Scenes");
        }

        // MainMenu
        var mainMenu = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        mainMenu.name = "MainMenu";
        SetupMainMenuUI();
        string mainMenuPath = scenesPath + "/MainMenu.unity";
        EditorSceneManager.SaveScene(mainMenu, mainMenuPath);

        // Level01
        var level = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        level.name = "Level01";
        SetupLevelUI();
        string levelPath = scenesPath + "/Level01.unity";
        EditorSceneManager.SaveScene(level, levelPath);

        // Credits
        var credits = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        credits.name = "Credits";
        SetupCreditsUI("Tu Nombre");
        string creditsPath = scenesPath + "/Credits.unity";
        EditorSceneManager.SaveScene(credits, creditsPath);

        // Build Settings
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[] {
            new EditorBuildSettingsScene(mainMenuPath, true),
            new EditorBuildSettingsScene(levelPath, true),
            new EditorBuildSettingsScene(creditsPath, true),
        };

        EditorSceneManager.OpenScene(mainMenuPath, OpenSceneMode.Single);
        EditorUtility.DisplayDialog("Menu Pack", "Escenas y UI creadas y añadidas al Build Settings.", "OK");
    }

    static GameObject EnsureCanvas()
    {
        GameObject canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
        return canvasGO;
    }

    static GameObject CreateButton(Transform parent, string name, string label, Vector2 anchoredPos, Vector2 size)
    {
        GameObject btnGO = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        btnGO.transform.SetParent(parent, false);
        var rt = btnGO.GetComponent<RectTransform>();
        rt.sizeDelta = size;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = anchoredPos;

        var img = btnGO.GetComponent<Image>();
        img.type = Image.Type.Sliced;

        GameObject textGO = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        textGO.transform.SetParent(btnGO.transform, false);
        var trt = textGO.GetComponent<RectTransform>();
        trt.anchorMin = new Vector2(0, 0);
        trt.anchorMax = new Vector2(1, 1);
        trt.offsetMin = Vector2.zero;
        trt.offsetMax = Vector2.zero;
        var txt = textGO.GetComponent<Text>();
        txt.text = label;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.font = BuiltinFont();
        txt.resizeTextForBestFit = true;
        txt.resizeTextMinSize = 10;
        txt.resizeTextMaxSize = 40;
        return btnGO;
    }

    static GameObject CreateText(Transform parent, string name, string content, int size, Vector2 anchoredPos)
    {
        GameObject tGO = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        tGO.transform.SetParent(parent, false);
        var rt = tGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(800, 160);
        rt.anchorMin = new Vector2(0.5f, 1f);
        rt.anchorMax = new Vector2(0.5f, 1f);
        rt.anchoredPosition = anchoredPos;

        var txt = tGO.GetComponent<Text>();
        txt.text = content;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.fontSize = size;
        txt.font = BuiltinFont();
        txt.color = Color.white;
        return tGO;
    }

    static void WireButton(GameObject buttonGO, Object target, string methodName)
    {
        var btn = buttonGO.GetComponent<Button>();
        var mi = target.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        UnityAction action = System.Delegate.CreateDelegate(typeof(UnityAction), target, mi) as UnityAction;
        UnityEditor.Events.UnityEventTools.AddPersistentListener(btn.onClick, action);
        EditorUtility.SetDirty(buttonGO);
    }

    static void SetupMainMenuUI()
    {
        var canvas = EnsureCanvas();

        var bg = new GameObject("Background", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        bg.transform.SetParent(canvas.transform, false);
        var bgrt = bg.GetComponent<RectTransform>();
        bgrt.anchorMin = Vector2.zero;
        bgrt.anchorMax = Vector2.one;
        bgrt.offsetMin = Vector2.zero;
        bgrt.offsetMax = Vector2.zero;
        bg.GetComponent<Image>().color = new Color(0.10f, 0.12f, 0.16f, 1f);

        CreateText(canvas.transform, "Title", "Mi Prototipo", 56, new Vector2(0, -80));

        var systems = new GameObject("Systems");
        var loader = systems.AddComponent<SceneLoader>();

        var playBtn = CreateButton(canvas.transform, "BtnPlay", "Comenzar", new Vector2(0, -40), new Vector2(280, 60));
        var creditsBtn = CreateButton(canvas.transform, "BtnCredits", "Créditos", new Vector2(0, -120), new Vector2(280, 60));
        var quitBtn = CreateButton(canvas.transform, "BtnQuit", "Salir", new Vector2(0, -200), new Vector2(280, 60));

        WireButton(playBtn, loader, "LoadLevel");
        WireButton(creditsBtn, loader, "LoadCredits");
        WireButton(quitBtn, loader, "QuitGame");
    }

    static void SetupLevelUI()
    {
        var canvas = EnsureCanvas();
        CreateText(canvas.transform, "Info", "Escena de Nivel (pulsa el botón para volver)", 28, new Vector2(0, -80));

        var systems = new GameObject("Systems");
        var loader = systems.AddComponent<SceneLoader>();
        systems.AddComponent<BackToMenuOnEsc>();

        var backBtn = CreateButton(canvas.transform, "BtnBack", "Volver al menú", new Vector2(0, -200), new Vector2(320, 70));
        WireButton(backBtn, loader, "LoadMainMenu");
    }

    static void SetupCreditsUI(string studentName)
    {
        var canvas = EnsureCanvas();

        var bg = new GameObject("Background", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        bg.transform.SetParent(canvas.transform, false);
        var bgrt = bg.GetComponent<RectTransform>();
        bgrt.anchorMin = Vector2.zero;
        bgrt.anchorMax = Vector2.one;
        bgrt.offsetMin = Vector2.zero;
        bgrt.offsetMax = Vector2.zero;
        bg.GetComponent<Image>().color = new Color(0.08f, 0.10f, 0.12f, 1f);

        CreateText(canvas.transform, "Header", "Créditos", 48, new Vector2(0, -60));
        CreateText(canvas.transform, "Student", studentName + "\\n(Autoría del Proyecto)", 36, new Vector2(0, -160));

        var systems = new GameObject("Systems");
        var loader = systems.AddComponent<SceneLoader>();

        var backBtn = CreateButton(canvas.transform, "BtnBack", "Volver al menú", new Vector2(0, -220), new Vector2(320, 70));
        WireButton(backBtn, loader, "LoadMainMenu");
    }
}
#endif
