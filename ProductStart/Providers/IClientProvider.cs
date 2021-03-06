﻿using System.Collections.Generic;
using ClassLibrary1;

namespace ProductStart.Providers
{
    public interface IClientProvider
    {
        IEnumerable<Entity> GetAll();
        Client Get(int id);
        Client Add(Client client);
        Client Update(int id, Client client);
        void Delete(int id);
    }
}