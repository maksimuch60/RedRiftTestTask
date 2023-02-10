using System;
using System.Collections;
using Configs;
using Shared;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace WebModule
{
    public class GetImageWebModule : MonoBehaviour
    {
        [SerializeField] private SpriteContainer _spriteContainer;
        [SerializeField] private int _minImagesAmount;
        [SerializeField] private int _maxImagesAmount;
        

        private bool _isReady;
        private int _amountOfImages;
        
        private const string _httpsRequest = "https://picsum.photos/174/150";

        public bool IsReady => _isReady;

        private void Awake()
        {
            _amountOfImages = Random.Range(_minImagesAmount, _maxImagesAmount + 1);
            LoadData();
        }

        private void LoadData()
        {
            _spriteContainer.Clear();
            
            Uri uri = new Uri(_httpsRequest);
            StartCoroutine(GetRequest(uri));
        }

        private IEnumerator GetRequest(Uri uri)
        {
            for (int i = 0; i < _amountOfImages; i++)
            {
                using UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri);
                webRequest.timeout = 10;
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
                        _spriteContainer.Add(TextureToSpriteConverter.Convert(DownloadHandlerTexture.GetContent(webRequest)));
                        break;
                }
            }

            _isReady = true;
            SceneManager.LoadScene("GameScene");
        }
    }
}