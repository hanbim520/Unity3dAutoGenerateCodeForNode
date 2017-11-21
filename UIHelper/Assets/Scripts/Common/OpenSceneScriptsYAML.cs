using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.VR.WSA.WebCam;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using System.Reflection;
public class OpenSceneScriptsYAML : Editor
{

    [MenuItem("Assets/Open All Scripts YAML")]
    static void OpenAllScriptsYAML()
    {
        StreamReader sr = new StreamReader(SceneManager.GetActiveScene().path);
        string sceneString = sr.ReadToEnd();
        var input = new StringReader(sceneString);
        var yaml = new YamlStream();
        yaml.Load(input);
        // Examine the stream
        foreach (YamlDocument yamlDocument in yaml.Documents)
        {
            YamlMappingNode mapping = (YamlMappingNode)yamlDocument.RootNode;
            bool ifContainsMono = mapping.Children.ContainsKey(new YamlScalarNode("MonoBehaviour"));
            if (!ifContainsMono)
            {
                continue;
            }
            YamlMappingNode monoMapping = (YamlMappingNode)mapping.Children[new YamlScalarNode("MonoBehaviour")];
            YamlMappingNode scriptMapping = (YamlMappingNode)monoMapping.Children[new YamlScalarNode("m_Script")];
            YamlScalarNode guidScalarNode = (YamlScalarNode)scriptMapping.Children[new YamlScalarNode("guid")];
            //获得guid
            string guid = guidScalarNode.Value;
    //        MonoScript mono = (MonoScript)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(MonoScript));
  //          AssetDatabase.OpenAsset(mono);
       //     OpenBaseClass(mono.GetClass());

        }

    }

    [MenuItem("Assets/Open All Scripts NO YAML")]
    static void OpenAllScriptsNoYAML()
    {
        MonoBehaviour[] monos = FindObjectsOfType<MonoBehaviour>();
        foreach (var mono in monos)
        {
            Type monoType = mono.GetType();//包括命名空间
            string[] monoGuids = AssetDatabase.FindAssets("t:Script " + monoType.ToString().Split('.').Last());
            foreach (var monoGuid in monoGuids)
            {
                MonoScript monoScript = (MonoScript)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(monoGuid), typeof(MonoScript));
                if (monoScript.GetClass() == monoType)
                {
                    AssetDatabase.OpenAsset(monoScript);//找到了
                    OpenBaseClass(monoScript.GetClass());
                    break;
                }
            }
        }
    }

    static void OpenBaseClass(Type initType)
    {
        Type curType = initType.BaseType;
        while (curType != typeof(MonoBehaviour))//说明还有父类
        {
            string className = curType.ToString().Split('.').Last();//文件名就是类名
            string[] baseGuids = AssetDatabase.FindAssets(className + " t:Script");//guid是按照字母排的，没办法，只能遍历了。

            foreach (string baseGuid in baseGuids)
            {
                MonoScript mono2 = (MonoScript)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(baseGuid), typeof(MonoScript));
                if (mono2.GetClass() == curType)
                {
                    AssetDatabase.OpenAsset(mono2);
                    break;
                }
            }
            curType = curType.BaseType;//变到父类
        }
    }
}