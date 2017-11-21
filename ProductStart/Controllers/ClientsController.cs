﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ClassLibrary1;
using ClassLibrary1.ModelFactory;
using Microsoft.Web.Http;
using ProductStart.Providers;

namespace ProductStart.Controllers

{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [AllowAnonymous]
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        private readonly IClientProvider _provider;
        private ClientModelFactory _modelFactory;

        public ClientsController(IClientProvider provider)
        {
            _provider = provider;
        }

        [Route("", Name = "Clinets")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                
                _modelFactory = new ClientModelFactory(this.Request);
                List<ClientModel> clientModels = new List<ClientModel>();
                var clientLinks = _provider.GetAll();
                foreach (var client in clientLinks)
                {
                    clientModels.Add(_modelFactory.CreateClientModel(client));
                }

                return Ok(clientModels);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }

        }

        // GET: Clients
        [Route("~/api/client/{clientid:int}", Name = "Client")]
        public IHttpActionResult Get(int clientid)
        {
            var client = _provider.Get(clientid);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // GET: Clients
        [Route("~/api/client/{clientname:alpha}", Name = "ClientByName")]
        [HttpGet]
        public IHttpActionResult Get(string clientname)
        {
            var client = _provider.Get(clientname);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [Route("", Name = "AddClinet")]
        [HttpPost]
        public HttpResponseMessage PostClinet(Client client)
        {
            var newITem = _provider.Add(client);
            var response = Request.CreateResponse<Client>(HttpStatusCode.Created, newITem);

            string uri = Url.Link("Client", new { clientid = newITem.Id });

            response.Headers.Location = new Uri(uri);
            return response;
        }

        /*
         * @ToDo: Add a separate HttpPatch
         */
        [Route("{id}", Name = "UpdateClient")]
        [HttpPut]
        [HttpPatch]
        public IHttpActionResult PutClient(int id, Client client)
        {
            client.Id = id;
            var result = _provider.Update(id, client);
            if (result == null)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.Accepted, result); //Request.CreateResponse<Client>(HttpStatusCode.Accepted, result);

        }

        
        [Route("{id}", Name = "DeleteClients")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var response = Get(id);

            if (response.GetType() == typeof(NotFoundResult))
            {
                return NotFound();
            }

            _provider.Delete(id);
            return Ok();
        }
    }
}
