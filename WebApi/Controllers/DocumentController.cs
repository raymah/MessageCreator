using Data;
using Data.Commands;
using Data.Database;
using MessageCreator.WebApi.Controllers.Hal.Resources;
using MessageCreator.WebApi.Models;
using RabbitMQ.Client;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Hal;

namespace WebApi.Controllers
{
    /// <summary>
    /// https://github.com/JakeGinnivan/WebApi.Hal
    /// </summary>
    public class DocumentController : ApiController
    {
        // GET api/document
        [HttpGet]
        public DocumentListRepresentation GetAll()
        {
            IRepository repo = new DataRepository(DatabaseConfiguration.ConnectionString);
            var data = repo.GetAll<DocumentRecord>("GetAllDocuments", null).Select(p => new DocumentRepresentation(){ Id=Base64Encode(p.ToString())}).ToArray();

            var resourceList = new DocumentListRepresentation(data, data.Count(), 1, 1,  new Link("Document", "/api/document/getall{?page}"));
            return resourceList;
        }

        // GET api/document/5
        [HttpGet]
        public DocumentRepresentation Get(string id)
        {
            var result = new DocumentRecord(Base64Decode(id));
            ExtractionCommand cmd = new ExtractionCommand();
            cmd.Id = result.DocumentHandle;
            cmd.RepositoryLocation = result.RepositoryType;


            using (var connection = new ConnectionFactory()
                                    {
                                        HostName = "192.168.10.103",
                                        UserName = "test",
                                        Password = "test"
                                    }.CreateConnection())
            {
                IMessageQueueSender sender = new RabbitMQSender(connection, "WorkerQueue");
                sender.Send(cmd);
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
            //read from C:temp for doc
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
