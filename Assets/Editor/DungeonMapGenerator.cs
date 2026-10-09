// DungeonMapGenerator.cs
// Place this file inside Assets/Editor/ after importing ModularDungeonTiles_AssetPack.unitypackage.
// In Unity, select Tools > Dungeon Map > Generate Collectible Dungeon.
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DungeonMapGenerator
{
    [MenuItem("Tools/Dungeon Map/Generate Collectible Dungeon")]
    public static void Generate()
    {
        const string root = "Assets/Asset/Modular Dungeon Tiles/prefab/";
        string[] roomNames = {
            "Rooms/Room_A_1.prefab", "Rooms/Room_A_2.prefab", "Rooms/Room_A_3.prefab",
            "Rooms/Room_B_1.prefab", "Rooms/Room_B_2.prefab", "Rooms/Room_B_3.prefab",
            "Rooms/Room_C_1_A.prefab", "Rooms/Room_C_2.prefab", "Rooms/Room_C_3_A.prefab",
            "Rooms/Room_Open_D_1.prefab", "Rooms/Room_Open_D_2.prefab", "Rooms/Room_Open_D_3.prefab",
            "Rooms/Room_Hall_1.prefab", "Rooms/Room_Hall_2.prefab", "Rooms/Room_Hall_3.prefab",
            "Rooms/Room_Hall_OneEntrance_1.prefab", "Rooms/Room_Hall_TwoEntrance_1.prefab",
            "Rooms/Room_Hall_ThreeEntrance.prefab"
        };

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        GameObject rootObject = new GameObject("Collectible Dungeon Map");
        rootObject.transform.position = Vector3.zero;
        // Compact 5 x 4 arrangement. Modular pieces remain editable prefabs in the scene.
        // Room footprints vary by asset; the 12-unit spacing is a practical starting point.
        Vector2Int[] layout = {
            new(0,0), new(1,0), new(2,0), new(3,0), new(4,0),
            new(0,1), new(2,1), new(3,1), new(4,1),
            new(0,2), new(1,2), new(2,2), new(4,2),
            new(0,3), new(1,3), new(2,3), new(3,3), new(4,3)
        };
        for (int i = 0; i < layout.Length; i++)
        {
            string path = root + roomNames[i % roomNames.Length];
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) { Debug.LogWarning("Prefab not found: " + path); continue; }
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
            instance.name = $"Room_{layout[i].x}_{layout[i].y}_{Path.GetFileNameWithoutExtension(path)}";
            instance.transform.SetParent(rootObject.transform, true);
            instance.transform.position = new Vector3(layout[i].x * 12f, 0f, layout[i].y * 12f);
        }

        // Add sample collectible prop prefabs as visible placeholders to customize.
        AddProp(root + "Props/PropSet_1_Chest.prefab", new Vector3(6f, 0f, 6f), rootObject.transform, "Treasure_Chest_Start");
        AddProp(root + "Props/PropSet_1_Chest.prefab", new Vector3(42f, 0f, 30f), rootObject.transform, "Treasure_Chest_Mid");
        AddProp(root + "Props/PropSet_5_SpikeTrap.prefab", new Vector3(30f, 0f, 18f), rootObject.transform, "Spike_Trap");
        AddProp(root + "Props/PropSet_2_Torch.prefab", new Vector3(18f, 0f, 6f), rootObject.transform, "Torch");

        // Simple light so the generated scene is inspectable immediately.
        GameObject lightObject = new GameObject("Dungeon Preview Light");
        Light light = lightObject.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.1f;
        lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        lightObject.transform.SetParent(rootObject.transform, true);

        string output = "Assets/CollectibleDungeon.unity";
        EditorSceneManager.SaveScene(scene, output);
        AssetDatabase.SaveAssets();
        Debug.Log("Collectible dungeon generated at " + output + ". Open the scene and adjust room spacing/rotation to fit your prefab footprints.");
        EditorUtility.DisplayDialog("Dungeon map created", "Created Assets/CollectibleDungeon.unity.\n\nNote: modular room prefab sizes/door orientations vary, so you may need to move or rotate rooms to connect corridors cleanly. Add your own collectible scripts to the chest placeholders.", "OK");
    }

    private static void AddProp(string path, Vector3 position, Transform parent, string objectName)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) { Debug.LogWarning("Prop prefab not found: " + path); return; }
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        instance.name = objectName;
        instance.transform.SetParent(parent, true);
        instance.transform.position = position;
    }
}
