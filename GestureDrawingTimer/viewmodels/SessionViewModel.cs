using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestureDrawingTimer.models;

namespace GestureDrawingTimer.viewmodels
{
    public class SessionViewModel
    {
        // Properties
        public int CurrentImageIndex; // TODO Session doesn't need this property...

        // Instance variables
        private Session mSession;

        // Constructor
        public SessionViewModel(Session session)
        {
            mSession = session;
        }
    }
}
