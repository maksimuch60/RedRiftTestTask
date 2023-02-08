using System;

namespace Services
{
    public class GetImageService : IGetImageService
    {
        private readonly GetImageWebModule _getImageWebModule;

        public GetImageService(GetImageWebModule getImageWebModule)
        {
            _getImageWebModule = getImageWebModule;
        }
        public event Action OnReady;
        public bool IsReady { get; private set; }
        public void LoadData(Action completeCallback)
        {
            _getImageWebModule.LoadData(() =>
            {
                IsReady = true;
                OnReady?.Invoke();
                completeCallback.Invoke();
            });
        }
    }
}