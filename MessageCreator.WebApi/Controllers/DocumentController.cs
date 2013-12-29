using Data;
using Data.Database;
using MessageCreator.WebApi.Controllers.Hal.Resources;
using MessageCreator.WebApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace MessageCreator.WebApi.Controllers
{
    public class DocumentController : ApiController
    {
        // GET api/document
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            IRepository repo = new DataRepository(DatabaseConfiguration.ConnectionString);
            var data = repo.GetAll<DocumentRecord>("GetAllDocuments", null).Select(p => Base64Encode(p.ToString())).ToArray();

            return data;
        }

        // GET api/document/5
        [HttpGet]
        public DocumentRepresentation Get(string id)
        {
            var result = new DocumentRecord(Base64Decode(id));

            using (var connection = new ConnectionFactory()
                                    {
                                        HostName = "192.168.10.103",
                                        UserName = "test",
                                        Password = "test"
                                    }.CreateConnection())
            {
                IMessageQueueSender sender = new RabbitMQSender(connection, "WorkerQueue");
                sender.Send(result);
            }

            return new DocumentRepresentation()
            {
                 Id=id
            };
        }

        [HttpGet]
        public HttpResponseMessage DownloadDocument(string id)
        {
            var result = new DocumentRecord(Base64Decode(id));

            var res = new HttpResponseMessage();
            res.Content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(result.ToString()));
            return res;
        }

        // POST api/document
        public void Post([FromBody]string value)
        {
        }

        // PUT api/document/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/document/5
        public void Delete(int id)
        {
        }

        public static string Base64Encode<T>(T message) where T : class
        {
            byte[] value = null;
            value = System.Text.Encoding.UTF8.GetBytes(message.ToString());

            return System.Convert.ToBase64String(value);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
