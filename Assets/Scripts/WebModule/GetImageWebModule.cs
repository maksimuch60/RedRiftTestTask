using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace WebModule
{
    public class GetImageWebModule : MonoBehaviour
    {
        private readonly List<Texture2D> _textures = new();
        private const string _httpsRequest = "https://picsum.photos/174/145";
        
        public List<Texture2D> Textures => _textures;

        public void LoadData(int amountOfImages, Action completedCallback = null)
        {
            Uri uri = new Uri(_httpsRequest);
            StartCoroutine(GetRequest(uri, amountOfImages));
            completedCallback?.Invoke();
        }
        
        IEnumerator GetRequest(Uri uri, int amountOfImages)
        {
            for (int i = 0; i < amountOfImages; i++)
            {
                using UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri);
                webRequest.timeout = 5;
                yield return webRequest.SendWebRequest();
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        _textures.Add(DownloadHandlerTexture.GetContent(webRequest));
                        break;
                }
            }
        }
    }
}