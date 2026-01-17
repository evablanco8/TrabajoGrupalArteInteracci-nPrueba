#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public static class TwoDLevelInstaller
{
    static Font BuiltinFont()
    {
        return Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    }

    [MenuItem("Tools/Setup/Install 2D Level (Player+Death)")]
    public static void Install2DLevel()
    {
        string scenesPath = "Assets/Scenes";
        string levelPath = scenesPath + "/Level01.unity";
        if (!File.Exists(levelPath))
        {
            EditorUtility.DisplayDialog("2D Level", "No existe Assets/Scenes/Level01.unity. Ejecuta antes 'Menu Pack (Scenes+UI)'.", "OK");
            return;
        }

        var scene = EditorSceneManager.OpenScene(levelPath, OpenSceneMode.Single);

        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        Physics2D.gravity = new Vector2(0, -30f);

        var systems = GameObject.Find("Systems") ?? new GameObject("Systems");
        var loader = systems.GetComponent<SceneLoader>() ?? systems.AddComponent<SceneLoader>();
        var gm = systems.GetComponent<GameManager2D>() ?? systems.AddComponent<GameManager2D>();

        // Ground
        var ground = new GameObject("Ground", typeof(SpriteRenderer), typeof(BoxCollider2D));
        ground.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/ground.png");
        ground.transform.position = new Vector3(0, -2.5f, 0);
        ground.transform.localScale = new Vector3(8, 1, 1);

        // Player
        var player = new GameObject("Player", typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Player2DController));
        player.tag = "Player";
        player.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/player.png");
        player.transform.position = new Vector3(-5f, -1.5f, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        var gc = new GameObject("GroundCheck");
        gc.transform.SetParent(player.transform);
        gc.transform.localPosition = new Vector3(0, -0.6f, 0);
        var ctrl = player.GetComponent<Player2DController>();
        ctrl.groundCheck = gc.transform;
        ctrl.groundMask = LayerMask.GetMask("Default");

        // Hazard
        var hazard = new GameObject("Spikes", typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Hazard2D));
        hazard.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/spike.png");
        hazard.transform.position = new Vector3(2.5f, -2.1f, 0);
        hazard.GetComponent<BoxCollider2D>().isTrigger = true;

        // Platform
        var plat = new GameObject("Platform", typeof(SpriteRenderer), typeof(BoxCollider2D));
        plat.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/ground.png");
        plat.transform.position = new Vector3(-1.5f, -0.5f, 0);
        plat.transform.localScale = new Vector3(2.5f, 1, 1);

        // UI Panel
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }
        var deathPanel = new GameObject("DeathPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        deathPanel.transform.SetParent(canvas.transform, false);
        var dprt = deathPanel.GetComponent<RectTransform>();
        dprt.anchorMin = new Vector2(0.5f, 0.5f);
        dprt.anchorMax = new Vector2(0.5f, 0.5f);
        dprt.sizeDelta = new Vector2(420, 240);
        dprt.anchoredPosition = Vector2.zero;
        deathPanel.GetComponent<Image>().color = new Color(0,0,0,0.8f);
        deathPanel.SetActive(false);

        var info = new GameObject("InfoText", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        info.transform.SetParent(deathPanel.transform, false);
        var irt = info.GetComponent<RectTransform>();
        irt.anchorMin = new Vector2(0.5f, 1f);
        irt.anchorMax = new Vector2(0.5f, 1f);
        irt.anchoredPosition = new Vector2(0, -40);
        irt.sizeDelta = new Vector2(360, 60);
        var itxt = info.GetComponent<Text>();
        itxt.text = "Has muerto";
        itxt.alignment = TextAnchor.MiddleCenter;
        itxt.fontSize = 28;
        itxt.font = BuiltinFont();
        itxt.color = Color.white;

        var btn = new GameObject("BtnQuitToMenu", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        btn.transform.SetParent(deathPanel.transform, false);
        var brt = btn.GetComponent<RectTransform>();
        brt.anchorMin = new Vector2(0.5f, 0f);
        brt.anchorMax = new Vector2(0.5f, 0f);
        brt.anchoredPosition = new Vector2(0, 40);
        brt.sizeDelta = new Vector2(260, 60);
        var btxtGO = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        btxtGO.transform.SetParent(btn.transform, false);
        var btrt = btxtGO.GetComponent<RectTransform>();
        btrt.anchorMin = new Vector2(0,0);
        btrt.anchorMax = new Vector2(1,1);
        btrt.offsetMin = Vector2.zero;
        btrt.offsetMax = Vector2.zero;
        var btxt = btxtGO.GetComponent<Text>();
        btxt.text = "Quit (Volver al Menú)";
        btxt.alignment = TextAnchor.MiddleCenter;
        btxt.font = BuiltinFont();
        btxt.resizeTextForBestFit = true;
        btxt.resizeTextMinSize = 10;
        btxt.resizeTextMaxSize = 36;

        gm.deathPanel = deathPanel;
        gm.quitButton = btn.GetComponent<Button>();
        gm.sceneLoader = loader;

        EditorSceneManager.SaveScene(scene, levelPath);
        EditorUtility.DisplayDialog("2D Level", "Level01 personalizado con jugador 2D y botón Quit al menú.", "OK");
    }
}
#endif
