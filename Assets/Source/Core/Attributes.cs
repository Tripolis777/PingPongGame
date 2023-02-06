using System;
using UnityEngine;

namespace Source.Core
{
    public class SelectImplementationAttribute : PropertyAttribute
    {
        public Type instanceType;

        public SelectImplementationAttribute(Type instanceType)
        {
            this.instanceType = instanceType;
        }
    }
}