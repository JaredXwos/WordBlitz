using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public enum TileSelectionMode
    {
        //
        // Summary:
        //     Tap Mode Only.
        TapOnly = 0,
        //
        // Summary:
        //     Both Swipe and Tap functionality, does not auto submit. //default setting
        SwipeTapManualSubmit = 1,
        //
        // Summary:
        //     Both Swipe and Tap functionality, without the submit button. //prefered setting
        SwipeTap = 2,
        //
        // Summary:
        //     Swipe Mode Only.
        SwipeOnly = 3,
    }
}
