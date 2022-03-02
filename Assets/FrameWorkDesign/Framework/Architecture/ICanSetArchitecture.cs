using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface ICanSetArchitecture
    {
        void SetArchitecture(IArchitecture architecture);
    }
}