using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxaliaRevitToolkit.Services
{
    public interface IUserInteractionService
    {
        bool ConfirmDeletion(string itemName);
        void ShowErrorMessage(string message);
    }
}
