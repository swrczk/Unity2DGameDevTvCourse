using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BundleObjectLoader : MonoBehaviour
{
   public string assetName = "BundleSpriteObject";
   public string bundleName = "testbundle";

   void Start()
   {
      var localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));
      if (!localAssetBundle)
      {
         Debug.LogError("Failed to load AssetBundle");
         return;
      }
      var asset = localAssetBundle.LoadAsset<GameObject>(assetName);
      Instantiate(asset);
      localAssetBundle.Unload(false);
   }
}
