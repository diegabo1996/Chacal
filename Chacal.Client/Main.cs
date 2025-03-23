using Chacal.Client.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Logging;
using TikTokLiveSharp.Client;
using TikTokLiveSharp.Events;

namespace Chacal.Client
{
    public partial class Main : Form
    {
        private readonly TikTokLiveClient client;
        public Main()
        {
            InitializeComponent();
            client = new TikTokLiveClient("kamake009", "");
            //client.OnConnected += Client_OnConnected;
            //client.OnDisconnected += Client_OnDisconnected;
            //client.OnRoomUpdate += Client_OnViewerData;
            //client.OnLiveEnded += Client_OnLiveEnded;
            //client.OnJoin += Client_OnJoin;
            client.OnChatMessage += Client_OnComment;
            //client.OnFollow += Client_OnFollow;
            //client.OnShare += Client_OnShare;
            //client.OnSubscribe += Client_OnSubscribe;
            //client.OnLike += Client_OnLike;
            //client.OnGiftMessage += Client_OnGiftMessage;
            //client.OnEmoteChat += Client_OnEmote;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await client.RunAsync(new System.Threading.CancellationToken());
        }
        private async Task RegisterMessage(string text)
        {
            if (txtComments.InvokeRequired)
            {
                txtComments.Invoke((MethodInvoker)delegate
                {
                    txtComments.AppendText(Environment.NewLine + text);
                });
            }
            else
            {
                txtComments.AppendText(Environment.NewLine + text);
            }

            await Task.CompletedTask;
        }
        private async void Client_OnComment(TikTokLiveClient sender, Chat e)
        {
            Console.WriteLine($"{e.Sender.UniqueId}: {e.Message}");
            await RegisterMessage($"{e.Sender.UniqueId}: {e.Message}");
        }
    }     
}
