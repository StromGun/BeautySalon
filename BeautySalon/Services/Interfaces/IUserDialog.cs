using BeautySalon.DAL.Entities;
using System.Collections;

namespace BeautySalon.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool EditClient(ref Client? client);
        bool EditOrder(ref Order? order);

        bool OpenServices(ICollection collection);
        bool OpenClientList(ref Client client);

        bool OpenFileDialog(ref string path);

        bool ConfirmedInformation(string Information, string Caption);
        bool ConfirmedWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
        bool OpenAboutBox();
    }

}
