 using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {

        
    //Queue q = new Queue();
    static async Task Main()
    {
        // //Calling Quueues
        // // send a message to the queue
        // await ConsoleDemo.Queue.SendMessageAsync();

        // // send a batch of messages to the queue
        // await ConsoleDemo.Queue.SendMessageBatchAsync();


        //Calling Topics
        // // send a message to the topic
        // await ConsoleDemo.Topics.SendMessageToTopicAsync();

        // // send a batch of messages to the topic
        // await ConsoleDemo.Topics.SendMessageBatchToTopicAsync();

        // // receive messages from the subscription
        // await ConsoleDemo.Topics.ReceiveMessagesFromSubscriptionAsync();

        await ConsoleDemo.Topics.SendObjectMessageToTopicAsync();

        await ConsoleDemo.Topics.ReceiveObjectMessagesFromSubscriptionAsync();

    }
      
    }
}



//// * O L D   C O D E * ////
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

  