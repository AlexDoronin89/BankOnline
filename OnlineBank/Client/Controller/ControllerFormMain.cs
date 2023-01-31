using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankEntityClassLibrary;
using Client.Model;
using Client.View;

namespace Client.Controller
{
    class ControllerFormMain
    {
        private ApiWorker _api;
        private Bank _view;
        private User _user;

        public ControllerFormMain(Bank view, User user)
        {
            _api = ApiWorker.GetInstance();
            _view = view;
            _user=user;
        }

        public void CloseApp()=>Environment.Exit(0);

        public async Task<List<Card>> GetUserCardsByIdAsync() => await _api.SelectCardsByIdUser(_user.Id);
        
        public async Task<bool> AddNewCardAsync() => await _api.InsertNewCards(_user.Id);

    }
}
