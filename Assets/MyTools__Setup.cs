using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;

namespace ThinkShark.Setup
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders() {
            Folders.CreateDefault("_Project", "Animation", "Art", "Materials", "MyTools", "Prefabs", "Shader",
                "Shader/SubGraphs",
                "Scenes", "ScriptableObjects", "ScriptableObjects/ScriptableValues", "Scripts",
                "Scripts/ScriptableObjects", "Settings",
                "{------------------}"); // <---- Set folder names for default folders generation here!!!
            Refresh();
        }

        [MenuItem("Tools/Setup/Install Essential Tools")]
        public static void ImportEssentials() {
            // Collection of essential tools for every project start to install all in one click
            InstallRiderEditor();
            ImportHotReload();
            ImportVHierachyAndVInspectorAndVTabs();
            ImportSelectionHistory();
            ImportComponentNames();
            ImportEditorConsolePro();
            ImportFullscreenEditor();
            ImportMouseButtonShortcutsAndSelectionHistory();
            ImportSceneViewBookmarkTool();
            ImportColorStudio();
            ImportAudioPreviewTool();
            ImportOdinInspectorAndValidator();
            ImportBetterTransform();
            ImportSceneSelectionTool();
        }

        #region Packages

        [MenuItem("Tools/Setup/Packages/Input System")]
        public static void ImportInputSystem() {
            Packages.InstallPackages(new[] { "com.unity.inputsystem" }); // <---- Set package name here!!!
        }

        [MenuItem("Tools/Setup/Packages/Cinemachine")]
        public static void ImportCinemachine() {
            Packages.InstallPackages(new[] { "com.unity.cinemachine" });
        }


        [MenuItem("Tools/Setup/Packages/Unity AI Navigation")]
        public static void InstallUnityAINavigation() {
            Packages.InstallPackages(new[] {
                "com.unity.ai.navigation"
            });
        }

        [MenuItem("Tools/Setup/Packages/TextMeshPro")]
        public static void InstallTextMeshPro() {
            Packages.InstallPackages(new[] {
                "com.unity.textmeshpro"
            });
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Rider Editor")]
        public static void InstallRiderEditor() {
            Packages.InstallPackages(new[] {
                "com.unity.ide.rider"
            });
        }

        [MenuItem("Tools/Setup/Packages/Shader Graph")]
        public static void InstallShaderGraph() {
            Packages.InstallPackages(new[] {
                "com.unity.shadergraph"
            });
        }

        #endregion

        #region Assets

        [MenuItem("Tools/Setup/Packages/Essentials/HotReload")]
        public static void ImportHotReload() {
            Assets.ImportAsset("HotReload.unitypackage",
                "Essentials"); // <---- Set 1. the name of the package & 2. the subfolder inside your selected asset folder (set in class Assets) here!!!
        }

        [MenuItem("Tools/Setup/Packages/Essentials/vHierarchy, vInspector and vTabs")]
        public static void ImportVHierachyAndVInspectorAndVTabs() {
            Assets.ImportAsset("vHierarchy 2.unitypackage", "Essentials");
            Assets.ImportAsset("vInspector 2.unitypackage", "Essentials");
            Assets.ImportAsset("vTabs 2.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Selection History")]
        public static void ImportSelectionHistory() {
            Assets.ImportAsset("Selection History.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/DOTween")]
        public static void ImportDOTween() {
            Assets.ImportAsset("DOTween Pro.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Component Names")]
        public static void ImportComponentNames() {
            Assets.ImportAsset("Component Names.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Console Pro")]
        public static void ImportEditorConsolePro() {
            Assets.ImportAsset("Editor Console Pro.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Fullscreen Editor")]
        public static void ImportFullscreenEditor() {
            Assets.ImportAsset("Fullscreen Editor.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Mouse Button Shortcuts and Selection History")]
        public static void ImportMouseButtonShortcutsAndSelectionHistory() {
            Assets.ImportAsset("Mouse Button Shortcuts and Selection History.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Scene View Bookmark Tool")]
        public static void ImportSceneViewBookmarkTool() {
            Assets.ImportAsset("Scene View Bookmark Tool.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Scene Selection Tool")]
        public static void ImportSceneSelectionTool() {
            Assets.ImportAsset("Scene Selection Tool.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Color Studio")]
        public static void ImportColorStudio() {
            Assets.ImportAsset("Color Studio.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Audio Preview Tool")]
        public static void ImportAudioPreviewTool() {
            Assets.ImportAsset("Audio Preview Tool.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Curved Worlds")]
        public static void ImportCurvedWorlds() {
            Assets.ImportAsset("Curved World.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Odin Inspector, Serializer and Validator")]
        public static void ImportOdinInspectorAndValidator() {
            Assets.ImportAsset("Odin Inspector and Serializer.unitypackage", "Essentials");
            Assets.ImportAsset("Odin Validator.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Pro Camera 2D")]
        public static void ImportProCamera2D() {
            Assets.ImportAsset("Pro Camera 2D.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Pro Pixelizer")]
        public static void ImportProPixelizer() {
            Assets.ImportAsset("ProPixelizer.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/UniRx")]
        public static void ImportUniRx() {
            Assets.ImportAsset("UniRx.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Amplify Shader Editor, Color and Occlusion")]
        public static void ImportAmplifyPackage() {
            Assets.ImportAsset("Amplify Shader Editor.unitypackage", "Essentials");
            Assets.ImportAsset("Amplify Color.unitypackage", "Essentials");
            Assets.ImportAsset("Amplify Occlusion.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Essentials/Better Transform")]
        public static void ImportBetterTransform() {
            Assets.ImportAsset("Better Transform.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Feel")]
        public static void ImportFeel() {
            Assets.ImportAsset("Feel.unitypackage", "Essentials");
        }

        [MenuItem("Tools/Setup/Packages/Easy Collider Editor")]
        public static void ImportEasyColliderEditor() {
            Assets.ImportAsset("Easy Collider Editor.unitypackage", "Essentials");
        }

        #endregion

        static class Packages
        {
            static AddRequest Request;
            static Queue<string> PackagesToInstall = new();

            public static void InstallPackages(string[] packages) {
                foreach (var package in packages) {
                    PackagesToInstall.Enqueue(package);
                }

                // Start the installation of the first package
                if (PackagesToInstall.Count > 0) {
                    Request = Client.Add(PackagesToInstall.Dequeue());
                    EditorApplication.update += Progress;
                }
            }

            static async void Progress() {
                if (Request.IsCompleted) {
                    if (Request.Status == StatusCode.Success)
                        Debug.Log("Installed: " + Request.Result.packageId);
                    else if (Request.Status >= StatusCode.Failure)
                        Debug.Log(Request.Error.message);

                    EditorApplication.update -= Progress;

                    // If there are more packages to install, start the next one
                    if (PackagesToInstall.Count > 0) {
                        // Add delay before next package install
                        await Task.Delay(1000);
                        Request = Client.Add(PackagesToInstall.Dequeue());
                        EditorApplication.update += Progress;
                    }
                }
            }
        }

        static class Assets
        {
            public static void ImportAsset(string asset, string subfolder,
                string rootFolder = "C:/Unity/Assets") { // <---- Set the folder with your assets here (without subfolders)!!!
                ImportPackage(Combine(rootFolder, subfolder, asset), false);
            }
        }

        static class Folders
        {
            public static void CreateDefault(string root, params string[] folders) {
                var fullpath = Combine(Application.dataPath, root);
                foreach (var folder in folders) {
                    var path = Combine(fullpath, folder);
                    if (!Exists(path)) {
                        CreateDirectory(path);
                    }
                }
            }
        }

        public class SelectiveMultiScriptGenerator : EditorWindow
        {
            private string[] templatePaths;

            private string
                targetFolder = "Assets/_Project/Scripts"; // <---- Set your target folder inside your project here!!!

            private List<string> scriptNames = new List<string>();
            private List<bool> templateSelections = new List<bool>();
            private Vector2 scrollPosition;
            private bool selectAll = false;

            [MenuItem("Tools/Script Generator")]
            public static void ShowWindow() {
                GetWindow<SelectiveMultiScriptGenerator>("Script Generator");
            }

            private void OnGUI() {
                GUILayout.Label("Generate Selected Scripts From Templates", EditorStyles.boldLabel);

                // Folder for templates
                EditorGUILayout.LabelField("Template Folder Path:");
                if (GUILayout.Button("Select Template Folder")) {
                    string initialPath = "C:/Unity/Assets/Scripts/"; // <---- Set your folder with your script templates here!!!
                    string path = EditorUtility.OpenFolderPanel("Select Template Folder", initialPath, "");
                    if (!string.IsNullOrEmpty(path)) {
                        LoadTemplates(path);
                    }
                }

                // Target folder for generated scripts
                EditorGUILayout.LabelField("Target Folder Path:");
                EditorGUILayout.BeginHorizontal();
                targetFolder = EditorGUILayout.TextField(targetFolder);

                if (GUILayout.Button("Select Target Folder", GUILayout.Width(150))) {
                    string defaultTargetPath =
                        Path.Combine(Application.dataPath,
                            "_Project/Scripts"); // <---- Set your folder inside the asset folder here!!!
                    string selectedTargetPath =
                        EditorUtility.OpenFolderPanel("Select Target Folder", defaultTargetPath, "");

                    if (!string.IsNullOrEmpty(selectedTargetPath)) {
                        if (selectedTargetPath.StartsWith(Application.dataPath)) {
                            targetFolder = "Assets" + selectedTargetPath.Substring(Application.dataPath.Length);
                        }
                        else {
                            targetFolder = selectedTargetPath;
                        }
                    }
                }

                EditorGUILayout.EndHorizontal();

                // Toggle for "Select All"
                selectAll = EditorGUILayout.Toggle("Select All", selectAll);
                if (selectAll) {
                    for (int i = 0; i < templateSelections.Count; i++) {
                        templateSelections[i] = true;
                    }
                }

                // Scroll area for checkbox list of templates
                if (templatePaths != null && templatePaths.Length > 0) {
                    GUILayout.Label("Templates Found:");
                    scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));

                    for (int i = 0; i < scriptNames.Count; i++) {
                        EditorGUILayout.BeginHorizontal();
                        templateSelections[i] = EditorGUILayout.Toggle(templateSelections[i], GUILayout.Width(20));
                        EditorGUILayout.LabelField(scriptNames[i], GUILayout.Width(200));
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndScrollView();
                }

                // Button to generate selected scripts
                if (GUILayout.Button("Generate Selected Scripts")) {
                    GenerateSelectedScripts();
                }
            }

            private void LoadTemplates(string path) {
                templatePaths = Directory.GetFiles(path, "*.txt");
                scriptNames.Clear();
                templateSelections.Clear();

                foreach (string templatePath in templatePaths) {
                    string scriptName = Path.GetFileNameWithoutExtension(templatePath);
                    scriptNames.Add(scriptName);
                    templateSelections.Add(false); // Initially unselected
                }
            }

            private void GenerateSelectedScripts() {
                if (templatePaths == null || templatePaths.Length == 0) {
                    Debug.LogError("No templates selected. Please select a folder with template files.");
                    return;
                }

                for (int i = 0; i < templatePaths.Length; i++) {
                    if (templateSelections[i]) // Check if the template is selected
                    {
                        string templatePath = templatePaths[i];
                        string templateContent = File.ReadAllText(templatePath);
                        string scriptName = Path.GetFileNameWithoutExtension(templatePath);
                        string scriptContent = templateContent.Replace("Setup", scriptName);
                        string targetPath = Path.Combine(targetFolder, scriptName + ".cs");

                        Directory.CreateDirectory(targetFolder);

                        Debug.Log("Final target path: " + targetPath);

                        if (File.Exists(targetPath)) {
                            Debug.LogWarning("File already exists: " + targetPath);
                            continue;
                        }

                        File.WriteAllText(targetPath, scriptContent);
                        Debug.Log("Script generated: " + targetPath);
                    }
                }

                AssetDatabase.Refresh();
                Debug.Log("Selected scripts generated successfully in " + targetFolder);
            }
        }
    }
}