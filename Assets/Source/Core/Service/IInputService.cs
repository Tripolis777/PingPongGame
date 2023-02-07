using System;

namespace Source.Core.Input
{
    public interface IInputService : IService
    {
        public event Action<InputAction> OnAction;
        
        public void SendAction(InputAction input);
    }
}