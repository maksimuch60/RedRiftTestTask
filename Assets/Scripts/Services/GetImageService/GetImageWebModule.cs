using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class GetImageWebModule : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Texture2D _texture;
        
        private void Start()
        {
            LoadData(() => {});
        }

        public void LoadData(Action completedCallback)
        {
            Uri uri = new Uri("https://picsum.photos/174/145");
            StartCoroutine(GetRequest(uri));
        }
        
        IEnumerator GetRequest(Uri uri)
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
                    _texture = DownloadHandlerTexture.GetContent(webRequest);
                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
            
            _spriteRenderer.sprite = Sprite.Create(_texture, new Rect(0.0f,0.0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}