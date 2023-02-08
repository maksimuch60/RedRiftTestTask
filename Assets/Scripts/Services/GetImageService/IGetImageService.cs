using System;

namespace Services
{
    public interface IGetImageService 
    {
        event Action OnReady;

        bool IsReady { get; }

        void LoadData(Action completeCallback);
    }
}
