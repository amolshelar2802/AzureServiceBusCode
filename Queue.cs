 using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;

namespace ConsoleDemo
{
    class Queue
    {

        static string connectionString = "Endpoint=sb://amol-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LpyL17/4gzckMYLDejoLApmRKOo7UeBKb8wE6eAQezE=";
        static string queueName = "samplequeue";

        // public static async Task Main(string[] args)
        // {    
        //     const int numberOfMessages = 10;

        //     Console.WriteLine("======================================================");
        //     Console.WriteLine("Press ENTER key to exit after sending all the messages.");
        //     Console.WriteLine("======================================================");

        //     // Send messages.
        //     await SendMessagesAsync(numberOfMessages);

        //     Console.ReadKey();
        // }

    public static async Task SendMessageAsync()
    {
        // create a Service Bus client 
        await using (ServiceBusClient client = new ServiceBusClient(connectionString))
        {
            // create a sender for the queue 
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send
            ServiceBusMessage message = new ServiceBusMessage("Hello world!");

            // send the message
            await sender.SendMessageAsync(message);
            Console.WriteLine($"Sent a single message to the queue: {queueName}");
        }
    }

    private static Queue<ServiceBusMessage> CreateMessages()
    {
        // create a queue containing the messages and return it to the caller
        Queue<ServiceBusMessage> messages = new Queue<ServiceBusMessage>();
        
        messages.Enqueue(new ServiceBusMessage("First message"));
        messages.Enqueue(new ServiceBusMessage("Second message"));
        messages.Enqueue(new ServiceBusMessage("Third message"));
        return messages;
    }


public static async Task SendMessageBatchAsync()
    {
        // create a Service Bus client 
        await using (ServiceBusClient client = new ServiceBusClient(connectionString))
        {
            // create a sender for the queue 
            ServiceBusSender sender = client.CreateSender(queueName);

            // get the messages to be sent to the Service Bus queue
            Queue<ServiceBusMessage> messages = CreateMessages();

            // total number of messages to be sent to the Service Bus queue
            int messageCount = messages.Count;

            // while all messages are not sent to the Service Bus queue
            while (messages.Count > 0)
            {
                // start a new batch 
                using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

                // add the first message to the batch
                if (messageBatch.TryAddMessage(messages.Peek()))
                {
                    // dequeue the message from the .NET queue once the message is added to the batch
                    messages.Dequeue();
                }
                else
                {
                    // if the first message can't fit, then it is too large for the batch
                    throw new Exception($"Message {messageCount - messages.Count} is too large and cannot be sent.");
                }

                // add as many messages as possible to the current batch
                while (messages.Count > 0 && messageBatch.TryAddMessage(messages.Peek()))
                {
                    // dequeue the message from the .NET queue as it has been added to the batch
                    messages.Dequeue();
                }

                // now, send the batch
                await sender.SendMessagesAsync(messageBatch);

                // if there are any remaining messages in the .NET queue, the while loop repeats 
            }

            Console.WriteLine($"Sent a batch of {messageCount} messages to the topic: {queueName}");
        }
    }





//         // Connection String for the namespace can be obtained from the Azure portal under the 
//         // 'Shared Access policies' section.
//         const string ServiceBusConnectionString = "Endpoint=sb://amol-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LpyL17/4gzckMYLDejoLApmRKOo7UeBKb8wE6eAQezE=";
//         const string QueueName = "samplequeue";
//         static IQueueClient queueClient;

//         static void Main(string[] args)
//         {
//             MainAsync().GetAwaiter().GetResult();
//         }

//         static async Task MainAsync()
//         {
//             queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

//             Console.WriteLine("======================================================");
//             Console.WriteLine("Press ENTER key to exit after receiving all the messages.");
//             Console.WriteLine("======================================================");

//             // Register QueueClient's MessageHandler and receive messages in a loop
//             RegisterOnMessageHandlerAndReceiveMessages();
 
//             Console.ReadKey();

//             await queueClient.CloseAsync();
//         }


// static void RegisterOnMessageHandlerAndReceiveMessages()
//         {
//             // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
//             var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
//             {
//                 // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
//                 // Set it according to how many messages the application wants to process in parallel.
//                 MaxConcurrentCalls = 1,

//                 // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
//                 // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
//                 AutoComplete = false
//             };

//             // Register the function that will process messages
//             queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
//         }

// static async Task ProcessMessagesAsync(Message message, CancellationToken token)
//         {
//             // Process the message
//             Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

//             // Complete the message so that it is not received again.
//             // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
//             await queueClient.CompleteAsync(message.SystemProperties.LockToken);

//             // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
//             // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
//             // to avoid unnecessary exceptions.
//         }

//         static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
//         {
//             Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
//             var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
//             Console.WriteLine("Exception context for troubleshooting:");
//             Console.WriteLine($"- Endpoint: {context.Endpoint}");
//             Console.WriteLine($"- Entity Path: {context.EntityPath}");
//             Console.WriteLine($"- Executing Action: {context.Action}");
//             return Task.CompletedTask;
//         }

        // static void Main(string[] args)
        // {
        //     try
        //     {
        //         Console.WriteLine("Enter message");
        //         string msg = Console.ReadLine();
        //         string servicebusConnectionString = "Endpoint=sb://amol-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LpyL17/4gzckMYLDejoLApmRKOo7UeBKb8wE6eAQezE=";
        //         string queueName = "samplequeue";
        //         var message = new Message(Encoding.UTF8.GetBytes(msg));
                
        //         QueueClient qClient = new QueueClient(servicebusConnectionString, 
        //         queueName, ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);

        //         qClient.SendAsync(message);
        //         Console.WriteLine(msg + " message sent");

        //     }
        //     catch(Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }

        // }

        
    }
}
