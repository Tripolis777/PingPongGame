using System;
using System.Collections.Generic;

namespace Source.Core.Input
{
    public sealed class InputService : IInputService
    {
        public event Action<InputAction> onAction;
        
        public void SendAction(InputAction input)
        {
            onAction?.Invoke(input);    
        }
    }
}