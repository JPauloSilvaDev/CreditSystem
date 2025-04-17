using System.ComponentModel;
using Credit.System.App.Models;
using Platform.Entity.ServiceSystem;

namespace Credit.System.App.Mapper
{
    public static class ClientMapper
    {

        public static List<ClientViewModel> MapClientToClientViewModel(List<Client> clients)
        {
            var viewModelClients = new List<ClientViewModel>();

            try
            {
                foreach (Client client in clients)
                {
                    var viewModelClient = new ClientViewModel()
                    {
                        ClientId = client.ClientId,
                        CompanyId = client.CompanyId,
                        PrimaryName = client.PrimaryName,
                        SecondaryName = client.SecondaryName,
                        Email = client.Email,
                        Document = client.Document,
                        PrimaryPhone = client.PrimaryPhone,
                        SecondaryPhone = client.SecondaryPhone,
                        Zipcode = client.Zipcode,
                        Street = client.Street,
                        StreetNumber = client.StreetNumber,
                        Observation = client.Observation,
                        State = client.State,
                        City = client.City,
                        Neighborhood = client.Neighborhood
                    };

                    viewModelClients.Add(viewModelClient);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return viewModelClients;
        }

        public static Client MapClientViewModelToClient(ClientViewModel viewModel)
        {

            try
            {
                var client = new Client()
                {
                    ClientId = viewModel.ClientId,
                    CompanyId = viewModel.CompanyId,
                    PrimaryName = viewModel.PrimaryName,
                    SecondaryName = viewModel.SecondaryName,
                    Email = viewModel.Email,
                    Document = viewModel.Document,
                    PrimaryPhone = viewModel.PrimaryPhone,
                    SecondaryPhone = viewModel.SecondaryPhone,
                    Zipcode = viewModel.Zipcode,
                    Street = viewModel.Street,
                    StreetNumber = viewModel.StreetNumber,
                    Observation = viewModel.Observation,
                    State = viewModel.State,
                    City = viewModel.City,
                    Neighborhood = viewModel.Neighborhood
                };
               
                return client;
            }
            catch (Exception)
            {
                throw;
            }

            
        }

    }

}
