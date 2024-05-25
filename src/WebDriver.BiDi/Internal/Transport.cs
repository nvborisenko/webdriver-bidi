using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;
using System.Diagnostics;
using System.Text;

namespace OpenQA.Selenium.BiDi.Internal;

internal class Transport : IDisposable
{
    private readonly ClientWebSocket _webSocket;
    private readonly Uri _uri;

    public Transport(Uri uri)
    {
        _webSocket = new ClientWebSocket();
        _uri = uri;
    }

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        await _webSocket.ConnectAsync(_uri, cancellationToken).ConfigureAwait(false);
    }

    public async Task<T> ReceiveAsJsonAsync<T>(JsonSerializerOptions jsonSerializerOptions, CancellationToken cancellationToken)
    {
        var buffer = new ArraySegment<byte>(new byte[1024]);

        using var ms = new MemoryStream();

        WebSocketReceiveResult result;

        do
        {
            result = await _webSocket.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);
            ms.Write(buffer.Array, buffer.Offset, result.Count);
        } while (!result.EndOfMessage);

        ms.Seek(0, SeekOrigin.Begin);

#if DEBUG
        Debug.WriteLine($"RCV << {Encoding.UTF8.GetString(ms.ToArray())}");
#endif

        return JsonSerializer.Deserialize<T>(ms, jsonSerializerOptions)!;
    }

    public async Task SendAsJsonAsync<T>(T obj, JsonSerializerOptions jsonSerializerOptions, CancellationToken cancellationToken)
    {
        var buffer = JsonSerializer.SerializeToUtf8Bytes(obj, jsonSerializerOptions);

#if DEBUG
        Debug.WriteLine($"SND >> {Encoding.UTF8.GetString(buffer)}");
#endif

        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _webSocket.Dispose();
    }
}
