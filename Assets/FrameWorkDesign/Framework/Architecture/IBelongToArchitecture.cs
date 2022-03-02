using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface IBelongToArchitecture
    {
        IArchitecture GetArchitecture();
    }
}
