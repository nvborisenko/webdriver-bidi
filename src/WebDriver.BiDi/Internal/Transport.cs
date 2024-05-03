using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Internal;

internal class Transport : IDisposable
{
    private readonly Uri uri;
    private readonly ClientWebSocket webSocket;

    public Transport(Uri uri)
    {
        webSocket = new ClientWebSocket();
        this.uri = uri;
    }

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        await webSocket.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);

        IsClosed = false;
    }

    public async Task SendAsync(string message, CancellationToken cancellationToken)
    {
        var encoded = Encoding.UTF8.GetBytes(message);
        var buffer = new ArraySegment<byte>(encoded, 0, encoded.Length);
        await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
    }

    public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

    public async Task ReceiveMessageAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[2048];

        while (!IsClosed)
        {
            var endOfMessage = false;
            var response = new StringBuilder();

            while (!endOfMessage)
            {
                WebSocketReceiveResult result;

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);

                endOfMessage = result.EndOfMessage;

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    response.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    IsClosed = true;
                }
            }

            Debug.WriteLine($"RCV << {response}");

            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(response.ToString()));
        }
    }

    public bool IsClosed { get; private set; } = true;

    public void Dispose()
    {
        IsClosed = true;

        webSocket.Dispose();
    }
}