using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private CardValueRandomizer _cardValueRandomizer;
        

        private void Start()
        {
            _playButton.onClick.AddListener(PlayButtonClicked);
        }

        private void PlayButtonClicked()
        {
            _cardValueRandomizer.Play();
            _playButton.onClick.RemoveListener(PlayButtonClicked);
        }
    }
}