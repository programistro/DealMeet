using TL;
using WTelegram;

namespace DealMeet.Service;

public class TelegramService
{
    private WTelegram.UpdateManager Manager;
    private string User(long id) => Manager.Users.TryGetValue(id, out var user) ? user.ToString() : $"User {id}";
    private string Chat(long id) => Manager.Chats.TryGetValue(id, out var chat) ? chat.ToString() : $"Chat {id}";
    private string Peer(Peer peer) => Manager.UserOrChat(peer)?.ToString() ?? $"Peer {peer?.ID}";
    
    private static Client Client;
    
    public async Task Login()
    {
        using (Client = new WTelegram.Client(Config))
        {
            await Client.LoginUserIfNeeded();
            Console.WriteLine(Client.Disconnected);

            // Console.WriteLine("This user has joined the following:");
            // foreach (var (id, chat) in chats.chats)
            //     if (chat.IsActive)
            //         Console.WriteLine($"{id,10}: {chat}");
            // Console.Write("Type a chat ID to send a message: ");
            // long chatId = long.Parse(Console.ReadLine());
            // Console.WriteLine($"Sending a message in chat {chatId}: {target.Title}");
            // var chats = await Client.Messages_GetAllChats();
            // var target = chats.chats[2173961414];
            // // await Client.SendMessageAsync(target, "Hello, World");
            // Manager = Client.WithUpdateManager(ClientOnOnUpdates/*, "Updates.state"*/);
            // var channel = (Channel)chats.chats[2173961414]; // the channel we want
            // for (int offset = 0; ;)
            // {
            //     var participants = await Client.Channels_GetParticipants(channel, null, offset);
            //     foreach (var (id, user) in participants.users)
            //         Console.WriteLine(user);
            //     offset += participants.participants.Length;
            //     if (offset >= participants.count || participants.participants.Length == 0) break;
            // }
        };
    }
    
    // private async Task ClientOnOnUpdates(Update update)
    // {
    //     switch (update)
    //     {
    //         case UpdateNewMessage unm: 
    //             var chats = await Client.Messages_GetAllChats();
    //             var target = chats.chats[2173961414];
    //             await Client.SendMessageAsync(target, "Вау круто");
    //             break;
    //         case UpdateEditMessage uem: await HandleMessage(uem.message, true); break;
    //         case UpdateDeleteMessages udm: Console.WriteLine($"{udm.messages.Length} message(s) deleted"); break;
    //         case UpdateUserTyping uut: Console.WriteLine($"{User(uut.user_id)} is {uut.action}"); break;
    //         case UpdateUserStatus uus: Console.WriteLine($"{User(uus.user_id)} is now {uus.status.GetType().Name[10..]}"); break;
    //         case UpdateUserName uun: Console.WriteLine($"{User(uun.user_id)} has changed profile name: {uun.first_name} {uun.last_name}"); break;
    //         case UpdateUser uu: Console.WriteLine($"{User(uu.user_id)} has changed infos/photo"); break;
    //         default: Console.WriteLine(update.GetType().Name); break; // there are much more update types than the above example cases
    //     }
    // }
    
    // private  Task HandleMessage(MessageBase messageBase, bool edit = false)
    // {
    //     if (edit) Console.Write("(Edit): ");
    //     switch (messageBase)
    //     {
    //         case Message m: Console.WriteLine($"{Peer(m.from_id) ?? m.post_author} in {Peer(m.peer_id)}> {m.message}"); break;
    //         case MessageService ms: Console.WriteLine($"{Peer(ms.from_id)} in {Peer(ms.peer_id)} [{ms.action.GetType().Name[13..]}]"); break;
    //     }
    //     return Task.CompletedTask;
    // }
    
    public string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "25421922";
            case "api_hash": return "8ed9b2cb68b4b22166105e81ddafb969";
            case "phone_number": return "+79204489841";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine();
            // case "password": return "Roman2021090";
            default: return null;                  // let WTelegramClient decide the default config
        }
    }
}