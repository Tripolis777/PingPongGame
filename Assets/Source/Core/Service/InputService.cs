using System;

namespace Source.Core.Input
{
    public sealed class InputService : IInputService
    {
        public event Action<InputAction> OnAction;
        
        public void SendAction(InputAction input)
        {
            OnAction?.Invoke(input);    
        }
    }
}