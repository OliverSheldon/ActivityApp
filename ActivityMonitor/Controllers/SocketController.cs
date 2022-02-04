using ActivityMonitor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityMonitor.Controllers
{
    public class SocketController : Controller
    {
        private List<WebSocket> Sockets = new List<WebSocket>();

        [HttpGet("/ws")]
        public async Task Sync()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                                   HttpContext.WebSockets.AcceptWebSocketAsync();
                Sockets.Add(webSocket);
                await SendRecieve(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private async Task SendRecieve(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            
            while (!result.CloseStatus.HasValue)
            {
                //buffer = new byte[result.Count];
                //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                string messageRecieved = Encoding.UTF8.GetString(buffer, 0, result.Count);

                ActivityState actSt = new ActivityState();
                await actSt.UpdateUser(JsonConvert.DeserializeObject<User>(messageRecieved));
                var returnMessage = JsonConvert.SerializeObject(actSt);

                ArraySegment<Byte> newBuffer = new ArraySegment<Byte>(Encoding.UTF8.GetBytes(returnMessage));
                foreach (WebSocket s in Sockets)
                {
                    await webSocket.SendAsync(newBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
