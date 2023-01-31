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
    class ControllerFormAuth
    {
        private ApiWorker _api;
        private AuthView _view;

        public ControllerFormAuth(AuthView form)
        {
            _api = ApiWorker.GetInstance();
            _view = form;
        }

        public async Task AuthorizeAsync(string login, string password)
        {
            try
            {
                User user = await _api.SelectUserByLoginAndPassword(login, password);

                if (user == null)
                    throw new Exception("Invalid login or password");

                _view.Hide();

                Bank view = new Bank(user);
                view.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
