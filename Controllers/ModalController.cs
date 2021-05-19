using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Controllers
{
    public class ModalController
    {
        IModalView _view;

        public ModalController(IModalView view)
        {
            _view = view;
            view.SetController(this);
        }
    }
}
