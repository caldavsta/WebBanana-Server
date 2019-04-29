using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    abstract class IManagedComponent
    {
        public enum AppState
        {
            Created,
            Running,
            Stopped,
            Removed
        }

        public AppState State;

        public IManagedComponent()
        {
            this.State = AppState.Created;
        }

        private void SetState(AppState newState)
        {
            switch (newState)
            {
                case AppState.Created:
                    break;
                case AppState.Running:
                    break;
                case AppState.Stopped:
                    break;
                case AppState.Removed:
                    break;
                default:
                    break;
            }
        }
        private void Create()
        {
            OnCreate();
        }
        private void Start()
        {
            OnStart();
        }
        private void Stop()
        {
            OnStop();
        }
        private void Remove()
        {
            Remove();
        }
    
        public abstract void OnCreate();
        public abstract void OnStart();
        public abstract void OnStop();
        public abstract void OnRemoved();

    }
}
