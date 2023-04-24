using System.Collections.Generic;
using BeautySalon.DAL.Entities;

namespace BeautySalon.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool EditClient(ref Client? client);

        bool ConfirmedInformation(string Information, string Caption);
        bool ConfirmedWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
        bool OpenAboutBox();
    }

}
