using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeliver {
    int Deliver(ResourceTypes type, int amount);

    bool RequiresResource(ResourceTypes? type);
}
