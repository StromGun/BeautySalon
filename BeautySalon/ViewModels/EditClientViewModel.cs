using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.ViewModels
{
    internal class EditClientViewModel : ViewModel
    {
        public Client Client { get; set; } = new();


        public EditClientViewModel(Client? client)
        {

            Client = client!;

        }
    }
}
