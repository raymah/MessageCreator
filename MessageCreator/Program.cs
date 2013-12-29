using Data;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var Connection = new ConnectionFactory() 
                                    { 
                                        HostName = "192.168.10.103", 
                                        UserName = "test", 
                                        Password = "test" 
                                    }.CreateConnection())
            {
                IMessageTopicSender sender = new RabbitMQTopicSender(Connection, "TopicQueue");

                sender.Send(string.Format("Message: 'derpy'"), "derp.derpy");
                sender.Send(string.Format("Message: 'derpa'"), "derp");
                sender.Send(string.Format("Message: 'derp#'"), "derp.derpy");
                
            }
        }
    }
}
