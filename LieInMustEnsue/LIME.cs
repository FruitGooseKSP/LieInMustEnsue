using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LieInMustEnsue
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class LIME : MonoBehaviour
    {
        [KSPField]
        public double snoozeTime;

        public void Start()
        {
            snoozeTime = 0.467;                                         // an hour later than dawn
            KSP.UI.UIWarpToNextMorning.timeOfDawn = snoozeTime;
        }
    }
}
